using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CS.Data.Entities
{
    public class InscriereDetaliu:BaseEntity
    {
        public string TipInscriere
        {
            get;
            set;
        }
        public int IdTipDrept
        {
            get;
            set;
        }
        [MaxLength(2000)]
        public string ObservatiiDrept
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
        public string ModDobandire
        {
            get;
            set;
        }
        [MaxLength(50)]
        public string TipCota
        {
            get;
            set;
        }
        [MaxLength(50)]
        public string CotaInitiala
        {
            get;
            set;
        }
        [MaxLength(50)]
        public string CotaActuala
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
        public int ParteaCartiiFunciare
        {
            get;
            set;
        }
        public int Pozitia
        {
            get;
            set;
        }
        public int NumarulCererii
        {
            get;
            set;
        }
        public DateTime DataCererii
        {
            get;
            set;
        }

        [ForeignKey("ImobilReferinta")]
        public int? ImobilReferintaId
        {
            get;
            set;
        }

        public Imobil ImobilReferinta
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
