using System;
using System.IO;
using System.Threading.Tasks;
using CS.Data.DTO.Excel;
using CS.Data.Entities;
using CS.Data.Mappers;
using CS.EF;
using CS.EF.EntitiesValidators;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace CS.Excel
{
    public static class Exporter
    {
        public static void Export<T>(IRow row, T data, string[] columnNames, ValidationResult validation) where T:Output
        {

        }

        public static MemoryStream CycleProprietari(DbSet<Proprietar> proprietari, string[] columnNames, string fileName)
        {
            var wbk = new XSSFWorkbook();
            var sheet = wbk.CreateSheet("Sheet 1");

            Parallel.ForEach(proprietari, x =>
            {
                var propDTO = new OutputProprietar();
                propDTO.FromPOCO(x);
                var row = sheet.CreateRow(x.ExcelRow);
                Export<OutputProprietar>(row, propDTO,columnNames, new ProprietarValidator()).Validate( )

            });

        }

    }
}
