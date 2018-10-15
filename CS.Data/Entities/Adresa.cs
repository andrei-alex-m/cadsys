using System;
using System.ComponentModel.DataAnnotations;

namespace CS.Data.Entities
{
    public class Adresa : BaseEntity
    {
        [MaxLength(50)]
        public int? SIRSUP
        {
            get;
            set;
        }

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

        public string Concat
        {
            get;
            set;
        }
    }
}
