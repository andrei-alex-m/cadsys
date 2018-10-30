using System;
using System.Collections.Generic;
using System.Linq;

namespace Caly.Common
{
    public class AddressParser : IAddressParser
    {

        public void Parse(IAdresaFaraLocalitate address, string field, IMatcher matcher, IMatchProcessor matchProcessor)
        {
            var classification = new List<Classification>()
            {
                new Classification(){Order=0, Name="root"},
                new Classification(){Order=1, Name="Adresa"}
            };

            var chunks = field.Split(',', ';');

            chunks.ToList().ForEach(x =>
            {
                var trimmed = x.Trim();
                var split = trimmed.Split(' ', 2);
                if (split.Count() != 2)
                {
                    split = trimmed.Split('.', 2);
                }

                if (split.Count() == 2)
                {
                    var prefix = split[0].Trim('.');
                    split[1] = split[1].Trim();

                    var classificationResult = matcher.Match(classification, prefix, matchProcessor);

                    if (classificationResult.Count > 0)
                    {
                        switch (classificationResult[0].Name)
                        {
                            case "TipStrada":
                                address.TipStrada = classificationResult[1].Name;
                                address.Strada = split[1];
                                break;
                            case "Numar":
                                address.Numar = split[1];
                                break;
                            case "Bloc":
                                address.Bloc = split[1];
                                break;
                            case "Scara":
                                address.Scara = split[1];
                                break;
                            case "Etaj":
                                address.Etaj = split[1];
                                break;
                            case "Apt":
                                address.Apt = split[1];
                                break;
                        }
                    };
                }
            });
        }
    }
}
