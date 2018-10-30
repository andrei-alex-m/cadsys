using System.Text;

namespace Caly.Common
{
    public interface IAdresaFaraLocalitate
    {
        string TipStrada { get; set; }
        string Strada { get; set; }
        string Numar { get; set; }
        string Bloc { get; set; }
        string Scara { get; set; }
        string Etaj { get; set; }
        string Apt { get; set; }
        string CodPostal { get; set; }
        string Descriere { get; set; }
        string AdresaImport { get; set; }
    }

    public interface IAdresa: IAdresaFaraLocalitate
    {
        string Judet { get; set; }
        string Localitate { get; set; }
    }

    public static class AdresaExtensions
    {
        public static string GetConcat(this IAdresaFaraLocalitate adresa)
        {
            string result = string.Empty;

            if (!string.IsNullOrEmpty(adresa.Strada))
            {

                if (!string.IsNullOrEmpty(adresa.TipStrada))
                {
                    result += adresa.TipStrada + " ";
                }

                result += adresa.Strada;
            }

            if (!string.IsNullOrEmpty(adresa.Numar))
            {
                result += ", Nr. " + adresa.Numar;
            }

            if(!string.IsNullOrEmpty(adresa.Bloc))
            {
                result += ", Bl. " + adresa.Bloc;
            }

            if (!string.IsNullOrEmpty(adresa.Scara))
            {
                result += ", Sc. " + adresa.Scara;
            }

            if (!string.IsNullOrEmpty(adresa.Etaj))
            {
                result += ", Et. " + adresa.Etaj;
            }

            if (!string.IsNullOrEmpty(adresa.Apt))
            {
                result += ", Ap. " + adresa.Apt;
            }

            return result.Trim(',', ' ');
        }


    }
}