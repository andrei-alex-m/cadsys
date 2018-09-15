using System;
using FluentValidation;
using CS.Data;
using CS.Data.Entities;
using CS.Data.DTO;
using Caly.Common;
using System.Linq;

namespace CS.EF.EntitiesValidators
{
    public class ProprietarValidator : AbstractValidator<Proprietar>
    {
        public ProprietarValidator(CadSysContext context)
        {
            RuleSet("NoContext", () =>
            {
                RuleFor(x => x.Index).NotEmpty().WithMessage("Index lipsa");
                RuleFor(x => x.Nume).NotEmpty().WithMessage("Nume lipsa");
                RuleFor(x => x.Prenume).NotEmpty().WithMessage("Prenume lipsa").When(x => x.TipPersoana == TipPersoana.F);
                RuleFor(x => x.Adresa).NotEmpty().WithMessage("Adresa lipsa");

                RuleFor(x => x.Identificator).Must(Validation.isValidCNP)
                                             .When(x => x.TipPersoana == TipPersoana.F 
                                                   //&& (x.TipActIdentitate == TipActIdentitate.BI || x.TipActIdentitate == TipActIdentitate.CI)
                                                  )
                                             .WithMessage("CNP nevalid");

                RuleFor(x => x.Serie).NotEmpty()
                                   .When(x => x.TipPersoana == TipPersoana.F &&
                                                   (x.TipActIdentitate == TipActIdentitate.BI || x.TipActIdentitate == TipActIdentitate.CI))
                                   .WithMessage("Serie lipsa");

                RuleFor(x => x.Numar).NotEmpty()
                                     .When(x => x.TipPersoana == TipPersoana.F &&
                                         (x.TipActIdentitate == TipActIdentitate.BI || x.TipActIdentitate == TipActIdentitate.CI || x.TipActIdentitate == TipActIdentitate.Deces))
                                     .WithMessage("Numar act identitate lipsa");

                RuleFor(x => x.Index).NotNull().NotEqual(0).WithMessage("Index lipsa");
            });

            RuleSet("InSet", () =>
            {
                RuleFor(x => x).Custom((x, c) =>
                {
                    var opResult = x.CompareInSet(context, y => y.Index, y => y.Index, y => y.Nume, y => y.Prenume);
                    if (opResult.Result)
                    {
                        c.AddFailure("Duplicat pe nume la indecsii: " + string.Join(',', opResult.Observations));
                    }
                });

                RuleFor(x => x).Custom((x, c) =>
                {
                    var opResult = x.CompareInSet(context, y => y.Index,y=>y.Index, y => y.Identificator);
                    if (opResult.Result)
                    {
                        c.AddFailure("Duplicat pe CNP la indecsii: " + string.Join(',', opResult.Observations));
                    }
                });
            });

            RuleSet("Context", () =>
            {
                RuleFor(x => x).Custom((x, c) =>
                {
                    if (!context.InscrieriProprietari.Any(z => z.IdProprietar == x.Id))
                    {
                        c.AddFailure("Nu are inscrieri");
                    }
                });
            });
        }


    }
}
