using System;
using System.Collections.Generic;
using System.Linq;
using Caly.Common;
using CS.Services.Interfaces;

namespace CS.Services
{
    public class ServiceBuilder : IServiceBuilder
    {
        static readonly IMatchProcessor adresaMatchProcessor = new adresaMatchProcessor();

        static readonly IMatcher adresaMatcher = new Matcher(SarateniInit);

        static readonly IAddressParser addressParser = new AddressParser();

        static readonly IMatchProcessor combosIndexMatchProcessor = new combosIndexMatchProcessor();

        static readonly IMatcher combosIndexMatcher = new Matcher(CadGenCombosInit);

        public object GetService(string serviceName, params string[] parameters)
        {
            switch (serviceName)
            {
                case "AddressMatcher":
                    return GetAdresaMatcher(parameters);
                case "AddressMatchProcessor":
                    return GetAdresaMatchProcessor(parameters);
                // no need to derive as address structure won't change very soon
                case "AddressParser":
                    return addressParser;
                case "CombosIndexMatcher":
                    return combosIndexMatcher;
                case "CombosIndexMatchProcessor":
                    return combosIndexMatchProcessor;
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
            return adresaMatcher;
        }


        /// <summary>
        /// Gets the match processor.
        /// </summary>
        /// <returns>The match processor.</returns>
        /// <param name="parameters">Parameters.</param> Derive by User prefs later
        static IMatchProcessor GetAdresaMatchProcessor(params string[] parameters)
        {
            return adresaMatchProcessor;
        }

        static IMatcher GetCombosIndexMatcher(params string[] parameters)
        {
            return combosIndexMatcher;
        }

