using System;
using System.Collections.Generic;
using System.Linq;
using CS.Data.DTO.Excel;
using CS.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CS.Data.Mappers
{
    public static class InscriereDetaliuMapperExtensions
    {
        enum cazuri
        {
            nustim,
            titlu,
            sparge,
            nusparge
        }
        /// <summary>
        /// Tiganie
        /// </summary>
        /// <returns>The dto.</returns>
        /// <param name="outputInscriereD">Output inscriere d.</param>
        /// <param name="proprietari">Proprietari.</param>
        /// <param name="acte">Acte.</param>
        /// <param name="parcele">Parcele.</param>
        public static IEnumerable<InscriereDetaliu> FromDTO(List<OutputInscriereDetaliu> outputInscriereD, IQueryable<Proprietar> proprietari, IQueryable<ActProprietate> acte, IQueryable<Parcela> parcele, IEnumerable<ModDobandire> moduriDobandire, IEnumerable<TipDrept> tipuriDrept, IEnumerable<TipInscriere> tipuriInscriere)
        {
            cazuri caz = cazuri.nustim;
            string cotaGenerala = string.Empty;

            var excelRow = outputInscriereD.Min(x => x.RowIndex);

            var modDobandire = outputInscriereD.Select(x => x.ModDobandire).FirstOrDefault(x => !string.IsNullOrEmpty(x));
            var tipInscriere = outputInscriereD.Select(x => x.TipInscriere).FirstOrDefault(x => !string.IsNullOrEmpty(x));
            var tipDrept = outputInscriereD.Select(x => x.TipDrept).FirstOrDefault(x => !string.IsNullOrEmpty(x));
            var parteaCF = outputInscriereD.Select(x => x.ParteaCF).FirstOrDefault(x => x.HasValue);
            var nota = outputInscriereD.Select(x => x.Nota).FirstOrDefault(x => !string.IsNullOrEmpty(x));
            var observatii = outputInscriereD.Select(x => x.Observatii).FirstOrDefault(x => !string.IsNullOrEmpty(x));
            var detaliiDrept = outputInscriereD.Select(x => x.DetaliiDrept).FirstOrDefault(x => !string.IsNullOrEmpty(x));
            var pozitia = outputInscriereD.Select(x => x.Pozitia).FirstOrDefault(x => x.HasValue);
            var numarCerere = outputInscriereD.Select(x => x.NumarCerere).FirstOrDefault(x => x.HasValue);
            var dataCerere = outputInscriereD.Select(x => x.DataCerere).FirstOrDefault(x => x.HasValue);

            var indexParcela = outputInscriereD.FirstOrDefault(x => x.IndexParcela.HasValue).IndexParcela.Value;
            var indexAct = outputInscriereD.FirstOrDefault(x => x.IndexAct.HasValue).IndexAct.Value;
            var indecsiProprietari = outputInscriereD.Where(x => x.IndexProprietar.HasValue).Select(x => new { index = x.IndexProprietar.Value, cota = x.CotaParte }).Distinct().ToList();

            if (indecsiProprietari.All(x => !string.IsNullOrEmpty(x.cota)))
            {
                caz = cazuri.sparge;
            }

            if (indecsiProprietari.Count(x => !string.IsNullOrEmpty(x.cota)) == 1)
            {
                caz = cazuri.nusparge;
                cotaGenerala = indecsiProprietari.Single(x => string.IsNullOrEmpty(x.cota)).cota;
            }

            if (indecsiProprietari.Count == 0)
            {
                caz = cazuri.titlu;
            }

            var parcela = parcele.FirstOrDefault(y => y.Index == indexParcela);

            var act = acte.FirstOrDefault(y => y.Index == indexAct);

            var modDobandireId = string.IsNullOrEmpty(modDobandire) ? act?.TipActProprietate?.ModDobandireId : moduriDobandire.FirstOrDefault(x => string.Equals(modDobandire, x.Denumire, StringComparison.InvariantCultureIgnoreCase))?.Id;
            var parteaCFId = parteaCF.HasValue ? parteaCF : act.TipActProprietate.ParteaCF;
            var tipDreptId = string.IsNullOrEmpty(tipDrept) ? act?.TipActProprietate?.TipDreptId : tipuriDrept.FirstOrDefault(x => string.Equals(tipDrept, x.Denumire, StringComparison.InvariantCultureIgnoreCase))?.Id;
            var tipInscriereId = !string.IsNullOrEmpty(nota) ? tipuriInscriere.FirstOrDefault(x => x.Denumire == "NOTATION").Id : string.IsNullOrEmpty(tipInscriere) ? act.TipActProprietate.TipInscriereId : tipuriInscriere.FirstOrDefault(x => string.Equals(tipInscriere, x.Denumire, StringComparison.InvariantCultureIgnoreCase))?.Id;

            var inscriereD = getInscriereDetaliu();

            if (caz == cazuri.nusparge)
            {
                inscriereD.Cota = cotaGenerala;
            }

            for (var i = 0; i < indecsiProprietari.Count; i++)
            {
                var x = indecsiProprietari[i];
                var proprietar = proprietari.FirstOrDefault(y => y.Index == x.index);
                var inscriereProprietar = new InscriereProprietar()
                {
                    Index = x.index,
                    ExcelRow = excelRow + i,
                    Proprietar = proprietar
                };

                inscriereD.InscrieriProprietari.Add(inscriereProprietar);

                if (caz == cazuri.sparge)
                {
                    inscriereD.Cota = x.cota;
                    inscriereD.ExcelRow = excelRow + i;
                    yield return inscriereD;
                    inscriereD = getInscriereDetaliu();
                }
            }

            InscriereImobil getInscriereImobil() =>
                 new InscriereImobil()
                 {
                     Index = indexParcela,
                     ExcelRow = excelRow,
                     Imobil = parcela?.Imobil
                 };

            InscriereAct getInscriereAct() =>
                new InscriereAct()
                {
                    Index = indexAct,
                    ExcelRow = excelRow,
                    ActProprietate = act
                };

            InscriereDetaliu getInscriereDetaliu(int deltaIndex = 0)
            {
                var id = new InscriereDetaliu()
                {
                    ModDobandireId = modDobandireId,
                    ParteaCF = parteaCFId,
                    TipDreptId = tipDreptId,
                    TipInscriereId = tipInscriereId,
                    Observatii = observatii,
                    Nota = nota,
                    DetaliiDrept = detaliiDrept,
                    Pozitia = pozitia,
                    NumarCerere = numarCerere,
                    DataCerere = dataCerere
                };

                var iImobil = getInscriereImobil();

                id.InscrieriImobile.Add(iImobil);
                id.InscrieriActe.Add(getInscriereAct());
                id.ImobilReferinta = iImobil.Imobil;

                return id;
            }

            //adauga imobil referinta, ExcelRow+indexul din indecsiProp la inscriereD
        }

        public static void FromPOCO(this List<OutputInscriereDetaliu> outputInscrieriD, InscriereDetaliu inscriereD)
        {

            var max = new[] { inscriereD.InscrieriActe.Count, inscriereD.InscrieriImobile.Count, inscriereD.InscrieriProprietari.Count }.Max();
            for (var index = 0; index < max; index++)
            {
                var item = new OutputInscriereDetaliu()
                {
                    CotaParte = inscriereD.Cota,
                    ModDobandire = inscriereD.ModDobandire.Denumire,
                    ParteaCF = inscriereD.ParteaCF,
                    TipDrept = inscriereD.TipDrept.Denumire,
                    TipInscriere = inscriereD.TipInscriere.Denumire,
                    Observatii = inscriereD.Observatii,
                    Nota = inscriereD.Nota,
                    DetaliiDrept = inscriereD.DetaliiDrept,
                    Pozitia = inscriereD.Pozitia,
                    NumarCerere = inscriereD.NumarCerere,
                    DataCerere = inscriereD.DataCerere
                };

                if (inscriereD.InscrieriActe.Count > index)
                {
                    item.IndexAct = inscriereD.InscrieriActe.ElementAt(index).Index;
                }

                if (inscriereD.InscrieriImobile.Count > index)
                {
                    item.IndexParcela = inscriereD.InscrieriImobile.FirstOrDefault().Index;
                }

                if (inscriereD.InscrieriProprietari.Count > index)
                {
                    item.IndexProprietar = inscriereD.InscrieriProprietari.ElementAt(index).Index;
                }

                item.RowIndex = inscriereD.ExcelRow + index;
                outputInscrieriD.Add(item);
            }
        }
    }
}
