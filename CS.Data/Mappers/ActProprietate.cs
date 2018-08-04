using System;
using CS.Data.Entities;
using CS.Data.DTO.Excel;
using System.Collections.Generic;
using System.Linq;

namespace CS.Data.Mappers
{
    public static class ActProprietateMapperExtensions
    {
        public static void FromDTO(this ActProprietate actProp, OutputActProprietate actPropDTO, List<TipActProprietate> tipActe)
        {
            actProp.ExcelRow = actPropDTO.RowIndex;
            actProp.Index = actPropDTO.Index;
            actProp.TipActProprietateId = tipActe.FirstOrDefault(x => x.Denumire == actPropDTO.TipAct).Id;
            actProp.Numar = actPropDTO.Numar;
            actProp.Data = actPropDTO.Data;
            actProp.Emitent = actPropDTO.Emitent;
            actProp.Carnet = actPropDTO.Carnet;
        }

        public static void FromPOCO(this OutputActProprietate actPropDTO, ActProprietate actProp)
        {
            actPropDTO.RowIndex = actProp.ExcelRow;
            actPropDTO.Index = actProp.Index;
            actPropDTO.TipAct = actProp.TipActProprietate.Denumire;
            actPropDTO.Numar = actProp.Numar;
            actPropDTO.Data = actProp.Data;
            actPropDTO.Emitent = actProp.Emitent;
            actPropDTO.Carnet = actProp.Carnet;
        }
    }
}
