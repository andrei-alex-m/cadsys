using System;
using System.Collections.Generic;
using System.Linq;
using NPOI.SS.UserModel;

namespace CS.Excel
{
    public static class Utils
    {
        public static string[] GetColumnNames(IRow row)
        {
            return row.Cells.Select(x => x.ToString().ToUpper()).ToArray();
        }
    }
}
