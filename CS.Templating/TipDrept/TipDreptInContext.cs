
using CS.Data.Entities;
using Microsoft.EntityFrameworkCore;
namespace CS.EF
{
    public static partial class CadSysContextExtensions
    {
        public static void TipDreptTemplateAdditions(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TipDrept>().HasData(
               new TipDrept{ Id = 1, Denumire = "ADMINISTRARE", Partea2 = true, Partea3 = false, CotaObligatorie = false, RIGHTOWNERTYPE = 1, ModDobandireObligatoriu = false, ValoareaObligatorie = false},
               new TipDrept{ Id = 33, Denumire = "COMODAT", Partea2 = false, Partea3 = true, CotaObligatorie = false, RIGHTOWNERTYPE = 1, ModDobandireObligatoriu = false, ValoareaObligatorie = false},
               new TipDrept{ Id = 4, Denumire = "CONCESIUNE", Partea2 = true, Partea3 = true, CotaObligatorie = false, RIGHTOWNERTYPE = 2, ModDobandireObligatoriu = false, ValoareaObligatorie = false},
               new TipDrept{ Id = 11, Denumire = "FOLOSINTA", Partea2 = false, Partea3 = true, CotaObligatorie = false, RIGHTOWNERTYPE = 1, ModDobandireObligatoriu = false, ValoareaObligatorie = false},
               new TipDrept{ Id = 42, Denumire = "FOLOSINTA CU TITLU GRATUIT", Partea2 = true, Partea3 = false, CotaObligatorie = false, RIGHTOWNERTYPE = 1, ModDobandireObligatoriu = false, ValoareaObligatorie = false},
               new TipDrept{ Id = 32, Denumire = "FOLOSINTA SPECIALA", Partea2 = false, Partea3 = true, CotaObligatorie = false, RIGHTOWNERTYPE = 1, ModDobandireObligatoriu = false, ValoareaObligatorie = false},
               new TipDrept{ Id = 13, Denumire = "HABITATIE", Partea2 = false, Partea3 = true, CotaObligatorie = false, RIGHTOWNERTYPE = 1, ModDobandireObligatoriu = false, ValoareaObligatorie = false},
               new TipDrept{ Id = 14, Denumire = "INCHIRIERE", Partea2 = false, Partea3 = true, CotaObligatorie = false, RIGHTOWNERTYPE = 1, ModDobandireObligatoriu = false, ValoareaObligatorie = false},
               new TipDrept{ Id = 18, Denumire = "IPOTECA", Partea2 = false, Partea3 = true, CotaObligatorie = false, RIGHTOWNERTYPE = 1, ModDobandireObligatoriu = false, ValoareaObligatorie = true},
               new TipDrept{ Id = 41, Denumire = "IPOTECA LEGALA", Partea2 = false, Partea3 = true, CotaObligatorie = false, RIGHTOWNERTYPE = 1, ModDobandireObligatoriu = false, ValoareaObligatorie = false},
               new TipDrept{ Id = 34, Denumire = "LEASING IMOBILIAR", Partea2 = false, Partea3 = true, CotaObligatorie = false, RIGHTOWNERTYPE = 1, ModDobandireObligatoriu = false, ValoareaObligatorie = false},
               new TipDrept{ Id = 20, Denumire = "PRIVILEGIU IMOBILIAR", Partea2 = false, Partea3 = true, CotaObligatorie = false, RIGHTOWNERTYPE = 1, ModDobandireObligatoriu = false, ValoareaObligatorie = false},
               new TipDrept{ Id = 23, Denumire = "PROPRIETATE", Partea2 = true, Partea3 = false, CotaObligatorie = true, RIGHTOWNERTYPE = 1, ModDobandireObligatoriu = true, ValoareaObligatorie = false},
               new TipDrept{ Id = 27, Denumire = "SERVITUTE", Partea2 = true, Partea3 = true, CotaObligatorie = false, RIGHTOWNERTYPE = 2, ModDobandireObligatoriu = false, ValoareaObligatorie = false},
               new TipDrept{ Id = 29, Denumire = "SUPERFICIE", Partea2 = true, Partea3 = true, CotaObligatorie = false, RIGHTOWNERTYPE = 2, ModDobandireObligatoriu = false, ValoareaObligatorie = false},
               new TipDrept{ Id = 30, Denumire = "UZ", Partea2 = false, Partea3 = true, CotaObligatorie = false, RIGHTOWNERTYPE = 1, ModDobandireObligatoriu = false, ValoareaObligatorie = false},
               new TipDrept{ Id = 31, Denumire = "UZUFRUCT", Partea2 = false, Partea3 = true, CotaObligatorie = false, RIGHTOWNERTYPE = 1, ModDobandireObligatoriu = false, ValoareaObligatorie = false},
               new TipDrept{ Id = 35, Denumire = "UZUFRUCT VIAGER", Partea2 = false, Partea3 = true, CotaObligatorie = false, RIGHTOWNERTYPE = 1, ModDobandireObligatoriu = false, ValoareaObligatorie = false}
            );
        }
    }
}
