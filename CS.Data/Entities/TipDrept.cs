﻿using System;
namespace CS.Data.Entities
{
    public class TipDrept:BaseDictionary
    {
        public bool Partea2
        {
            get;
            set;
        }
        public bool Partea3
        {
            get;
            set;
        }

        public bool CotaObligatorie
        {
            get;
            set;
        }

        public short RIGHTOWNERSTYPE
        {
            get;
            set;
        }

        public bool TitluObligatoriu
        {
            get;
            set;
        }

        public bool ValoareaObligatorie
        {
            get;
            set;
        }

    }
}