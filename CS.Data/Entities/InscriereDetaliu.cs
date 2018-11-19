using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CS.Data.Entities
{
    public class InscriereDetaliu:BaseEntity
    {

        [ForeignKey("ImobilReferinta")]
        public int? ImobilReferintaId
        {
            get;
            set;
        }

        [ForeignKey("TipDrept")]
        public int? TipDreptId
        {
            get;
            set;
        }

        [ForeignKey("ModDobandire")]
        public int? ModDobandireId
        {
            get;
            set;
        }

        [ForeignKey("TipInscriere")]
        public int? TipInscriereId
        {
            get;
            set;
        }

        [MaxLength(2000)]
        public string DetaliiDrept
        {
            get;
            set;
        }
        [MaxLength(4000)]
        public string Nota
        {
            get;
            set;
        }

        [MaxLength(50)]
        public string Cota
        {
            get;
            set;
        }
        [MaxLength(50)]
        public string Moneda
        {
            get;
            set;
        }
        [MaxLength(50)]
        public string Valoarea
        {
            get;
            set;
        }
        [MaxLength(2000)]
        public string Observatii
        {
            get;
            set;
        }
        public int? ParteaCF
        {
            get;
            set;
        }
        public int? Pozitia
        {
            get;
            set;
        }
        public int? NumarCerere
        {
            get;
            set;
        }
        public DateTime? DataCerere
        {
            get;
            set;
        }

        public virtual Imobil ImobilReferinta
        {
            get;
            set;
        }

        public virtual TipDrept TipDrept
        {
            get;
            set;
        }
        public virtual ModDobandire ModDobandire
        {
            get;
            set;
        }

        public virtual TipInscriere TipInscriere
        {
            get;
            set;
        }

        [InverseProperty("InscriereDetaliu")]
        public virtual ICollection<InscriereAct> InscrieriActe { get; set; } = new HashSet<InscriereAct>();

        [InverseProperty("InscriereDetaliu")]
        public virtual ICollection<InscriereImobil> InscrieriImobile { get; set; } = new HashSet<InscriereImobil>();

        [InverseProperty("InscriereDetaliu")]
        public virtual ICollection<InscriereProprietar> InscrieriProprietari { get; set; } = new HashSet<InscriereProprietar>();

    }
}
