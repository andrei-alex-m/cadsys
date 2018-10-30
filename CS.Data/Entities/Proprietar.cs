using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CS.Data.Entities
{
    public class Proprietar : BaseEntity
    {
        public Proprietar()
        {
            Adresa = new Adresa();
        }

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
        public long? Identificator
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

        //public string Localitate
        //{
        //    get;
        //    set;
        //}

        //public string Judet
        //{
        //    get;
        //    set;
        //}
        public string Tara
        {
            get;
            set;
        }
        public TipPersoana? TipPersoana
        {
            get;
            set;
        }
        public Sex? Sex
        {
            get;
            set;
        }

        [ForeignKey("Adresa")]
        public int? IdAdresa { get; set; }

        public Adresa Adresa { get; set; }

        [InverseProperty("Proprietar")]
        public virtual ICollection<InscriereProprietar> Inscrieri { get; set; } = new HashSet<InscriereProprietar>();

    }
}
