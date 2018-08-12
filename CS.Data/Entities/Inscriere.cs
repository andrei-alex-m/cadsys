using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//[IdInscriere]
//[int]
//NOT NULL,

//[IdInscriereDetaliu] [int] NULL,
    //[IdTipEntitate] [int] NULL,
    //[IdEntitate] [int] NULL,
    //[] [int] NULL,

namespace CS.Data.Entities
{
    public class Inscriere
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get;
            set;
        }

        [Required]
        public int IdInscriereDetaliu
        {
            get;
            set;
        }
        [ForeignKey("IdInscriereDetaliu")]
        public InscriereDetaliu InscriereDetaliu
        {
            get;
            set;
        }
    }

    public class InscriereAct:Inscriere
    {
        
        [Required]
        public int IdActProprietate
        {
            get;
            set;
        }

        [ForeignKey("IdActProprietate")]
        public ActProprietate ActProprietate
        {
            get;
            set;
        }

    }

    public class InscriereProprietar:Inscriere
    {
        
        [Required]
        public int IdProprietar
        {
            get;
            set;
        }

        [ForeignKey("IdProprietar")]
        public Proprietar Proprietar
        {
            get;
            set;
        }
    }

    public class InscriereImobil:Inscriere
    {
        
        [Required]
        public int IdImobil
        {
            get;
            set;
        }

        [ForeignKey("IdImobil")]
        public Imobil Imobil
        {
            get;
            set;
        }
    }

}
