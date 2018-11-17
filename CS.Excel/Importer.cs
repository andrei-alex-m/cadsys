
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using CS.Data.DTO.Excel;
using CS.Services.Interfaces;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace CS.Excel
{
    public static class Importer
    {
        public static Task<ConcurrentBag<T>> GetDTOs<T>(MemoryStream stream, string fileName, ImportConfig config, IExcelConfigurationRepo excelConfig) where T:Output, new()
        {
            return Task.Run(() =>
            {
                var result = new ConcurrentBag<T>();

                stream.Position = 0;

                IWorkbook wbk;

                if (fileName.EndsWith(".xls", StringComparison.InvariantCultureIgnoreCase))
                {
                    wbk = new HSSFWorkbook(stream); //This will read 2007 Excel format  

                }
                else if (fileName.EndsWith(".xlsx", StringComparison.InvariantCultureIgnoreCase))
                {
                    wbk = new XSSFWorkbook(stream);
                }
                else
                {
                    throw new Exception("This format is not supported");
                }

                var sheet = wbk.GetSheetAt(0); //get first sheet from workbook 

                IRow headerRow = sheet.GetRow(0); //Get Header Row 

                var columnNames = Utils.GetColumnNames(headerRow);

                excelConfig.Save(1, typeof(T).Name, fileName, columnNames);

                var firstRow = sheet.FirstRowNum;
                var lastRow = sheet.LastRowNum;

                for (int i = (firstRow + 1); i <= lastRow; i++) //Read Excel File
                {
                    var row = sheet.GetRow(i);

                    if (row == null && config.IgnoreNullRows) continue;
                    try
                    {
                        if (row.All(x => x == null || string.IsNullOrEmpty(x.ToString()))) continue;
                    }
                    catch
                    {
                        System.Diagnostics.Debugger.Break();
                    }
                    result.Add(GetDTO<T>(row, columnNames.ToList(), i));

                }

                return result;
            });
        }

        public static Task<List<List<T>>> GetGroupedDTOs<T>(MemoryStream stream, string fileName, ImportConfig config, IExcelConfigurationRepo excelConfig) where T : Output, new()
        {
            return Task.Run(() =>
            {
                stream.Position = 0;

                IWorkbook wbk;

                if (fileName.EndsWith(".xls", StringComparison.InvariantCultureIgnoreCase))
                {
                    wbk = new HSSFWorkbook(stream); //This will read 2007 Excel format  

                }
                else if (fileName.EndsWith(".xlsx", StringComparison.InvariantCultureIgnoreCase))
                {
                    wbk = new XSSFWorkbook(stream);
                }
                else
                {
                    throw new Exception("This format is not supported");
                }

                var sheet = wbk.GetSheetAt(0); //get first sheet from workbook 

                IRow headerRow = sheet.GetRow(0); //Get Header Row 

                var columnNames = Utils.GetColumnNames(headerRow);

                excelConfig.Save(1, typeof(T).Name, fileName, columnNames);

                var result = new List<List<T>>();

                var miniResult = new List<T>();

                var firstRow = sheet.FirstRowNum;
                var lastRow = sheet.LastRowNum;

                for (int i = (firstRow + 1); i <= lastRow; i++) //Read Excel File
                {
                    var row = sheet.GetRow(i);

                    try
                    {
                        if (miniResult.Count > 0 && (row == null || row.All(x => x.CellType == CellType.Blank)))
                        {
                            result.Add(miniResult);
                            miniResult = new List<T>();
                        }
                        else
                        {
                            miniResult.Add(GetDTO<T>(row, columnNames.ToList(), i));
                        }
                    }
                    catch(Exception ex)
                    {
                        System.Diagnostics.Debugger.Break();
                    }
                }

                if (miniResult.Count>0) result.Add(miniResult);

                return result;
            });
        }

        private static T GetDTO<T>(IRow row, List<string> columnNames, int rowIndex = 0) where T : Output, new()
        {
            var result = new T
            {
                RowIndex = rowIndex
            };
            if (row == null) return result;

            var kvp = new Dictionary<string, string>();


            for (var i = 0; i < columnNames.Count(); i++)
            {
                var cell = row.GetCell(i);

                if (cell == null) continue;

                string value = cell.ToString();

                if (cell.CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(cell))
                {
                    value = cell.DateCellValue.ToString("dd/MM/yyyy");
                }

                kvp.Add(columnNames[i], value);
            }

            Caly.Common.Reflection.FillInstanceFromDictionary(kvp, result, true);
            return result;
        }

    }

    public class ImportConfig
    {
        public bool IgnoreNullRows
        {
            get;
            set;
        } = true;

        public bool UppercaseColumnsCompare
        {
            get;
            set;
        } = true;
    }
}
