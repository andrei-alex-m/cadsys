using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CS.Data.Entities.Dictionaries
{
    public class UAT:BaseDictionary
    {
        [ForeignKey("JudetId")]
        public Judet Judet { get; set; }
        [Required]
        public int JudetId { get; set; }
    }
}
