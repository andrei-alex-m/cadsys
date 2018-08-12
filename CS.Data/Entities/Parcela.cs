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

        [ForeignKey("TarlaId")]
        public Tara Tarla { get; set; }
        [Required]
        public int TarlaId { get; set; }

    }
}
