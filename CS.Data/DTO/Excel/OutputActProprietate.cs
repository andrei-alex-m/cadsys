using System;
namespace CS.Data.DTO.Excel
{
    public class OutputActProprietate:Output
    {

        public int Index
        {
            get;
            set;
        }
        public string TipAct
        {
            get;
            set;
        }
        public string Numar
        {
            get;
            set;
        }
        public DateTime? Data
        {
            get;
            set;
        }
        public string Emitent
        {
            get;
            set;
        }
        public string Carnet
        {
            get;
            set;
        }
    }
}
