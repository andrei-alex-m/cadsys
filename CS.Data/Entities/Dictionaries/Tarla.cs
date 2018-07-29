using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CS.Data.Entities.Dictionaries;

namespace CS.Data.Entities
{
    public class Tarla:BaseDictionary
    {
        [ForeignKey("UATId")]
        public UAT UAT { get; set; }
        [Required]
        public int UATId { get; set; }

        public ICollection<Parcela> Parcele { get; set; }

    }
}
