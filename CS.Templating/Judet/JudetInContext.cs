using CS.Data.Entities;
using Microsoft.EntityFrameworkCore;
namespace CS.EF
{
    public static partial class CadSysContextExtensions
    {
        public static void JudeteTemplateAdditions(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Judet>().HasData(
               new Judet{ Id = 10, Cod = "AB", Denumire = "ALBA", SIRUTA = 10, Activ = true},
               new Judet{ Id = 38, Cod = "AG", Denumire = "ARGES", SIRUTA = 38, Activ = true},
               new Judet{ Id = 29, Cod = "AR", Denumire = "ARAD", SIRUTA = 29, Activ = true},
               new Judet{ Id = 403, Cod = "B", Denumire = "BUCURESTI", SIRUTA = 403, Activ = true},
               new Judet{ Id = 47, Cod = "BC", Denumire = "BACAU", SIRUTA = 47, Activ = true},
               new Judet{ Id = 56, Cod = "BH", Denumire = "BIHOR", SIRUTA = 56, Activ = true},
               new Judet{ Id = 65, Cod = "BN", Denumire = "BISTRITA NASAUD", SIRUTA = 65, Activ = true},
               new Judet{ Id = 74, Cod = "BT", Denumire = "BOTOSANI", SIRUTA = 74, Activ = true},
               new Judet{ Id = 92, Cod = "BR", Denumire = "BRAILA", SIRUTA = 92, Activ = true},
               new Judet{ Id = 83, Cod = "BV", Denumire = "BRASOV", SIRUTA = 83, Activ = true},
               new Judet{ Id = 109, Cod = "BZ", Denumire = "BUZAU", SIRUTA = 109, Activ = true},
               new Judet{ Id = 519, Cod = "CL", Denumire = "CALARASI", SIRUTA = 519, Activ = true},
               new Judet{ Id = 118, Cod = "CS", Denumire = "CARASSEVERIN", SIRUTA = 118, Activ = true},
               new Judet{ Id = 127, Cod = "CJ", Denumire = "CLUJ", SIRUTA = 127, Activ = true},
               new Judet{ Id = 136, Cod = "CT", Denumire = "CONSTANTA", SIRUTA = 136, Activ = true},
               new Judet{ Id = 145, Cod = "CV", Denumire = "COVASNA", SIRUTA = 145, Activ = true},
               new Judet{ Id = 154, Cod = "DB", Denumire = "DAMBOVITA", SIRUTA = 154, Activ = true},
               new Judet{ Id = 163, Cod = "DJ", Denumire = "DOLJ", SIRUTA = 163, Activ = true},
               new Judet{ Id = 172, Cod = "GL", Denumire = "GALATI", SIRUTA = 172, Activ = true},
               new Judet{ Id = 528, Cod = "GR", Denumire = "GIURGIU", SIRUTA = 528, Activ = true},
               new Judet{ Id = 181, Cod = "GJ", Denumire = "GORJ", SIRUTA = 181, Activ = true},
               new Judet{ Id = 190, Cod = "HR", Denumire = "HARGHITA", SIRUTA = 190, Activ = true},
               new Judet{ Id = 207, Cod = "HD", Denumire = "HUNEDOARA", SIRUTA = 207, Activ = true},
               new Judet{ Id = 216, Cod = "IL", Denumire = "IALOMITA", SIRUTA = 216, Activ = true},
               new Judet{ Id = 225, Cod = "IS", Denumire = "IASI", SIRUTA = 225, Activ = true},
               new Judet{ Id = 234, Cod = "IF", Denumire = "ILFOV", SIRUTA = 234, Activ = true},
               new Judet{ Id = 243, Cod = "MM", Denumire = "MARAMURES", SIRUTA = 243, Activ = true},
               new Judet{ Id = 252, Cod = "MH", Denumire = "MEHEDINTI", SIRUTA = 252, Activ = true},
               new Judet{ Id = 261, Cod = "MS", Denumire = "MURES", SIRUTA = 261, Activ = true},
               new Judet{ Id = 270, Cod = "NT", Denumire = "NEAMT", SIRUTA = 270, Activ = true},
               new Judet{ Id = 289, Cod = "OT", Denumire = "OLT", SIRUTA = 289, Activ = true},
               new Judet{ Id = 298, Cod = "PH", Denumire = "PRAHOVA", SIRUTA = 298, Activ = true},
               new Judet{ Id = 314, Cod = "SJ", Denumire = "SALAJ", SIRUTA = 314, Activ = true},
               new Judet{ Id = 305, Cod = "SM", Denumire = "SATU MARE", SIRUTA = 305, Activ = true},
               new Judet{ Id = 323, Cod = "SB", Denumire = "SIBIU", SIRUTA = 323, Activ = true},
               new Judet{ Id = 332, Cod = "SV", Denumire = "SUCEAVA", SIRUTA = 332, Activ = true},
               new Judet{ Id = 341, Cod = "TR", Denumire = "TELEORMAN", SIRUTA = 341, Activ = true},
               new Judet{ Id = 350, Cod = "TM", Denumire = "TIMIS", SIRUTA = 350, Activ = true},
               new Judet{ Id = 369, Cod = "TL", Denumire = "TULCEA", SIRUTA = 369, Activ = true},
               new Judet{ Id = 387, Cod = "VL", Denumire = "VALCEA", SIRUTA = 387, Activ = true},
               new Judet{ Id = 378, Cod = "VS", Denumire = "VASLUI", SIRUTA = 378, Activ = true},
               new Judet{ Id = 396, Cod = "VN", Denumire = "VRANCEA", SIRUTA = 396, Activ = true}
            );
        }
    }
}
