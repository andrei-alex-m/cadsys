using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CS.Data.Entities
{
    public class Parcela:BaseEntity
    {
        [Required]
        public CatFol? CatFol
        {
            get;
            set;
        }

        [Required]
        public string Denumire
        {
            get;
            set;
        }

        [Required]
        public int Suprafata
        {
            get;
            set;
        } 

        [ForeignKey("Tarla")]
        public int? IdTarla { get; set; }

        public Tarla Tarla { get; set; }

        [ForeignKey("Imobil")]
        public int? IdImobil { get; set; }

        public Imobil Imobil { get; set; }

    }
}
