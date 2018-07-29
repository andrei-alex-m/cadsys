using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CS.Data.Entities
{
    public class ActProprietate:BaseEntity
    {

        [ForeignKey("TipActProprietateId")]
        public TipActProprietate TipActProprietate { get; set; }
        [Required]
        public int TipActProprietateId { get; set; }

        [Required]
        public string Numar
        {
            get;
            set;
        }

        public DateTime Data
        {
            get;
            set;
        }

        public string Emitent
        {
            get;
            set;
        }

        public decimal Suprafata
        {
            get;
            set;
        }

        public string Carnet
        {
            get;
            set;
        }

        public ICollection<Inscriere> Inscrieri { get; set; }
    }
}
