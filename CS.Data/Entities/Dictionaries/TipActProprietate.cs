using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CS.Data.Entities
{
    public class TipActProprietate:BaseDictionary
    {
        public ICollection<ActProprietate> ActeProprietate { get; set; }
    }
}
