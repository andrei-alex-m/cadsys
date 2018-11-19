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

        public virtual InscriereDetaliu InscriereDetaliu
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

        public virtual ActProprietate ActProprietate
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

        public virtual Proprietar Proprietar
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

        public virtual Imobil Imobil
        {
            get;
            set;
        }
    }

}
