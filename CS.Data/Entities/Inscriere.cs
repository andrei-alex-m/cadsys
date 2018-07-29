using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CS.Data.Entities
{
    public class Inscriere: BaseEntity
    {
        [Required]
        public int IdProprietar
        { get; set; }
        [ForeignKey("IdProprietar")]
        public Proprietar Proprietar
        { get; set; }

        [Required]
        public int IdActProprietate
        { get; set; }
        [ForeignKey("IdActProprietate")]
        public ActProprietate ActProprietate
        { get; set; }

        [Required]
        public int IdParcela
        { get; set; }
        [ForeignKey("IdParcela")]
        public  Parcela Parcela
        { get; set; }

    }

}
