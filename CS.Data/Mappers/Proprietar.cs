using System;
using Caly.Common;
using CS.Data.DTO.Excel;
using CS.Data.Entities;

namespace CS.Data.Mappers
{
    public static class ProprietarMapperExtensions
    {
        public static void FromDTO(this Proprietar prop, OutputProprietarAdresa propDTO)
        {
            prop.ExcelRow = propDTO.RowIndex;
            prop.Index = propDTO.Index.Value;
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

            prop.TipPersoana = tipPersoana(prop);

            prop.Sex = prop.TipPersoana == TipPersoana.F ?
                prop.Identificator.HasValue && (int)prop.Identificator.ToString()[0] % 2 == 1 ? Sex.M : Sex.F
                        : (Sex?)null;

            TipPersoana tipPersoana(Proprietar p)
            {
                return Validation.isValidCNP(p.Identificator) || !String.IsNullOrEmpty(p.Prenume) ? TipPersoana.F : TipPersoana.J;
            }

        }

        public static void FromPOCO(this OutputProprietarAdresa propDTO, Proprietar prop)
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
        }
    }

}


