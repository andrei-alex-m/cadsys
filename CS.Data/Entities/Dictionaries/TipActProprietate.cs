using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CS.Data.Entities
{
    public class TipActProprietate:BaseDictionary
    {
        [InverseProperty("TipActProprietate")]
        public virtual ICollection<ActProprietate> ActeProprietate { get; set; } = new HashSet<ActProprietate>();

        [ForeignKey("TipDocument")]
        public int? TipDocumentId
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

        public short? ParteaCF
        {
            get;
            set;
        }

        public virtual TipDocument TipDocument
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


    }



}
