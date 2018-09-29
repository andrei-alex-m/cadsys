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
        public static void FromDTO(this InscriereDetaliu inscriereD, List<OutputInscriereDetaliu> outputInscriereD, DbSet<Proprietar> proprietari, DbSet<ActProprietate> acte, DbSet<Parcela> parcele)
        {
            inscriereD.ExcelRow = outputInscriereD.Min(x => x.RowIndex);
                      
            var indecsiParcela = outputInscriereD.Where(x => x.IndexParcela.HasValue).Select(x => x.IndexParcela.Value).Distinct().ToList();

            var indecsiActe = outputInscriereD.Where(x => x.IndexAct.HasValue).Select(x => x.IndexAct.Value).Distinct().ToList();

            var indecsiProprietari = outputInscriereD.Where(x => x.IndexProprietar.HasValue).Select(x => x.IndexProprietar.Value).Distinct().ToList();

            for (var i = 0; i < indecsiParcela.Count; i++)
            {
                var x = indecsiParcela[i];
                var parcela = parcele.FirstOrDefault(y => y.Index == x);

                var inscriereImobil = new InscriereImobil()
                {
                    Index = x,
                    ExcelRow = inscriereD.ExcelRow+i
                };

                if (parcela != null)
                {
                    var imobil = new Imobil();
                    imobil.Parcele.Add(parcela);

                    inscriereImobil.Imobil = imobil;
                    inscriereD.ImobilReferinta = imobil;
                }

                inscriereD.InscrieriImobile.Add(inscriereImobil);
            }

            for (var i = 0; i < indecsiActe.Count; i++)
            {
                var x = indecsiActe[i];
                var inscriereAct = new InscriereAct
                {
                    Index = x,
                    ExcelRow=inscriereD.ExcelRow+i
                };

                var act = acte.FirstOrDefault(y => y.Index == x);

                if (act != null)
                {
                    inscriereAct.ActProprietate = act;
                }

                inscriereD.InscrieriActe.Add(inscriereAct);
            }

            for (var i = 0; i < indecsiProprietari.Count; i++)
            {
                var x = indecsiProprietari[i];
                var proprietar = proprietari.FirstOrDefault(y => y.Index == x);
                var inscriereProprietar = new InscriereProprietar()
                {
                    Index = x,
                    ExcelRow=inscriereD.ExcelRow+i
                };

                if (proprietar != null)
                {
                    inscriereProprietar.Proprietar = proprietar;
                }

                inscriereD.InscrieriProprietari.Add(inscriereProprietar);
            }
        }

        public static void FromPOCO(this List<OutputInscriereDetaliu> outputInscrieriD, InscriereDetaliu inscriereD)
        {

            var max = new[] { inscriereD.InscrieriActe.Count, inscriereD.InscrieriImobile.Count, inscriereD.InscrieriProprietari.Count }.Max();
            for (var index = 0; index < max; index++)
            {
                var item = new OutputInscriereDetaliu();

                if (inscriereD.InscrieriActe.Count>index)
                {
                    item.IndexAct = inscriereD.InscrieriActe.ElementAt(index).Index;
                }

                if (inscriereD.InscrieriImobile.Count>index)
                {
                    item.IndexParcela = inscriereD.InscrieriImobile.FirstOrDefault().Index;
                }

                if (inscriereD.InscrieriProprietari.Count>index)
                {
                    item.IndexProprietar = inscriereD.InscrieriProprietari.ElementAt(index).Index;
                }
                item.RowIndex = inscriereD.ExcelRow + index;
                outputInscrieriD.Add(item);
            }
        }
    }
}
