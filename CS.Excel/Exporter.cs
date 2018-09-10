using System;
using System.IO;
using System.Threading.Tasks;
using CS.Data.DTO.Excel;
using CS.Data.Entities;
using CS.Data.Mappers;
using CS.EF;
using CS.EF.EntitiesValidators;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace CS.Excel
{
    public static class Exporter
    {
        public static void Export<T>(IRow row, T data, string[] columnNames, ValidationResult validation) where T : Output
        {

        }

        //public static MemoryStream CycleProprietari(DbSet<Proprietar> proprietari, string[] columnNames, string fileName)
        //{
        //    var wbk = new XSSFWorkbook();
        //    var sheet = wbk.CreateSheet("Sheet 1");

        //    Parallel.ForEach(proprietari, x =>
        //    {
        //        var propDTO = new OutputProprietar();
        //        propDTO.FromPOCO(x);
        //        var row = sheet.CreateRow(x.ExcelRow);
        //        //Export<OutputProprietar>(row, propDTO, columnNames, new ProprietarValidator()).Validate();

        //    });
        //}

        public static MemoryStream Cycle<T>(DbSet<T> set, AbstractValidator<T> validator, string ruleSet) where T:BaseEntity
        {
            var wbk = new XSSFWorkbook();
            var sheet = wbk.CreateSheet("Sheet 1");

            using (var exportData = new MemoryStream())
            {
                wbk.Write(exportData);
                return exportData;
            }

        }


    }

}
