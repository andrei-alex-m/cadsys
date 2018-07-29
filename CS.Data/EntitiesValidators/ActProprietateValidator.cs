using System;
using FluentValidation;
using CS.Data;
using CS.Data.Entities;
using CS.Data.DTO;
namespace CS.Data.EntitiesValidators
{
    public class ActProprietateValidator : AbstractValidator<ActProprietate>
    {
        public ActProprietateValidator()
        {
            RuleFor(x => x.TipActProprietateId).NotNull().NotEqual(0).WithMessage("Tip Act lipsa");
            RuleFor(x => x.Numar).NotEmpty().WithMessage("Numar Act lipsa");
            RuleFor(x => x.Data).NotEmpty().WithMessage("Data lipsa");
            RuleFor(x => x.Emitent).NotEmpty().WithMessage("Emitent lipsa");


            RuleFor(x => x.Index).NotNull().NotEqual(0).WithMessage("Index lipsa");
        }
    }
}
