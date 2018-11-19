using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CS.Data.Entities
{
    public class UAT
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id
        {
            get;
            set;
        }

        [Required]
        [ForeignKey("Judet")]
        public int JudetId
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
        public int SIRUTA
        {
            get;
            set;
        }

        [Required]
        public bool Activ
        {
            get;
            set;
        }

        public virtual Judet Judet { get; set; }

        [InverseProperty("UAT")]
        public virtual ICollection<Localitate> Localitati { get; set; } = new HashSet<Localitate>();
    }
}
