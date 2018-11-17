using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CS.Data.Entities
{
    public class Inscriere:BaseEntity
    {
        [Required]
        [ForeignKey("InscriereDetaliu")]
        public int InscriereDetaliuId
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
        public int? ActProprietateId
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
        public int? ProprietarId
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
        public int? ImobilId
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
