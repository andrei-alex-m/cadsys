using System;
using FluentValidation;
using CS.Data;
using CS.Data.Entities;
using CS.Data.DTO;
namespace CS.Data.EntitiesValidators
{
    public class ParcelaValidator : AbstractValidator<Parcela>
    {
        public ParcelaValidator()
        {
            RuleFor(x => x.TarlaId).NotNull().NotEqual(0).WithMessage("Tarla lipsa");

            RuleFor(x => x.Denumire).NotEmpty().WithMessage("Numar lipsa");

            RuleFor(x => x.CatFol).NotEmpty().Must(x => x != 0).WithMessage("Cat Fol lipsa");

            RuleFor(x => x.Suprafata).NotEmpty().WithMessage("Suprafata lipsa");

            RuleFor(x => x.Index).NotNull().NotEqual(0).WithMessage("Index lipsa");
        }
    }
}
