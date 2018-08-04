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
