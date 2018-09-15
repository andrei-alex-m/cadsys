using System;
using FluentValidation;
using CS.Data;
using CS.Data.Entities;
using CS.Data.DTO;
using FluentValidation.Results;
using Caly.Common;
using System.Linq;

namespace CS.EF.EntitiesValidators
{
    public class ActProprietateValidator : AbstractValidator<ActProprietate>
    {
        public ActProprietateValidator(CadSysContext context)
        {
            RuleSet("NoContext", () =>
            {
                RuleFor(x => x.Index).NotEmpty().WithMessage("Index lipsa");
                RuleFor(x => x.IdTipActProprietate).NotNull().NotEqual(0).WithMessage("Tip Act lipsa");
                RuleFor(x => x.Numar).NotEmpty().WithMessage("Numar Act lipsa");
                RuleFor(x => x.Data).Must(x => x.HasValue).WithMessage("Data lipsa");
                RuleFor(x => x.Emitent).NotEmpty().WithMessage("Emitent lipsa");

                RuleFor(x => x.Index).NotNull().NotEqual(0).WithMessage("Index lipsa");
            });

            RuleSet("InSet", () =>
            {
                RuleFor(x => x).Custom((x, c) =>
                {
                    var opResult = x.CompareInSet(context, y => y.Index, y => y.Index, y => y.Numar, y=>y.Data, y => y.IdTipActProprietate);
                    if (opResult.Result)
                    {
                        c.AddFailure("Duplicat pe numar, data si tip la indecsii: " + string.Join(',', opResult.Observations));
                    }
                });
            });

            RuleSet("Context", () =>
            {
                RuleFor(x => x).Custom((x, c) =>
                {
                    if (!context.InscrieriActe.Any(z => z.IdActProprietate == x.Id))
                    {
                        c.AddFailure("Nu are inscrieri");
                    }
                });
            });
        }

    }
}
