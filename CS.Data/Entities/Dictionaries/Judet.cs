using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CS.Data.Entities
{
    public class Judet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id
        {
            get;
            set;
        }

        [Required]
        public string Cod
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

        [InverseProperty("Judet")]
        public virtual ICollection<UAT> UATs { get; set; } = new HashSet<UAT>();
    }
}
