using System;
using System.Linq;
using CS.Data.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CS.EF.EntitiesValidators
{
    public class InscriereValidator : AbstractValidator<Inscriere>
    {
        public InscriereValidator(CadSysContext context)
        {
            RuleSet("Context", () =>
            {
                RuleFor(x => x).Custom((x, c) =>
                {
                    switch (x.GetType().Name)
                    {
                        case "InscriereAct":
                            var ia = context.InscrieriActe.Include(y => y.ActProprietate).SingleOrDefault(y => y.Id == x.Id);
                            if (ia.ActProprietate == null)
                            {
                                c.AddFailure("Index", "Index Act Inexistent");
                            }
                            break;
                        case "InscriereImobil":
                            var ii = context.InscrieriImobile.Include(y => y.Imobil).SingleOrDefault(y => y.Id == x.Id);
                            if (ii.Imobil == null)
                            {
                                c.AddFailure("Index", "Index Parcela inexistent");
                            }
                            break;
                        case "InscriereProprietar":
                            var ip = context.InscrieriProprietari.Include(y => y.Proprietar).SingleOrDefault(y => y.Id == x.Id);
                            if (ip.Proprietar == null)
                            {
                                c.AddFailure("Index", "Index Proprietar inexistent");
                            }
                            break;
                        default:
                            c.AddFailure("Tip Inscriere inexistent");
                            break;
                    }
                });
            });
        }
    }

    //must validate with include ActProprietate from dbset

    public class InscriereActValidator : AbstractValidator<InscriereAct>
    {
        public InscriereActValidator()
        {
            RuleFor(x => x).Custom((x, c) =>
            {
                if (x.ActProprietate == null)
                {
                    c.AddFailure("Index", "Index Act Inexistent");
                }
            });
        }
    }

    //must validate with (imobil then include parcele)

    public class InscriereImobilValidator : AbstractValidator<InscriereImobil>
    {
        public InscriereImobilValidator()
        {
            RuleFor(x => x).Custom((x, c) =>
            {
                if (x.Imobil == null || x.Imobil.Parcele.Count == 0 || x.Imobil.Parcele.FirstOrDefault()?.Index != x.Index)
                {
                    c.AddFailure("Index", "Index Parcela Inexistent");
                }
            });
        }
    }

    public class InscriereProprietarValidator : AbstractValidator<InscriereProprietar>
    {
        public InscriereProprietarValidator()
        {
            RuleFor(x => x).Custom((x, c) =>
            {
                if (x.Proprietar == null)
                {
                    c.AddFailure("Index", "Index Act Inexistent");
                }
            });
        }
    }
}
