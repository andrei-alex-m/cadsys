﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CS.Data.Entities
{
    public class Imobil
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get;
            set;
        }

        [ForeignKey("Adresa")]
        public int AdresaId { get; set; } = 1;


        [MaxLength(200)]
        public String SectorCadastral
        {
            get;
            set;
        }
        public decimal? SuprafataMasurata
        {
            get;
            set;
        }
        public decimal? SuprafataDinActe
        {
            get;
            set;
        }
        public bool ImobilNou
        {
            get;
            set;
        }
        public decimal SuprafataDinActeConstructii
        {
            get;
            set;
        }
        public decimal ValoareImpozitare
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

        [MaxLength(200)]
        public string IdentificatorElectronic
        {
            get;
            set;
        }

        [MaxLength(200)]
        public string NumarCadastral
        {
            get;
            set;
        }
        [MaxLength(200)]
        public string NumarCarteFunciara
        {
            get;
            set;
        }

        [MaxLength(200)]
        public string NumarTopografic
        {
            get;
            set;
        }

        [MaxLength(200)]
        public string NumarCadGeneral
        {
            get;
            set;
        }

        public Adresa Adresa
        {
            get;
            set;
        }

        [InverseProperty("ImobilReferinta")]
        public virtual ICollection<InscriereDetaliu> InscrieriDetaliu { get; set; } = new HashSet<InscriereDetaliu>();

        [InverseProperty("Imobil")]
        public virtual ICollection<InscriereImobil> Inscrieri { get; set; } = new HashSet<InscriereImobil>();

        [InverseProperty("Imobil")]
        public virtual ICollection<Parcela> Parcele { get; set; } = new HashSet<Parcela>();

    }
}
