using System;
using FluentValidation;
using CS.Data;
using CS.Data.Entities;
using CS.Data.DTO;
using Caly.Common;
using System.Linq;

namespace CS.EF.EntitiesValidators
{
    public class AdresaValidator: AbstractValidator<Adresa>
    {
        public AdresaValidator()
        {
            RuleSet("NoContext", () =>
            {
                RuleFor(x => x).Custom((x, c) =>
                {
                    if (x.Localitate==null)
                    {
                        c.AddFailure("Judet","Combinatia de Judet + Localitate nu exista");
                    }
                });

                RuleFor(x => x).Custom((x, c) =>
                {
                    if (string.IsNullOrEmpty(x.Strada) && (!string.IsNullOrWhiteSpace(x.Bloc)|| !string.IsNullOrEmpty(x.Numar)))
                    {
                        c.AddFailure("Adresa", "Numar sau Bloc fara Strada");
                    }

                    if (string.IsNullOrEmpty(x.TipStrada) && !string.IsNullOrEmpty(x.Strada))
                    {
                        c.AddFailure("Adresa", "Strada fara TipStrada");
                    }

                    if (!string.IsNullOrEmpty(x.Bloc) && (string.IsNullOrEmpty(x.Apt) || string.IsNullOrEmpty(x.Scara)))
                    {
                        c.AddFailure("Adresa", "Bloca fara Scara sau Apt");
                    }

                    if(!string.IsNullOrEmpty(x.Etaj) && string.IsNullOrEmpty(x.Bloc) && string.IsNullOrEmpty(x.Numar))
                    {
                        c.AddFailure("Adresa", "Etaj fara Numar sau Bloc");
                    }

                    if (!string.IsNullOrEmpty(x.Apt) && string.IsNullOrEmpty(x.Bloc) && string.IsNullOrEmpty(x.Numar))
                    {
                        c.AddFailure("Adresa", "Apt fara Numar sau Bloc");
                    }

                });

            });
        }
    }
}
