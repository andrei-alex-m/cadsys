using System;
using System.Collections.Generic;
using System.Linq;
using Caly.Common;
using CS.Data.DTO.Excel;
using CS.Data.Entities;

namespace CS.EF.Parsers
{
    public static class AdresaParser
    {
        public static Dictionary<string, string>  streetTypeAbreviations = new Dictionary<string, string>()
        {
            {"AL", "Aleea,Ale,Al"},
            {"BDUL","Bulevardul,Bdul,Bd,Blvd"},
            {"CALEA","Calea"},
            {"DRUMUL","Drumul,Dr"},
            {"INTR","Intrarea,Intr"},
            {"PREL", "Prelungirea,Prel"},
            {"PIATA","Piata,Pta"},
            {"PSJ","Pasajul,Psj"},
            {"SOS","Soseaua,Sos"},
            {"SPLAIUL","Splaiul"},
            {"STR","Strada,Str,St"}
        };
        public static Matcher AdresaMatcher
        {
            get
            {
                var matcher =  new Matcher();

                var adresaNode = matcher.Root.AddChild(new Classification(1, "Adresa"));

                //tip strada
                var streetTypeNode = adresaNode.AddChild(new Classification(2, "TipStrada"));
                streetTypeAbreviations.Keys.ToList().ForEach(x =>
                {
                    var abr = streetTypeNode.AddChild(new Classification(3, x));
                    abr.AddChild(streetTypeAbreviations[x]);
                });

                //numar
                var streetNumberNode = adresaNode.AddChild(new Classification(2, "Numar"));
                streetNumberNode.AddChild("Nr,Numar,Numarul");

                //bloc
                var blocNode = adresaNode.AddChild(new Classification(2, "Bloc"));
                blocNode.AddChild("Bloc,Bl");

                //etaj
                var etajNode = adresaNode.AddChild(new Classification(2, "Etaj"));
                etajNode.AddChild("Et,Etj,Etaj");

                //apt
                var aptNode = adresaNode.AddChild(new Classification(2, "Apt"));
                etajNode.AddChild("Apt,Ap,Apartament");

                return matcher;
            }
        }

        //public static Adresa FromDTO(OutputProprietar prop)
        //{
        //    var result = new Adresa();
        //    if (string.IsNullOrEmpty(prop.Adresa))
        //    {
        //        result.AdresaNecunoscuta = true;
        //    }
        //}

        //private static Tuple<int,int> SIRUTAsFromDTO(string judet, string localitate)
        //{

        //}

    }

    public class AdresaMatchProcessor : IMatchProcessor
    {
        char[] splitters = { ',', ';' };

        public bool Process(params object[] prm)
        {
            var find = prm[0].ToString();

            find = find.Replace("-","");

            var leaf = prm[1].ToString();
            return leaf.Match(find, splitters);
        }
    }


}
