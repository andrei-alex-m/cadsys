using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CS.Data.Entities
{
    public class Localitate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id
        {
            get;
            set;
        }

        [Required]
        [ForeignKey("UAT")]
        public int IdUAT
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

        public UAT UAT { get; set; }

    }
}
