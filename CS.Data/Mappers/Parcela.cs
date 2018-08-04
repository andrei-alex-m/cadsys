using System;
using CS.Data.Entities;
using CS.Data.DTO.Excel;
using System.Collections.Generic;
using System.Linq;

namespace CS.Data.Mappers
{
    public static class ParcelaMapperExtensions
    {
        public static void FromDTO(this Parcela parcela, OutputParcela parcelaDTO, List<Tarla> tarlale)
        {
            parcela.Index = parcelaDTO.Index;
            parcela.ExcelRow = parcelaDTO.RowIndex;

            parcela.TarlaId = tarlale.FirstOrDefault(x => x.Denumire == parcelaDTO.Tarla).Id;
            parcela.Denumire = parcelaDTO.Parcela;
            object catFol;
            if (Enum.TryParse(typeof(CatFol), parcelaDTO.CatFol, true, out catFol))
            {
                parcela.CatFol = (CatFol)catFol;
            }

            parcela.CatFol = string.IsNullOrEmpty(parcelaDTO.CatFol) ?
                (CatFol)Enum.Parse(typeof(CatFol), parcelaDTO.CatFol)
                : (CatFol?)null;

        }
    }
}
