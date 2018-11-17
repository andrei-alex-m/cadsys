using System;
namespace CS.Data.DTO.Excel
{
    public class OutputInscriereDetaliu: Output
    {
        public int? IndexParcela
        {
            get;
            set;
        }

        public int? IndexAct
        {
            get;
            set;
        }

        public int? IndexProprietar
        {
            get;
            set;
        }

        public string CotaParte
        {
            get;
            set;
        }

        public string DetaliiDrept
        {
            get;
            set;
        }

        public string Nota
        {
            get;
            set;
        }

        public string ModDobandire
        {
            get;
            set;
        }

        public string TipDrept
        {
            get;
            set;
        }

        public string TipInscriere
        {
            get;
            set;
        }

        public string Moneda//not propagated
        {
            get;
            set;
        }

        public string Valoarea//not propagated
        {
            get;
            set;
        }

        public string Observatii
        {
            get;
            set;
        }
        public int? ParteaCF
        {
            get;
            set;
        }
        public int? Pozitia
        {
            get;
            set;
        }
        public int? NumarCerere
        {
            get;
            set;
        }
        public DateTime? DataCerere
        {
            get;
            set;
        }
    }
}
