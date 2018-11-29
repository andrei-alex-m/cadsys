using System;
using System.Text;
using CS.Data.Entities;
using Caly.Common;
using System.Collections.Generic;
using System.Linq;
using CS.EF;
using CS.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CS.CadGen
{
    public class Exporter : IExporter
    {
        CadSysContext context;
        IServiceBuilder serviceBuilder;
        IMatcher comboMatcher;
        IMatchProcessor comboMatchProcessor;


        public Exporter(CadSysContext _context, IServiceBuilder _serviceBuilder)
        {
            context = _context;
            serviceBuilder = _serviceBuilder;
            comboMatcher = (IMatcher)serviceBuilder.GetService("CombosIndexMatcher");
            comboMatchProcessor = (IMatchProcessor)serviceBuilder.GetService("CombosIndexMatchProcessor");
        }


        public string[] Export(int indexImobil, IEnumerable<Point> coords, double suprafata, string nrCadGeneral, string sector)
        {
            var result = new List<string>();
            Imobil imobil;
            object locker = new object();

            lock(locker)
            {
                imobil = context.Imobile.FirstOrDefault(x => x.Parcele.Any(y => y.Index == indexImobil));
            }

            if (imobil == null)
            {
                return result.ToArray();
            }

            //var proprietari = context.Proprietari.Include(x => x.Adresa).SelectMany(y => y.Inscrieri.Select(z => z.InscriereDetaliu)).Where(w => w.ImobilReferintaId == imobil.Id);


            lock(locker)
            {
                context.Set<UAT>().Load();
                context.Set<Localitate>().Load();
                context.Set<Judet>().Load();

                context.Entry(imobil).Reference(x => x.Adresa).Load();

                context.Entry(imobil.Adresa).Reference(x => x.UAT).Load();
                context.Entry(imobil.Adresa).Reference(x => x.Localitate).Load();

                context.Entry(imobil).Collection(x => x.Parcele).Load();
                context.Entry(imobil.Parcele.FirstOrDefault()).Reference(x => x.Tarla).Load();

                context.Entry(imobil).Collection(x => x.InscrieriDetaliu).Load();
                foreach (var iD in imobil.InscrieriDetaliu)
                {
                    context.Entry(iD).Reference(x => x.ModDobandire).Load();
                    context.Entry(iD).Reference(x => x.TipDrept).Load();
                    context.Entry(iD).Reference(x => x.TipInscriere).Load();

                    context.Entry(iD).Collection(x => x.InscrieriActe).Load();
                    context.Entry(iD).Collection(x => x.InscrieriImobile).Load();
                    context.Entry(iD).Collection(x => x.InscrieriProprietari).Load();

                    foreach (var ia in iD.InscrieriActe)
                    {
                        context.Entry(ia).Reference(x => x.ActProprietate).Load();
                        if (ia.ActProprietate!=null)
                        {
                            context.Entry(ia.ActProprietate).Reference(x => x.TipActProprietate).Load();
                        }
                    }

                    foreach(var ip in iD.InscrieriProprietari)
                    {
                        context.Entry(ip).Reference(x => x.Proprietar).Load();
                        if (ip.Proprietar!=null)
                        {
                            //context.Entry(ip.Proprietar).Reference(x => x.Adresa).Load();
                            
                            //if (ip.Proprietar.Adresa!=null)
                            //{
                            //    var adr = ip.Proprietar.Adresa;
                            //    context.Entry(adr).Reload();
                            //    context.Entry(adr).Reference(x => x.UAT).Load();
                            //    context.Entry(adr).Reference(x => x.Localitate).Load();
                            //}
                        }
                    }

                    foreach (var ii in iD.InscrieriImobile)
                    {
                        context.Entry(ii).Reference(x => x.Imobil).Load();
                    }
                }
            }

            result.AddRange(ExportPozitia(imobil));
            var proprietari = imobil.InscrieriDetaliu.SelectMany(x => x.InscrieriProprietari).Select(y => y.Proprietar);

            //var proprietari = context.Proprietari.Include(x => x.Adresa).ThenInclude(y=>y.Localitate).ThenInclude(y=>y.UAT).Where(x => propIds.Contains(x.Id));

            foreach (var p in proprietari)
            {
                var adr = context.Set<Adresa>().Include(x=>x.UAT).Include(x=>x.Localitate).FirstOrDefault(x => x.Id == p.AdresaId);
                result.Add(ExportProprietar(p) + "$" + ExportAdresa(p.Adresa, comboMatcher, comboMatchProcessor));
            }

            var linesInscrieriPt3Short = new List<string>();
            var linesInscrieriPt3Long = new List<string>();

            var linesInscrieriPt2Short = new List<string>();
            var linesInscrieriPt2Long = new List<string>();


            foreach (var idPt3 in imobil.InscrieriDetaliu.Where(x=>x.ParteaCF==3))
            {
                var currentLines = ExportInscriere(idPt3, proprietari, comboMatcher, comboMatchProcessor);
                linesInscrieriPt3Short.Append(currentLines[0]);
                linesInscrieriPt3Long.Append(currentLines[1]);
            }



            foreach (var idPt2 in imobil.InscrieriDetaliu.Where(x => x.ParteaCF == 2))
            {
                var currentLines = ExportInscriere(idPt2, proprietari, comboMatcher, comboMatchProcessor);
                linesInscrieriPt2Short.Add(currentLines[0]);
                linesInscrieriPt2Long.Add(currentLines[1]);
            }

            result.AddRange(linesInscrieriPt3Short);
            result.AddRange(linesInscrieriPt3Long);

            result.AddRange(linesInscrieriPt2Short);
            result.AddRange(linesInscrieriPt2Long);

            result.Add(ExportParcela(imobil.Parcele.FirstOrDefault(),suprafata));

            result.Add("#02#" + ExportAdresa(imobil.Adresa, comboMatcher, comboMatchProcessor)/*+"0|"*/);//extravilan
            result.Add(ExportImobil(imobil, nrCadGeneral, sector));

            result.Add(ExportCoordonate(coords));

            return result.ToArray();

        }

        static List<string> ExportPozitia(Imobil imobil)
        {
            var result = new List<string>();

            var iPt3 = imobil.InscrieriDetaliu.Where(x => x.ParteaCF == 3).Select(y => y.Pozitia.Value);

            if (iPt3.Any())
            {
                result.Add("#7773#" + string.Join("",iPt3.Select(x => $"{x}|")));
            }

            var iPt2 = imobil.InscrieriDetaliu.Where(x => x.ParteaCF == 2).Select(y => y.Pozitia.Value);

            if (iPt2.Any())
            {
                result.Add("#7772#" + string.Join("",iPt2.Select(x => $"{x}|")));
            }
            return result;
        }

        static string ExportImobil(Imobil imobil, string nrCadGeneral, string sector)
        {
            StringBuilder builder = new StringBuilder("#01#");
            builder.Append(nrCadGeneral).Append('|');
            builder.Append(sector).Append('|');
            builder.Append('0').Append('|'); //cooperativizata
            builder.Append('1').Append('|'); //dunno
            builder.Append(imobil.IdentificatorElectronic).Append('|');
            builder.Append(imobil.NumarCadastral).Append('|');
            builder.Append(imobil.NumarCarteFunciara).Append('|');
            builder.Append('1').Append('|'); //neimprejmuit
            builder.Append('0').Append('|'); //suprafatadiminuata
            builder.Append(imobil.NumarTopografic).Append('|');
            //adresa
            return builder.ToString();
        }

        static string ExportProprietar(Proprietar proprietar)
        {
            StringBuilder builder = new StringBuilder("#07#");

            switch (proprietar.TipPersoana)
            {
                case TipPersoana.N:
                    builder.Append("sr").Append('|');
                    builder.Append("NEIDENTIFICAT").Append('|');
                    builder.Append(@"obs ").Append(proprietar.Observatii).Append('|');
                    builder.Append("$||||||||||||||||");
                    break;
                case TipPersoana.F:
                    builder.Append("pf").Append('|');
                    builder.Append(proprietar.Nume.Trim().ToUpper()).Append('|');
                    builder.Append(proprietar.Prenume.Trim().ToUpper()).Append('|');
                    builder.Append(proprietar.Identificator ?? 9999999999999).Append('|');
                    //tiganie
                    if (proprietar.TipActIdentitate.HasValue && proprietar.TipActIdentitate.Value != TipActIdentitate.Deces)
                    {
                        switch (proprietar.TipActIdentitate.Value)
                        {
                            case TipActIdentitate.BI:
                                builder.Append(1).Append('|');
                                break;
                            case TipActIdentitate.CI:
                                builder.Append(2).Append('|');
                                break;
                            case TipActIdentitate.Pasaport:
                                builder.Append(3).Append('|');
                                break;
                        }
                        builder.Append(proprietar.Serie).Append('|');
                        builder.Append(proprietar.Numar).Append('|');
                    }
                    builder.Append(proprietar.Initiala).Append('|');
                    builder.Append('|'); // nume anterior
                    builder.Append("RO").Append('|');//cetatenie
                    builder.Append('|');//telefon
                    builder.Append('|');//email
                    if (proprietar.TipActIdentitate.HasValue && proprietar.TipActIdentitate.Value == TipActIdentitate.Deces)// u sure?
                    {
                        builder.Append(1).Append('|');
                    }
                    else
                    {
                        builder.Append(0).Append('|');
                    }
                    if (proprietar.TipActIdentitate.HasValue && proprietar.TipActIdentitate.Value == TipActIdentitate.Deces)
                    {
                        builder.Append("obs DECEDAT ");
                    }
                    builder.Append(proprietar.Observatii).Append('|');
                    break;
                case TipPersoana.J:
                    builder.Append("pj").Append('|');
                    builder.Append(proprietar.Nume.Trim().ToUpper()).Append('|');
                    builder.Append(proprietar.Identificator).Append('|');
                    builder.Append(@"obs ").Append(proprietar.Observatii).Append('|');
                    break;
            }
            return builder.ToString();
        }

        //#02# inainte, 1| dupa prentru imobil
        static string ExportAdresa(Adresa adresa, IMatcher matcher, IMatchProcessor matchProcessor)
        {

            StringBuilder builder = new StringBuilder();

            //judet
            var clasJudet = new List<Classification>()
            {
                new Classification(0,"root"),
                new Classification(1,"Judet")
            };
            var matchJudet = matcher.Match(clasJudet, adresa.UAT.JudetId.ToString(), matchProcessor);

            builder.Append(matchJudet.Count > 0 ? matchJudet[0].Name : "").Append('|');
            builder.Append(adresa.UAT?.Denumire).Append('|');
            builder.Append(adresa.Localitate?.Denumire).Append('|');
            builder.Append("0||"); //district, tip district

            //tipStrada
            var clasTipStrada = new List<Classification>()
            {
                new Classification(0,"root"),
                new Classification(1,"TipStrada")
            };
            var matchTipStrada = matcher.Match(clasTipStrada, adresa.TipStrada, matchProcessor);

            builder.Append(matchTipStrada.Count > 0 ? matchTipStrada[0].Name : "0").Append('|');
            builder.Append(adresa.Strada).Append('|');
            builder.Append(adresa.Numar).Append('|');
            builder.Append("999999").Append('|');
            builder.Append('|');//tronson
            builder.Append(adresa.Bloc).Append('|');
            builder.Append(adresa.Scara).Append('|');
            builder.Append(adresa.Etaj).Append('|');
            builder.Append(adresa.Apt).Append('|');
            builder.Append('|');//in afara Romaniei
            builder.Append(adresa.Descriere).Append('|');
            builder.Append(adresa.SIRSUP == adresa.SIRUTA ? '0' : '1').Append('|');//intravilan/extravilan
            return builder.ToString();
        }

        static string ExportParcela(Parcela parcela, double suprafata)
        {
            StringBuilder builder = new StringBuilder("#03#");
            builder.Append(1).Append('|');//numarul parcelei in imobil
            builder.Append(Math.Round(suprafata,0)).Append('|');
            builder.Append(0).Append('|');//intravilan
            builder.Append('1').Append('|');//Arabil
            builder.Append('|');//valoare impozitare
            builder.Append(parcela.NumarTitlu).Append('|');
            builder.Append(parcela.Tarla?.Denumire).Append('|');
            builder.Append(parcela.Denumire).Append('|');
            builder.Append('|'); // nr topografic
            //builder.Append('|');//mentiuni
            return builder.ToString();
        }

        //include toate tipurile de inscrieri si entitatile asociate + asocierile lor :D
        // calculeaza pozitia
        // TipDrept
        // InscrieriProprietari, Proprietar
        // InscrieriActeProprietate, ActProprietate
        static List<string> ExportInscriere(InscriereDetaliu inscriereD, IEnumerable<Proprietar> proprietariImobil, IMatcher matcher, IMatchProcessor matchProcessor)
        {
            StringBuilder builder1 = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();

            var clasTipDocument = new List<Classification>()
            {
                new Classification(0,"root"),
                new Classification(1,"TipDocument")
            };

            var clasModDobandire = new List<Classification>()
            {
                new Classification(0,"root"),
                new Classification(1,"ModDobandire")
            };

            var clasTipInscriere = new List<Classification>()
            {
                new Classification(0,"root"),
                new Classification(1,"TipInscriere")
            };

            var clasTipDreptParte2 = new List<Classification>()
            {
                new Classification(0,"root"),
                new Classification(1,"TipDrept"),
                new Classification(2,"Parte2")
            };

            var clasTipDreptParte3 = new List<Classification>()
            {
                new Classification(0,"root"),
                new Classification(1,"TipDrept"),
                new Classification(2,"Parte3")
            };

            switch (inscriereD.ParteaCF)
            {
                case 2:
                    builder1.Append("#05#T").Append(' ', 6);
                    break;
                case 3:
                    builder1.Append("#06#T").Append(' ', 6);
                    break;
            }

            if (inscriereD.TipInscriere.Denumire == "NOTATION")
            {
                inscriereD.TipDrept = null;
                inscriereD.ModDobandire = null;
                inscriereD.Cota = string.Empty;
                inscriereD.Moneda = string.Empty;
                inscriereD.Valoarea = string.Empty;
                inscriereD.DetaliiDrept = string.Empty;
            }

            builder1.Append(inscriereD.TipInscriere?.Descriere).Append(' ', 6);
            builder1.Append(inscriereD.TipDrept?.Denumire.ToUpper()).Append(' ', 6);
            builder1.Append(inscriereD.Cota).Append(' ', 6);
            inscriereD.InscrieriProprietari.ToList().ForEach(x =>
            {
                switch (x.Proprietar.TipPersoana)
                {
                    case TipPersoana.F:
                        builder1.Append(x.Proprietar.Nume.Trim().ToUpper()).Append(' ').Append(x.Proprietar.Prenume.Trim().ToUpper());
                        break;
                    case TipPersoana.J:
                        builder1.Append(x.Proprietar.Nume.ToUpper());
                        break;
                    case TipPersoana.N:
                        builder1.Append("NEIDENTIFICAT");
                        break;
                }
                builder1.Append('.');
            });
            builder1.Remove(builder1.Length - 1, 1); //ultimul punct

            switch (inscriereD.ParteaCF)
            {
                case 2:
                    builder2.Append("#55#");
                    break;
                case 3:
                    builder2.Append("#66#");
                    break;
            }

            var act = inscriereD.InscrieriActe.FirstOrDefault().ActProprietate;

            builder2.Append(act.Numar).Append('|');
            builder2.Append(act.Data.HasValue ? act?.Data.Value.ToString("dd/MM/yyyy") : "").Append('|');

            var matchTipDocument = matcher.Match(clasTipDocument, act.TipActProprietate.TipDocumentId.ToString(), matchProcessor);

            builder2.Append(matchTipDocument.Count > 0 ? matchTipDocument[0].Name : "0").Append('|');
            builder2.Append(act.Emitent);
            builder2.Append('|'); //observatii
            builder2.Append(0).Append('|'); // no fucking clue

            var matchTipInscriere = matcher.Match(clasTipInscriere, inscriereD.TipInscriere?.Denumire, matchProcessor);
            builder2.Append(matchTipInscriere.Count > 0 ? matchTipInscriere[0].Name : "").Append('|');

            List<Classification> matchTipDrept = new List<Classification>();
            switch (inscriereD.ParteaCF)
            {
                case 2:
                    matchTipDrept = matcher.Match(clasTipDreptParte2, inscriereD.TipDreptId.ToString(), matchProcessor);
                    break;
                case 3:
                    matchTipDrept = matcher.Match(clasTipDreptParte3, inscriereD.TipDreptId.ToString(), matchProcessor);
                    break;
            }
            builder2.Append(matchTipDrept.Count > 0 ? matchTipDrept[0].Name : "0").Append('|');

            var matchModDobandire = matcher.Match(clasModDobandire, inscriereD.ModDobandire?.Denumire, matchProcessor);
            builder2.Append(matchModDobandire.Count > 0 ? matchModDobandire[0].Name : "0").Append('|');

            builder2.Append(inscriereD.Cota).Append('|');
            builder2.Append(inscriereD.Nota).Append('|');

            var propIndexes = inscriereD.InscrieriProprietari.ToList().Select(x => proprietariImobil.ToList().IndexOf(x.Proprietar));
            builder2.Append(string.Join(' ', propIndexes)).Append('|');

            builder2.Append('|'); //moneda
            builder2.Append('|'); //valoare

            builder2.Append(inscriereD.DetaliiDrept).Append('|');
            builder2.Append(inscriereD.NumarCerere).Append('|');
            builder2.Append(inscriereD.DataCerere.HasValue ? inscriereD.DataCerere.Value.ToString("yyyy-MM-dd") + "T00:00:00+02:00" : "").Append('|');
            builder2.Append(inscriereD.Observatii).Append('|');
            builder2.Append("poz#").Append(inscriereD.Pozitia).Append('|');

            return new List<string>
            {
                builder1.ToString(),
                builder2.ToString()
            };
        }

        static string ExportCoordonate(IEnumerable<Point> points)
        {
            var result = new StringBuilder("#00#");
            foreach(var p in points)
            {
                result.Append(p.X.ToString("0.000")).Append('_').Append(p.Y.ToString("0.000")).Append('|');
            }
            return result.ToString();
        }
    }
}
