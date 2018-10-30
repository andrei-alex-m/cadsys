using System;
using System.Collections.Generic;
using System.Linq;
using Caly.Common;
using CS.Services.Interfaces;

namespace CS.Services
{
    public class ServiceBuilder : IServiceBuilder
    {
        static readonly IMatchProcessor smp = new SarateniAdresaMatchProcessor();

        static readonly IMatcher sm = new Matcher(SarateniInit);

        static readonly IAddressParser ap = new AddressParser();

        public object GetService(string serviceName, params string[] parameters)
        {
            switch (serviceName)
            {
                case "AddressMatcher":
                    return GetAdresaMatcher(parameters);
                case "AddressMatchProcessor":
                    return GetMatchProcessor(parameters);
                // no need to derive as address structure won't change very soon
                case "AddressParser":
                    return ap;
                default:
                    throw new ArgumentException("Service Name Not Found!");

            }
        }

        /// <summary>
        /// Gets the adresa matcher.
        /// </summary>
        /// <returns>The adresa matcher.</returns>
        /// <param name="parameters">Parameters.</param> Derive by User prefs later
        static IMatcher GetAdresaMatcher(params string[] parameters)
        {
            return sm;
        }


        /// <summary>
        /// Gets the match processor.
        /// </summary>
        /// <returns>The match processor.</returns>
        /// <param name="parameters">Parameters.</param> Derive by User prefs later
        static IMatchProcessor GetMatchProcessor(params string[] parameters)
        {
            return smp;
        }

        #region trees
        /// <summary>
        /// Maybe load from config later
        /// </summary>
        public static void SarateniInit(Matcher matcher)
        {
            Dictionary<string, string> streetTypeAbreviations = new Dictionary<string, string>()
        {
            {"AL", "Aleea,Ale,Al"},
            {"BDUL","Bulevardul,Bld,Bdul,Bd,Blvd"},
            {"CALEA","Calea,Cal"},
            {"DRUMUL","Drumul,Dr"},
            {"INTR","Intrarea,Intr"},
            {"PREL", "Prelungirea,Prel"},
            {"PIATA","Piata,Pta"},
            {"PSJ","Pasajul,Psj"},
            {"SOS","Soseaua,Sos"},
            {"SPLAIUL","Splaiul"},
            {"STR","Strada,Str,St"}
        };

            matcher.Root = new TreeNode<Classification, string>(new Classification() { Name = "root", Order = 0 });

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

            var scaraNode = adresaNode.AddChild(new Classification(2, "Scara"));
            scaraNode.AddChild("Scara,Sc");

            //etaj
            var etajNode = adresaNode.AddChild(new Classification(2, "Etaj"));
            etajNode.AddChild("Et,Etj,Etaj");

            //apt
            var aptNode = adresaNode.AddChild(new Classification(2, "Apt"));
            aptNode.AddChild("Apt,Ap,Apartament");

        }

        #endregion
    }

    #region matchprocessor

    public class SarateniAdresaMatchProcessor : IMatchProcessor
    {
        readonly char[] splitters = { ',', ';' };

        public bool Process(params object[] prm)
        {
            var find = prm[0].ToString();

            find = find.Replace("-", "");

            var leaf = prm[1].ToString();
            return leaf.Match(find, splitters);
        }
    }


    #endregion

}
