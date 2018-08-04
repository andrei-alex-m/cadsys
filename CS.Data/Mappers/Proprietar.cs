using System;
using Caly.Common;
using CS.Data.DTO.Excel;
using CS.Data.Entities;

namespace CS.Data.Mappers
{
    public static class Mapper
    {
        public static void FromDTO (this Proprietar prop, OutputProprietar propDTO)
        {
            prop.ExcelRow = propDTO.RowIndex;
            prop.Index = propDTO.Index;
            prop.Nume = propDTO.Nume;
            prop.Initiala = propDTO.Initiala;
            prop.Prenume = propDTO.Prenume;

            prop.TipActIdentitate = string.IsNullOrEmpty(propDTO.TipActIdentitate) ? 
                                        (TipActIdentitate)Enum.Parse(typeof(TipActIdentitate), propDTO.TipActIdentitate)
                                       : (TipActIdentitate?)null;

            prop.Serie = propDTO.Serie;
            prop.Numar = propDTO.Numar;
            prop.Identificator = propDTO.Identificator;
            prop.Emitent = propDTO.Emitent;
            prop.DataEmiterii = propDTO.DataEmiterii;
            prop.Adresa = propDTO.Adresa;
            prop.Localitate = propDTO.Localitate;
            prop.Judet = propDTO.Judet;
            prop.Tara = propDTO.Tara;

            prop.TipPersoana = prop.Identificator.ToString().Length == 13
                                && !prop.Nume.ContainsAny("S.C.", "SC ", "S.R.L.", "SRL") ?
                                TipPersoana.F : TipPersoana.J;
            
            prop.Sex =prop.TipPersoana == TipPersoana.F ? 
                        (int)prop.Identificator.ToString()[0] % 2 == 1 ? Sex.M : Sex.F 
                        : (Sex?)null;

        }
    }

}


