using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CS.Data.Entities
{
    public class TipActProprietate:BaseDictionary
    {
        [InverseProperty("TipAct")]
        public virtual ICollection<ActProprietate> ActeProprietate { get; set; } = new HashSet<ActProprietate>();

        public TipDocument MyProperty
        {
            get;
            set;
        }

    }



}
