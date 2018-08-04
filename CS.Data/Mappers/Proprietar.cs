using System;
using Caly.Common;
using CS.Data.DTO.Excel;
using CS.Data.Entities;
using CS.Data.EntitiesValidators;

namespace CS.Data.Mappers
{
    public static class ProprietarMapperExtensions
    {
        public static void FromDTO (this Proprietar prop, OutputProprietar propDTO)
        {
            prop.ExcelRow = propDTO.RowIndex;
            prop.Index = propDTO.Index;
            prop.Nume = propDTO.Nume;
            prop.Initiala = propDTO.Initiala;
            prop.Prenume = propDTO.Prenume;

            object tipact;

            if (Enum.TryParse(typeof(TipActIdentitate), propDTO.TipActIdentitate, true, out tipact))
            {
                prop.TipActIdentitate = (TipActIdentitate)tipact;
            }

            prop.Serie = propDTO.Serie;
            prop.Numar = propDTO.Numar;
            prop.Identificator = propDTO.Identificator;
            prop.Emitent = propDTO.Emitent;
            prop.DataEmiterii = propDTO.DataEmiterii;
            prop.Adresa = propDTO.Adresa;
            prop.Localitate = propDTO.Localitate;
            prop.Judet = propDTO.Judet;
            prop.Tara = propDTO.Tara;

            prop.TipPersoana = ProprietarValidator.isValidCNP(prop.Identificator)
                                && !prop.Nume.ContainsAny("S.C.", "SC ", "S.R.L.", "SRL") ?
                                TipPersoana.F : TipPersoana.J;
            
            prop.Sex =prop.TipPersoana == TipPersoana.F ? 
                        (int)prop.Identificator.ToString()[0] % 2 == 1 ? Sex.M : Sex.F 
                        : (Sex?)null;

        }

        public static void FromPOCO (this OutputProprietar propDTO, Proprietar prop)
        {
            propDTO.RowIndex = prop.ExcelRow;
            propDTO.Index = prop.Index;
            propDTO.Nume = prop.Nume;
            propDTO.Initiala = prop.Initiala;
            propDTO.Prenume = prop.Prenume;
            propDTO.TipActIdentitate = prop.TipActIdentitate.HasValue ? prop.TipActIdentitate.ToString() : null;
            propDTO.Serie = prop.Serie;
            propDTO.Numar = prop.Numar;
            propDTO.Identificator = prop.Identificator;
            propDTO.Emitent = prop.Emitent;
            propDTO.DataEmiterii = prop.DataEmiterii;
            propDTO.Adresa = prop.Adresa;
            propDTO.Localitate = prop.Localitate;
            propDTO.Judet = prop.Judet;
            propDTO.Tara = prop.Tara;
        }
    }

}


