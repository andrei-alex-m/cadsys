using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Caly.Common;
using CS.Data.DTO.Excel;
using CS.Data.Entities;

namespace CS.Data.Mappers
{
    public static class AdresaMapperExtensions
    {
        public static void FromDTO(this Adresa adresa, OutputProprietarAdresa adresaDTO, IEnumerable<Judet> judeteAllInclussive, IAddressParser parser, IMatcher addressMatcher, IMatchProcessor matchProcessor)
        {
            var localitate = LocalitateFromDTO(adresaDTO.Judet, adresaDTO.Localitate, judeteAllInclussive);

            if (localitate != null)
            {
                adresa.SIRSUP = localitate.UAT.SIRUTA;
                adresa.SIRUTA = localitate.SIRUTA;
            }

            if (string.IsNullOrEmpty(adresaDTO.Adresa))
            {
                adresa.AdresaNecunoscuta = true;
            }
            else
            {
                parser.Parse(adresa, adresaDTO.Adresa, addressMatcher, matchProcessor);
            }

            adresa.AdresaImport = adresaDTO.Adresa;
            adresa.JudetImport = adresaDTO.Judet;
            adresa.LocalitateImport = adresaDTO.Localitate;
        }

        public static void FromPOCO(this OutputProprietarAdresa adresaDTO, Adresa adresa)
        {
            adresaDTO.Judet = adresa.Localitate?.UAT.Judet.Denumire;
            adresaDTO.Judet += Environment.NewLine + adresa.JudetImport;
            adresaDTO.Localitate = adresa.Localitate?.Denumire;
            adresaDTO.Localitate += Environment.NewLine + adresa.LocalitateImport;
            adresaDTO.Adresa = adresa.GetConcat();
            adresaDTO.Adresa += Environment.NewLine + adresa.AdresaImport;

        }

        static Localitate LocalitateFromDTO(string judet, string localitate, IEnumerable<Judet> judeteAllInclussive)
        {
            if (string.IsNullOrEmpty(judet) || string.IsNullOrEmpty(localitate))
            {
                return null;
            }

            var _judet = judeteAllInclussive.FirstOrDefault(x => x.Denumire.Equals(judet, StringComparison.InvariantCultureIgnoreCase));

            if (_judet == null)
            {
                return null;
            }

            var sirutas = _judet.UATs.SelectMany(x => x.Localitati).Where(x => x.Denumire.Equals(localitate, StringComparison.InvariantCultureIgnoreCase));
            if (sirutas.Count() == 1)
            {
                return sirutas.First();
            }

            var sirsup = _judet.UATs.FirstOrDefault(x => x.Denumire.Equals(localitate, StringComparison.InvariantCultureIgnoreCase));
            if (sirsup != null)
            {
                return sirsup.Localitati.FirstOrDefault(x => x.Denumire.Equals(localitate, StringComparison.InvariantCultureIgnoreCase));
            }

            return null;
        }

    }
}
