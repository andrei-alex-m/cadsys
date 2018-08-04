using System;
using FluentValidation;
using CS.Data;
using CS.Data.Entities;
using CS.Data.DTO;

namespace CS.Data.EntitiesValidators
{
    public class ProprietarValidator : AbstractValidator<Proprietar>
    {
        public ProprietarValidator()
        {
            RuleFor(x => x.Index).NotEmpty().WithMessage("Index lipsa");
            RuleFor(x => x.Nume).NotEmpty().WithMessage("Nume lipsa");
            RuleFor(x => x.Prenume).NotEmpty().WithMessage("Prenume lipsa").When(x => x.TipPersoana == TipPersoana.F);
            RuleFor(x => x.Adresa).NotEmpty().WithMessage("Adresa lipsa");

            RuleFor(x => x.Identificator).Must(isValidCNP)
                                         .When(x=>x.TipPersoana==TipPersoana.F && 
                                               (x.TipActIdentitate==TipActIdentitate.BI ||x.TipActIdentitate == TipActIdentitate.CI))
                                         .WithMessage("CNP nevalid");
            
            RuleFor(x=>x.Serie).NotEmpty()
                               .When(x => x.TipPersoana == TipPersoana.F &&
                                               (x.TipActIdentitate == TipActIdentitate.BI || x.TipActIdentitate == TipActIdentitate.CI))
                               .WithMessage("Serie lipsa");
            
            RuleFor(x => x.Numar).NotEmpty()
                                 .When(x => x.TipPersoana == TipPersoana.F &&
                                     (x.TipActIdentitate == TipActIdentitate.BI || x.TipActIdentitate == TipActIdentitate.CI || x.TipActIdentitate==TipActIdentitate.Deces))
                                 .WithMessage("Numar act identitate lipsa");
            
            RuleFor(x => x.Index).NotNull().NotEqual(0).WithMessage("Index lipsa");
        }

        public static bool isValidCNP(long vcnp)
        {
            var cnp = vcnp.ToString();
            try
            {
                if (cnp.Replace(" ", "").Length != 13)
                {
                    return false;
                }
                else
                {
                    string tempNumber = "1234567890";
                    if ((tempNumber.IndexOf(cnp[0]) < 0) ||
                        (tempNumber.IndexOf(cnp[1]) < 0) ||
                        (tempNumber.IndexOf(cnp[2]) < 0) ||
                        (tempNumber.IndexOf(cnp[3]) < 0) ||
                        (tempNumber.IndexOf(cnp[4]) < 0) ||
                        (tempNumber.IndexOf(cnp[5]) < 0) ||
                        (tempNumber.IndexOf(cnp[6]) < 0) ||
                        (tempNumber.IndexOf(cnp[7]) < 0) ||
                        (tempNumber.IndexOf(cnp[8]) < 0) ||
                        (tempNumber.IndexOf(cnp[9]) < 0) ||
                        (tempNumber.IndexOf(cnp[10]) < 0) ||
                        (tempNumber.IndexOf(cnp[11]) < 0) ||
                        (tempNumber.IndexOf(cnp[12]) < 0))
                    {
                        return false;
                    }

                    int s = Int32.Parse(cnp[0].ToString()) * 2 +
                            Int32.Parse(cnp[1].ToString()) * 7 +
                            Int32.Parse(cnp[2].ToString()) * 9 +
                            Int32.Parse(cnp[3].ToString()) * 1 +
                            Int32.Parse(cnp[4].ToString()) * 4 +
                            Int32.Parse(cnp[5].ToString()) * 6 +
                            Int32.Parse(cnp[6].ToString()) * 3 +
                            Int32.Parse(cnp[7].ToString()) * 5 +
                            Int32.Parse(cnp[8].ToString()) * 8 +
                            Int32.Parse(cnp[9].ToString()) * 2 +
                            Int32.Parse(cnp[10].ToString()) * 7 +
                            Int32.Parse(cnp[11].ToString()) * 9;

                    int r = s % 11;

                    if (r == 10)
                    {
                        r = 1;
                    }

                    if (Int32.Parse(cnp[12].ToString()) != r)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
