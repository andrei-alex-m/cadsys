using System;
using Caly.Common;

namespace CS.Data.DTO.DXF
{
    public class Poly
    {
        Point[] Coordonate
        {
            get;
            set;
        }

        public string NrCadGeneral
        {
            get;
            set;
        }

        public string Sector
        {
            get;
            set;
        }

        public decimal Suprafata
        {
            get;
            set;
        }

    }
}
