using System;
using System.Collections.Generic;
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
using Caly.Common;
using System.Linq;

namespace CS.Excel
{
    public static class Exporter
    {
        public static void Export<T>(IRow row, T data, string[] columnNames, ValidationResult validation) where T : Output
        {

        }

        public static XSSFWorkbook CycleProprietari(CadSysContext context, string[] columnNames, string ruleSet)
        {
            var wbk = new XSSFWorkbook();

            var sheet = wbk.CreateSheet("Sheet 1");

            var validator = new ProprietarValidator(context);
            var header = sheet.CreateRow(0);

            for (var i = 0; i < columnNames.Length; i++)
            {
                var cell = header.CreateCell(i);
                cell.SetCellValue(columnNames[i]);
            }

            foreach (var x in context.Proprietari)
            {
                ExportProprietar(sheet, columnNames, x, validator, ruleSet);
            }

            return wbk;
        }

        public static XSSFWorkbook CycleActeProprietate(CadSysContext context, string[] columnNames, string ruleSet)
        {
            var wbk = new XSSFWorkbook();

            var sheet = wbk.CreateSheet("Sheet 1");

            var validator = new ActProprietateValidator(context);
            var header = sheet.CreateRow(0);

            for (var i = 0; i < columnNames.Length; i++)
            {
                var cell = header.CreateCell(i);
                cell.SetCellValue(columnNames[i]);
            }

            foreach (var x in context.ActeProprietate.Include(X=>X.TipAct))
            {
                ExportAct(sheet, columnNames, x, validator, ruleSet);
            }

            return wbk;
        }

        private static void ExportProprietar(ISheet sheet, string[] columnNames, Proprietar proprietar, ProprietarValidator validator, string ruleSet)
        {
            var result = validator.Validate(proprietar, ruleSet: ruleSet);
            var excelDTO = new OutputProprietar();
            excelDTO.FromPOCO(proprietar);

            var row = sheet.CreateRow(excelDTO.RowIndex);

            writeRow(row, columnNames, excelDTO, validator.Validate(proprietar, ruleSet: ruleSet));

        }

        private static void ExportAct(ISheet sheet, string[] columnNames, ActProprietate act, ActProprietateValidator validator, string ruleSet)
        {
            var result = validator.Validate(act, ruleSet: ruleSet);
            var excelDTO = new OutputActProprietate();
            excelDTO.FromPOCO(act);

            var row = sheet.CreateRow(excelDTO.RowIndex);

            writeRow(row, columnNames, excelDTO, validator.Validate(act, ruleSet: ruleSet));
        }

        private static void writeRow(IRow row,  string[] columnNames, object DTO, ValidationResult result)
        {
            var keyValues = new Dictionary<string, string>();
            Reflection.FillDictionaryFromInstance(keyValues, DTO);

            var columnCount = columnNames.Length + 1;

            for (var i = 0; i < columnNames.Length; i++)
            {
                if (keyValues.ContainsKey(columnNames[i]))
                {
                    var cell = row.CreateCell(i, CellType.String);
                    cell.SetCellValue(keyValues[columnNames[i]]);
                }
            }


            result.Errors.GroupBy(y => y.PropertyName).ToList().ForEach(x =>
              {
                  if (!string.IsNullOrEmpty(x.Key))
                  {
                      var propIndex = Array.IndexOf(columnNames, x.Key.ToUpper());
                      if (propIndex < 0 && Reflection.matches.ContainsKey(x.Key.ToUpper()))
                      {
                          propIndex = Array.IndexOf(columnNames, Reflection.matches[x.Key.ToUpper()]);
                      }

                      if (propIndex >= 0)
                      {
                          ICell cell = row.GetCell(propIndex);
                          if (cell == null)
                          {
                              cell = row.CreateCell(propIndex, CellType.String);
                          }

                          var comment = row.Sheet.CreateDrawingPatriarch().CreateCellComment(new XSSFClientAnchor(0, 0, 0, 0, propIndex, row.RowNum, propIndex + 3, row.RowNum + 1));
                          comment.String = new XSSFRichTextString(String.Join("; ", x.Select(y => y.ErrorMessage)));
                          comment.Row = row.RowNum;
                          comment.Column = propIndex;
                      }
                  }
                  else
                  {
                      x.ToList().ForEach(w =>
                      {
                          var cell = row.CreateCell(columnCount);
                          cell.SetCellValue(w.ErrorMessage);
                          columnCount++;
                      });
                  }
              });
        }

        private static void writeComment(ICell cell, string comment)
        {

        }

        public static MemoryStream Cycle<T>(DbSet<T> set, string[] columnNames, AbstractValidator<T> validator, string ruleSet) where T : BaseEntity
        {
            var wbk = new XSSFWorkbook();
            var sheet = wbk.CreateSheet("Sheet 1");

            using (var exportData = new MemoryStream())
            {

                var header = sheet.CreateRow(0);

                for (var i = 0; i < columnNames.Length; i++)
                {
                    var cell = header.CreateCell(i);
                    cell.SetCellValue(columnNames[i]);

                }

                foreach (var x in columnNames)
                {

                }

                foreach (var x in set)
                {
                    var result = validator.Validate(x, ruleSet: ruleSet);
                }

                wbk.Write(exportData);
                return exportData;
            }



        }


    }

}
