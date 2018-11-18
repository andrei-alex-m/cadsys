using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Caly.Common;
using CS.Data.DTO.Excel;
using CS.Data.Entities;
using CS.Data.Mappers;
using CS.EF;
using CS.EF.EntitiesValidators;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace CS.Excel
{
    public static class Exporter
    {

        public static HSSFWorkbook CycleProprietari(CadSysContext context, string[] columnNames, string ruleSet)
        {
            var wbk = new HSSFWorkbook();

            var sheet = wbk.CreateSheet("Sheet 1");

            var validatorP = new ProprietarValidator(context);
            var validatorA = new AdresaValidator();
            var header = sheet.CreateRow(0);

            for (var i = 0; i < columnNames.Length; i++)
            {
                var cell = header.CreateCell(i);
                cell.SetCellValue(columnNames[i]);
            }

            foreach (var x in context.Proprietari.Include(x => x.Adresa).ThenInclude(y => y.Localitate).ThenInclude(z => z.UAT).ThenInclude(w => w.Judet))
            {
                ExportProprietar(sheet, columnNames, x, validatorP, validatorA, ruleSet);
            }

            return wbk;
        }

        public static HSSFWorkbook CycleActeProprietate(CadSysContext context, string[] columnNames, string ruleSet)
        {
            var wbk = new HSSFWorkbook();

            var sheet = wbk.CreateSheet("Sheet 1");

            var validator = new ActProprietateValidator(context);
            var header = sheet.CreateRow(0);

            for (var i = 0; i < columnNames.Length; i++)
            {
                var cell = header.CreateCell(i);
                cell.SetCellValue(columnNames[i]);
            }

            foreach (var x in context.ActeProprietate.Include(y => y.TipActProprietate))
            {
                ExportAct(sheet, columnNames, x, validator, ruleSet);
            }

            return wbk;
        }

        public static HSSFWorkbook CycleParcele(CadSysContext context, string[] columnNames, string ruleSet)
        {
            var wbk = new HSSFWorkbook();

            var sheet = wbk.CreateSheet("Sheet 1");

            var validator = new ParcelaValidator(context);
            var header = sheet.CreateRow(0);

            for (var i = 0; i < columnNames.Length; i++)
            {
                var cell = header.CreateCell(i);
                cell.SetCellValue(columnNames[i]);
            }

            foreach (var x in context.Parcele.ToList())
            {
                ExportParcela(sheet, columnNames, x, validator, ruleSet);
            }

            return wbk;
        }

        public static HSSFWorkbook CycleInscrieri(CadSysContext context, string[] columnNames, string ruleSet)
        {
            var wbk = new HSSFWorkbook();

            var sheet = wbk.CreateSheet("Sheet 1");

            var validatorInscriereDetaliu = new InscriereDetaliuValidator();

            var validatorInscriereAct = new InscriereActValidator();

            var validatorInscriereProprietar = new InscriereProprietarValidator();

            var validatorInscriereImobil = new InscriereImobilValidator();

            var header = sheet.CreateRow(0);

            for (var i = 0; i < columnNames.Length; i++)
            {
                var cell = header.CreateCell(i);
                cell.SetCellValue(columnNames[i]);
            }

            foreach (var x in context.InscrieriDetaliu
                     .Include(y => y.InscrieriActe)
                        .ThenInclude(z => z.ActProprietate)
                     .Include(y => y.InscrieriImobile)
                        .ThenInclude(z => z.Imobil)
                            .ThenInclude(i => i.Parcele)
                     .Include(y => y.InscrieriProprietari)
                        .ThenInclude(z => z.Proprietar))
            {
                ExportInscrieri(sheet, columnNames, x, validatorInscriereDetaliu, validatorInscriereAct, validatorInscriereImobil, validatorInscriereProprietar, ruleSet);

            }

            return wbk;
        }

        private static void ExportProprietar(ISheet sheet, string[] columnNames, Proprietar proprietar, ProprietarValidator validatorP, AdresaValidator validatorA, string ruleSet)
        {
            var resultP = validatorP.Validate(proprietar, ruleSet: ruleSet);
            var resultA = validatorA.Validate(proprietar.Adresa, ruleSet: ruleSet);
            var excelDTO = new OutputProprietarAdresa();
            excelDTO.FromPOCO(proprietar);
            excelDTO.FromPOCO(proprietar.Adresa);

            var row = sheet.CreateRow(excelDTO.RowIndex);

            writeRow(row, columnNames, excelDTO, false, resultP, resultA);


        }

        static void ExportAct(ISheet sheet, string[] columnNames, ActProprietate act, ActProprietateValidator validator, string ruleSet)
        {
            var result = validator.Validate(act, ruleSet: ruleSet);
            var excelDTO = new OutputActProprietate();
            excelDTO.FromPOCO(act);

            var row = sheet.CreateRow(excelDTO.RowIndex);

            writeRow(row, columnNames, excelDTO, false, validator.Validate(act, ruleSet: ruleSet));
        }

        static void ExportParcela(ISheet sheet, string[] columnNames, Parcela parcela, ParcelaValidator validator, string ruleSet)
        {
            var result = validator.Validate(parcela, ruleSet: ruleSet);
            var excelDTO = new OutputParcela();
            excelDTO.FromPOCO(parcela);


            var row = sheet.CreateRow(excelDTO.RowIndex);


            writeRow(row, columnNames, excelDTO, false, validator.Validate(parcela, ruleSet: ruleSet));
        }

        static void ExportInscrieri(ISheet sheet, string[] columnNames, InscriereDetaliu inscriereD, InscriereDetaliuValidator iDValidator, InscriereActValidator iActValidator, InscriereImobilValidator iImobilValidator, InscriereProprietarValidator iPropValidator, string ruleSet)
        {
            var xPorts = new List<OutputInscriereDetaliu>();
            xPorts.FromPOCO(inscriereD);

            xPorts.ForEach(excelDTO =>
            {
                var row = sheet.CreateRow(excelDTO.RowIndex);

                if (excelDTO.IndexAct.HasValue)
                {
                    var inscriereAct = inscriereD.InscrieriActe.FirstOrDefault(y => y.Index == excelDTO.IndexAct.Value);
                    if (inscriereAct != null)
                    {
                        writeRow(row, columnNames, excelDTO, true, iActValidator.Validate(inscriereAct, ruleSet: ruleSet));
                    }
                }

                if (excelDTO.IndexParcela.HasValue)
                {
                    var inscriereParcela = inscriereD.InscrieriImobile.FirstOrDefault(y => y.Index == excelDTO.IndexParcela.Value);
                    if (inscriereParcela != null)
                    {
                        writeRow(row, columnNames, excelDTO, true, iImobilValidator.Validate(inscriereParcela, ruleSet: ruleSet));
                    }
                }

                if (excelDTO.IndexProprietar.HasValue)
                {
                    var inscriereProprietar = inscriereD.InscrieriProprietari.FirstOrDefault(y => y.Index == excelDTO.IndexProprietar.Value);
                    if (inscriereProprietar != null)
                    {
                        writeRow(row, columnNames, excelDTO, true, iPropValidator.Validate(inscriereProprietar, ruleSet: ruleSet));
                    }
                }
            });

            //validarea de Inscriere Detaliu

            var anotherRow = sheet.GetRow(inscriereD.ExcelRow) ?? sheet.CreateRow(inscriereD.ExcelRow);

            writeRow(anotherRow, columnNames, null, false, iDValidator.Validate(inscriereD, ruleSet: ruleSet));


            //lista de outputinscriereD trecuta printr-un writerow special care sa ia inscrierile de la fiecare dintre cei 3 indecsi, sa le valideze obiectele si sa le scrie in icselß
        }


        static void writeRow(IRow row, string[] columnNames, object DTO, bool skipMatching = false, params ValidationResult[] result)
        {
            var keyValues = new Dictionary<string, string>();
            Reflection.FillDictionaryFromInstance(keyValues, DTO, skipMatching: skipMatching);

            var columnCount = columnNames.Length + 1;

            for (var i = 0; i < columnNames.Length; i++)
            {
                if (keyValues.ContainsKey(columnNames[i]))
                {
                    var cell = row.CreateCell(i, CellType.String);
                    cell.SetCellValue(keyValues[columnNames[i]]);
                }
            }


            result.SelectMany(x => x.Errors).GroupBy(y => y.PropertyName).ToList().ForEach(x =>
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
                            var comment = row.Sheet.CreateDrawingPatriarch().CreateCellComment(new HSSFClientAnchor(0, 0, 0, 0, propIndex, row.RowNum, propIndex + 3, row.RowNum + 2));
                            comment.String = new HSSFRichTextString(String.Join("; ", x.Select(y => y.ErrorMessage)));
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

        public static MemoryStream Cycle<T>(DbSet<T> set, string[] columnNames, AbstractValidator<T> validator, string ruleSet) where T : BaseEntity
        {
            var wbk = new XSSFWorkbook();
            var sheet = wbk.CreateSheet("Sheet 1");

            var exportData = new MemoryStream();

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
