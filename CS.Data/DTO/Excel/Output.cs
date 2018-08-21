 using System;
namespace CS.Data.DTO.Excel
{
    public abstract class Output
    {
        public int RowIndex
        {
            get;
            set;
        }

        public int? Index
        {
            get;
            set;
        }
    }
}
