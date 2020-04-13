using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CS.Data.Entities
{
    public class Tarla:BaseDictionary
    {
        [Required]
        [ForeignKey("UAT")]
        public int UATId { get; set; }

        public bool Diminuata { get; set; }

        public string Sector { get; set; }

        public virtual UAT UAT { get; set; }

        [InverseProperty("Tarla")]
        public virtual ICollection<Parcela> Parcele { get; set; } = new HashSet<Parcela>();

    }
}