        static IMatchProcessor GetCombosIndexMatchProcessor(params string[] parameters)
        {
            return combosIndexMatchProcessor;
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
            BuildNodes(adresaNode, "TipStrada", streetTypeAbreviations);

            /*
            var streetTypeNode = adresaNode.AddChild(new Classification(2, "TipStrada"));
            streetTypeAbreviations.Keys.ToList().ForEach(x =>
            {
                var abr = streetTypeNode.AddChild(new Classification(3, x));
                abr.AddChild(streetTypeAbreviations[x]);
            });
            */

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

        public static void CadGenCombosInit(Matcher matcher)
        {
            matcher.Root = new TreeNode<Classification, string>(new Classification() { Name = "root", Order = 0 });

            //Tip Document aka DOCT aka DEEDTYPE

            Dictionary<string, string> tipDocument = new Dictionary<string, string>()
            {
                {"4","287,ACTIUNE_INSTANTA,actiune in instanta"},
                {"2","288,ACT_NORMATIV,act normativ"},
                {"3","289,ACT_NOTARIAL,act notarial"},
                {"1","290,ADMINISTRATIVE_1,act administrativ"},
                {"5","291,CERTIFICAT_GREFA1,certificat grefa"},
                {"6","292,FISA_INTERVIU,fisa de interviu"},
                {"7","293,HOTARARE_JUDECATOREASCA,hotarare judecatoreasca"},
                {"8","294,INSCRIS_SUB_SEMNATURA_PRIVATA,inscris sub semnatura privata"},
                {"9","295,ORDONANTA,ordonanta"},
                {"10","296,SOMATIE,somatie"}
            };

            BuildNodes(matcher.Root, "TipDocument", tipDocument);

            Dictionary<string, string> tipInscriere = new Dictionary<string, string>()
            {
                {"0","316,INTAB,Intabulare"},
                {"1","317,NOTATION,Notare"},
                {"2","318,POSESION_REGISTATION,Inscrierea posesiei"},
                {"3","319,PROVISIONALENTRY,Inscrierea provizorie"},
            };

            BuildNodes(matcher.Root, "TipInscriere", tipInscriere);

            Dictionary<string, string> tipStrada = new Dictionary<string, string>()
            {
                {"1","320,AL,Aleea"},
                {"5","321,BDUL,Bulevardul"},
                {"2","322,CALEA,Calea"},
                {"8","323,CAREU,Careul"},
                {"14","324,DRUMUL,Drumul"},
                {"6","325,FND,Fundatura"},
                {"15","326,FNDC,Fundacul"},
                {"4","327,INTR,Intrarea"},
                {"10","330,PIATA,Piata"},
                {"7","328,PREL,Prelungirea"},
                {"3","331,SOS,Soseaua"},
                {"9","332,SPLAIUL,Splaiul"},
                {"0","333,STR,Strada"},
                {"11","334,STRADELA,Stradela"},
                {"12","335,STRD,Stradela"},
                {"13","336,ZONA,Zona"}
            };

            BuildNodes(matcher.Root, "TipStrada", tipStrada);

            Dictionary<string, string> modDobandire = new Dictionary<string, string>()
            {
                {"1","337,ACCESIUNE,Accesiune"},
                {"2","338,CONSTITUIRE,Constituire"},
                {"3","339,CONSTRUIRE,Construire"},
                {"4","340,CONVENTIE,Conventie"},
                {"5","341,EXPROPRIERE,Expropriere"},
                {"6","342,HOTARARE,Hotarare Judecatoreasca"},
                {"7","343,IESIRE_INDIVIZIUNE,Iesire Din Indiviziune"},
                {"8","344,LEGE,Lege"},
                {"9","345,SUCCESIUNE,Succesiune"},
                {"10","346,UZUCAPIUNE,Uzucapiune"},
                {"11","347,adjudecare,Adjudecare"},
                {"12","348,reconstituire,Reconstituire"}
            };

            BuildNodes(matcher.Root, "ModDobandire", modDobandire);

            Dictionary<string, string> tipDreptPt2 = new Dictionary<string, string>()
            {
                {"1","1,ADMINISTRARE"},
                {"2","4,CONCESIUNE"},
                {"4","23,PROPRIETATE"},
                {"5","27,SERVITUTE"},
                {"6","29,SUPERFICIE"},
                {"3","42,FOLOSINTA CU TITLU GRATUIT"}
            };

            Dictionary<string, string> tipDreptPt3 = new Dictionary<string, string>()
            {
                {"2","4,CONCESIUNE"},
                {"3","11,FOLOSINTA"},
                {"5","13,HABITATIE"},
                {"6","14,INCHIRIERE"},
                {"7","18,IPOTECA"},
                {"10","20,PRIVILEGIU IMOBILIAR"},
                {"11","27,SERVITUTE"},
                {"12","29,SUPERFICIE"},
                {"13","30,UZ"},
                {"14","31,UZUFRUCT"},
                {"4","32,FOLOSINTA SPECIALA"},
                {"1","33,COMODAT"},
                {"9","34,LEASING IMOBILIAR"},
                {"15","35,UZUFRUCT VIAGER"},
                {"8","41,IPOTECA LEGALA"}
            };

            var tipDreptNode = matcher.Root.AddChild(new Classification(1, "TipDrept"));
            BuildNodes(tipDreptNode, "Parte2", tipDreptPt2);
            BuildNodes(tipDreptNode, "Parte3", tipDreptPt3);

            Dictionary<string, string> judete = new Dictionary<string, string>()
            {
                {"1","10,AB,ALBA"},
                {"2","29,AR,ARAD"},
                {"3","38,AG,ARGES"},
                {"4","47,BC,BACAU"},
                {"5","56,BH,BIHOR"},
                {"6","65,BN,BISTRITA NASAUD"},
                {"7","74,BT,BOTOSANI"},
                {"8","92,BR,BRAILA"},
                {"9","83,BV,BRASOV"},
                {"10","403,B,BUCURESTI"},
                {"11","109,BZ,BUZAU"},
                {"12","519,CL,CALARASI"},
                {"13","118,CS,CARASSEVERIN"},
                {"14","127,CJ,CLUJ"},
                {"15","136,CT,CONSTANTA"},
                {"16","145,CV,COVASNA"},
                {"17","154,DB,DAMBOVITA"},
                {"18","163,DJ,DOLJ"},
                {"19","172,GL,GALATI"},
                {"20","528,GR,GIURGIU"},
                {"21","181,GJ,GORJ"},
                {"22","190,HR,HARGHITA"},
                {"23","207,HD,HUNEDOARA"},
                {"24","216,IL,IALOMITA"},
                {"25","225,IS,IASI"},
                {"26","234,IF,ILFOV"},
                {"27","243,MM,MARAMURES"},
                {"28","252,MH,MEHEDINTI"},
                {"29","261,MS,MURES"},
                {"30","270,NT,NEAMT"},
                {"31","289,OT,OLT"},
                {"32","298,PH,PRAHOVA"},
                {"33","314,SJ,SALAJ"},
                {"34","305,SM,SATU MARE"},
                {"35","323,SB,SIBIU"},
                {"36","332,SV,SUCEAVA"},
                {"37","341,TR,TELEORMAN"},
                {"38","350,TM,TIMIS"},
                {"39","369,TL,TULCEA"},
                {"40","387,VL,VALCEA"},
                {"41","378,VS,VASLUI"},
                {"42","396,VN,VRANCEA"}
            };

            BuildNodes(matcher.Root, "Judet", judete);
        }

        private static void BuildNodes(TreeNode<Classification, string> parent, string topBranchName, Dictionary<string, string> cherries)
        {
            var node = parent.AddChild(new Classification(parent.Value.Order + 1, topBranchName));
            cherries.Keys.ToList().ForEach(x =>
            {
                var bottomNode = node.AddChild(new Classification(parent.Value.Order + 2, x));
                bottomNode.AddChild(cherries[x]);
            });
        }

        #endregion
    }

    #region matchprocessor

    public class adresaMatchProcessor : IMatchProcessor
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

    public class combosIndexMatchProcessor : IMatchProcessor
    {
        public bool Process(params object[] prm)
        {
            var find = prm[0].ToString();

            var leaf = prm[1].ToString();
            return leaf.Match(find, ',');
        }
    }


    #endregion

}
