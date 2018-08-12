using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CS.Data.Entities
{
//    [IdInscriereDetaliu]
//    [int]
//    NOT NULL,

//[TipInscriere] [text] NULL,
    //[IdTipDrept] [int] NULL,
    //[ObservatiiDrept] [varchar] (2000) NULL,
    //[Nota] [varchar] (4000) NULL,
    //[ModDobandire] [varchar] (50) NULL,
    //[TipCota] [varchar] (50) NULL,
    //[CotaInitiala] [varchar] (50) NULL,
    //[CotaActuala] [varchar] (50) NULL,
    //[Moneda] [varchar] (50) NULL,
    //[Valoarea] [varchar] (50) NULL,
    //[Observatii] [varchar] (2000) NULL,
    //[ParteaCartiiFunciare] [int] NULL,
    //[Pozitia] [int] NULL,
    //[NumarulCererii] [int] NULL,
    //[DataCererii] [datetime] NULL,
    //[] [int] NULL,
    //[Concat]
    //[nvarchar]
    //(max) NULL,

    public class InscriereDetaliu
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get;
            set;
        }
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

        [Required]
        public int IdImobilReferinta
        {
            get;
            set;
        }
        [ForeignKey("IdForerinta")]
        public Imobil ImobilReferinta
        {
            get;
            set;
        }
    }
}
