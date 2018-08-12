using System;
using System.Collections.Generic;

namespace CS.Data.Entities
{
    public class Proprietar : BaseEntity
    {
        public string Nume
        {
            get;
            set;
        }
        public string Initiala
        {
            get;
            set;
        }
        public string Prenume
        {
            get;
            set;
        }
        public TipActIdentitate? TipActIdentitate
        {
            get;
            set;
        }
        public string Serie
        {
            get;
            set;
        }
        public string Numar
        {
            get;
            set;
        }
        public long Identificator
        {
            get;
            set;
        }
        public string Emitent
        {
            get;
            set;
        }

        public DateTime? DataEmiterii
        {
            get;
            set;
        }

        public string Adresa
        {
            get;
            set;
        }
        public string Localitate
        {
            get;
            set;
        }


        public string Judet
        {
            get;
            set;
        }
        public string Tara
        {
            get;
            set;
        }
        public TipPersoana TipPersoana
        {
            get;
            set;
        }
        public Sex? Sex
        {
            get;
            set;
        }
    }
}
