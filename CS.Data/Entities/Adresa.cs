using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Caly.Common;

namespace CS.Data.Entities
{
    public class Adresa : BaseEntity, IAdresaFaraLocalitate
    {
        [MaxLength(50)]
        [ForeignKey("UAT")]
        public int? SIRSUP
        {
            get;
            set;
        }
        [ForeignKey("Localitate")]
        [MaxLength(50)]
        public int? SIRUTA
        {
            get;
            set;
        }

        public bool? Intravilan
        {
            get;
            set;
        }

        [MaxLength(50)]
        public string TipDistrict
        {
            get;
            set;
        }

        [MaxLength(50)]
        public string District
        {
            get;
            set;
        }

        [MaxLength(50)]
        public string TipStrada
        {
            get;
            set;
        }

        [MaxLength(50)]
        public string Strada
        {
            get;
            set;
        }

        [MaxLength(50)]
        public string Numar
        {
            get;
            set;
        }

        [MaxLength(50)]
        public string Bloc
        {
            get;
            set;
        }

        [MaxLength(50)]
        public string Scara
        {
            get;
            set;
        }

        [MaxLength(50)]
        public string Etaj
        {
            get;
            set;
        }

        [MaxLength(50)]
        public string Apt
        {
            get;
            set;
        }

        [MaxLength(50)]
        public string CodPostal
        {
            get;
            set;
        }

        [MaxLength(4000)]
        public string Descriere
        {
            get;
            set;
        }

        [MaxLength(100)]
        public string SECTION
        {
            get;
            set;
        }

        public bool? AdresaNecunoscuta
        {
            get;
            set;
        }

        public string AdresaImport
        {
            get;
            set;
        }

        public string JudetImport
        {
            get;
            set;
        }

        public string LocalitateImport
        {
            get;
            set;
        }

        public Localitate Localitate { get; set; }

        public UAT UAT { get; set; }
    }
}
