using System;
using FluentValidation;
using CS.Data;
using CS.Data.Entities;
using CS.Data.DTO;
using Caly.Common;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CS.EF.EntitiesValidators
{
    public class ParcelaValidator : AbstractValidator<Parcela>
    {
        public ParcelaValidator(CadSysContext context)
        {
            RuleSet("NoContext", ()=>
            {
                RuleFor(x => x.Index).NotEmpty().WithMessage("Index lipsa");
                RuleFor(x => x.TarlaId).NotNull().NotEqual(0).WithMessage("Tarla lipsa");

                RuleFor(x => x.Denumire).NotEmpty().WithMessage("Numar lipsa");

                RuleFor(x => x.CatFol).Must(x => x.HasValue).WithMessage("Cat Fol lipsa");

                RuleFor(x => x.Suprafata).NotEmpty().WithMessage("Suprafata lipsa");

                RuleFor(x => x.Index).NotNull().NotEqual(0).WithMessage("Index lipsa");
            });

            RuleSet("InSet", () =>
            {
                RuleFor(x => x).Custom((x, c) =>
                {
                    var opResult = x.CompareInSet(context, y => y.Index, y => y.Index, y => y.TarlaId, y => y.Denumire);
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
                    if (!context.InscrieriImobile.Include(q=>q.Imobil).Select(y=>y.Imobil).Any(z => z.Id == x.ImobilId))
                    {
                        c.AddFailure("Nu are inscrieri");
                    }
                });
            });
        }
    }
}
