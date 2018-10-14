using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CS.Data.Entities
{
    public class Tarla:BaseDictionary
    {
        [Required]
        [ForeignKey("Admin")]
        public int AdminId { get; set; }

        public UAT Admin { get; set; }

        [InverseProperty("Tarla")]
        public virtual ICollection<Parcela> Parcele { get; set; } = new HashSet<Parcela>();

    }
}
