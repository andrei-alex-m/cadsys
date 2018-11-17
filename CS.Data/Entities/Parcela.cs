using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CS.Data.Entities
{
    public class Parcela:BaseEntity
    {
        public CatFol? CatFol
        {
            get;
            set;
        }

        public string Denumire
        {
            get;
            set;
        }

        public int? Suprafata
        {
            get;
            set;
        }

        public string NumarTitlu
        {
            get;
            set;
        }

        [ForeignKey("Tarla")]
        public int? TarlaId { get; set; }

        public Tarla Tarla { get; set; }

        [ForeignKey("Imobil")]
        public int? ImobilId { get; set; }

        public Imobil Imobil { get; set; }

    }
}
