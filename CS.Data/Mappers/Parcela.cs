using System;
using CS.Data.Entities;
using CS.Data.DTO.Excel;
using System.Collections.Generic;
using System.Linq;
using Caly.Common;
using System.Collections.Concurrent;

namespace CS.Data.Mappers
{
    public static class ParcelaMapperExtensions
    {
        public static void FromDTO(this Parcela parcela, OutputParcela parcelaDTO, IEnumerable<Tarla> tarlale)
        {
            parcela.Index = parcelaDTO.Index.Value;
            parcela.ExcelRow = parcelaDTO.RowIndex;
            Tarla tarla = string.IsNullOrEmpty(parcelaDTO.Tarla) ? null : tarlale.FirstOrDefault(x => x.Denumire.Trim().ReplaceMultiple('_','.',',','/').Equals(parcelaDTO.Tarla.Trim().ReplaceMultiple('_', '.', ',','/'), StringComparison.InvariantCultureIgnoreCase) );

            parcela.TarlaId = tarla!=null?tarla.Id:(int?)null;
            parcela.Denumire = parcelaDTO.Parcela;
            object catFol;
            if (Enum.TryParse(typeof(CatFol), parcelaDTO.CatFol, true, out catFol))
            {
                parcela.CatFol = (CatFol)catFol;
            }

            parcela.Suprafata = parcelaDTO.Suprafata;

        }

        public static void FromPOCO(this OutputParcela parcelaDTO, Parcela parcela)
        {
            parcelaDTO.RowIndex = parcela.ExcelRow;
            parcelaDTO.Index = parcela.Index;
            parcelaDTO.Tarla = parcela.Tarla?.Denumire;
            parcelaDTO.Parcela = parcela.Denumire;
            parcelaDTO.CatFol = parcela.CatFol.ToString();
            parcelaDTO.Suprafata = parcela.Suprafata;
        }
    }
}
