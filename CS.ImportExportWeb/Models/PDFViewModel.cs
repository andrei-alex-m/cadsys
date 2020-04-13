using System;
using System.ComponentModel;

namespace CS.ImportExportWeb.Models
{
    public class PDFViewModel
    {
        [DisplayName("Director Acte Proprietate")]
        public string ActeProprietateDirectory { get; set; } = "/CadGen/Centralizator Gri/ID ACTE";
        [DisplayName("Director Acte Identitate")]
        public string ActeIdentitateDirectory { get; set; } = "/CadGen/Centralizator Gri/ID PROPR";
        [DisplayName("Director Fise")]
        public string FiseDirectory { get; set; } = "/CadGen/Teste/Fise/S12/Fise Imobil Sector 12";
        [DisplayName("Director Output")]
        public string OutputDirectory { get; set; } = "/CadGen/PDFResults/PDFSector12";
    }
}
