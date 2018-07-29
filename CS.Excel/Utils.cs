using System;
using System.Collections.Generic;
using NPOI.SS.UserModel;

namespace CS.Excel
{
    public static class Utils
    {
        public static IEnumerable<string> GetColumnNames(IRow row)
        {
            int cellCount = row.LastCellNum;
            for (var i = 0; i < cellCount; i++)
            {
                yield return row.GetCell(i).ToString().ToUpper();
            }
        }
    }
}
