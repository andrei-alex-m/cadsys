using System;
using CS.Data.Entities;
using CS.Data.DTO.Excel;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Concurrent;

namespace CS.Data.Mappers
{
    public static class ActProprietateMapperExtensions
    {
        public static void FromDTO(this ActProprietate actProp, OutputActProprietate actPropDTO, IEnumerable<TipActProprietate> tipActe)
        {
            try
            {
                actProp.ExcelRow = actPropDTO.RowIndex;
                actProp.Index = actPropDTO.Index.Value;

                TipActProprietate tipActProprietate = string.IsNullOrEmpty(actPropDTO.TipAct) ? null :  tipActe.FirstOrDefault(x => x.Denumire.Trim().Equals(actPropDTO.TipAct.Trim(), StringComparison.InvariantCultureIgnoreCase));
                actProp.TipActProprietateId = tipActProprietate !=null ? tipActProprietate.Id: (int?)null;
                actProp.Numar = actPropDTO.Numar;
                actProp.Data = actPropDTO.Data;
                actProp.Emitent = actPropDTO.Emitent;
                actProp.Carnet = actPropDTO.Carnet;
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debugger.Break();
            }
        }

        public static void FromPOCO(this OutputActProprietate actPropDTO, ActProprietate actProp)
        {
            actPropDTO.RowIndex = actProp.ExcelRow;
            actPropDTO.Index = actProp.Index;
            actPropDTO.TipAct = actProp.TipAct?.Denumire;
            actPropDTO.Numar = actProp.Numar;
            actPropDTO.Data = actProp.Data;
            actPropDTO.Emitent = actProp.Emitent;
            actPropDTO.Carnet = actProp.Carnet;
        }
    }
}
