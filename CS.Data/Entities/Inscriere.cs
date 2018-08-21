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
    public class Inscriere:BaseEntity
    {
        [Required]
        [ForeignKey("InscriereDetaliu")]
        public int IdInscriereDetaliu
        {
            get;
            set;
        }

        public InscriereDetaliu InscriereDetaliu
        {
            get;
            set;
        }
    }

    public class InscriereAct:Inscriere
    {
        
        [ForeignKey("ActProprietate")]
        public int? IdActProprietate
        {
            get;
            set;
        }

        public ActProprietate ActProprietate
        {
            get;
            set;
        }

    }

    public class InscriereProprietar:Inscriere
    {
        
        [ForeignKey("Proprietar")]
        public int? IdProprietar
        {
            get;
            set;
        }

        public Proprietar Proprietar
        {
            get;
            set;
        }
    }

    public class InscriereImobil:Inscriere
    {
        
        [ForeignKey("Imobil")]
        public int? IdImobil
        {
            get;
            set;
        }


        public Imobil Imobil
        {
            get;
            set;
        }
    }

}
