using CS.Data.Entities;
using Microsoft.EntityFrameworkCore;
using CS.Templating;


namespace CS.EF
{
    public class CadSysContext : DbContext
    {
        public CadSysContext(DbContextOptions<CadSysContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Proprietar> Proprietari { get; set; }
        public DbSet<ActProprietate> ActeProprietate { get; set; }
        public DbSet<Parcela> Parcele { get; set; }
        public DbSet<Tarla> Tarlale { get; set; }
        public DbSet<TipActProprietate> TipuriActProprietate { get; set; }
        public DbSet<Inscriere> Inscrieri { get; set; }
        public DbSet<InscriereDetaliu> InscrieriDetaliu { get; set; }
        public DbSet<InscriereAct> InscrieriActe { get; set; }
        public DbSet<InscriereProprietar> InscrieriProprietari { get; set; }
        public DbSet<InscriereImobil> InscrieriImobile { get; set; }
        public DbSet<BaseXMLDictionary> Dictionar { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<TipActProprietate>().HasData(
                new TipActProprietate { Id = 1, Denumire = "Titlu Proprietate" },
                new TipActProprietate { Id = 2, Denumire = "Contract de Vanzare Cumparare" },
                new TipActProprietate { Id = 3, Denumire = "Contract de Vanzare Cumparare cu Clauza de Intretinere" },
                new TipActProprietate { Id = 4, Denumire = "Sentinta Civila" },
                new TipActProprietate { Id = 5, Denumire = "Certificat de Mostenitor" },
                new TipActProprietate { Id = 6, Denumire = "Certificat de Mostenitor Succesiv" },
                new TipActProprietate { Id = 7, Denumire = "Certificat de Mostenitor Suplimentar" },
                new TipActProprietate { Id = 8, Denumire = "Act de Donatie" },
                new TipActProprietate { Id = 9, Denumire = "Contract de Donatie" },
                new TipActProprietate { Id = 10, Denumire = "Contract de Donatie Imobiliara" },
                new TipActProprietate { Id = 11, Denumire = "Contract de Partaj" },
                new TipActProprietate { Id = 12, Denumire = "Contract de Partaj Voluntar" },
                new TipActProprietate { Id = 13, Denumire = "Act de Partaj Voluntar" },
                new TipActProprietate { Id = 14, Denumire = "Contract de Partaj Imobiliar" },
                new TipActProprietate { Id = 15, Denumire = "Testament" },
                new TipActProprietate { Id = 16, Denumire = "Declaratie Notariala" },
                new TipActProprietate { Id = 17, Denumire = "Certificat de Legatar Suplimentar" }
                );

            //modelBuilder.Entity<Judet>().HasData(
            //    new Judet { Id = 1, Denumire = "Ialomita" }
            //);

            //modelBuilder.Entity<UAT>().HasData(
            //    new UAT { Id = 1, JudetId = 1, Denumire = "Sarateni" }
            //);

            //modelBuilder.Entity<Localitate>().HasData(
            //    new Localitate { Id = 1, Denumire = "Sarateni" }
            //);

            modelBuilder.Entity<Tarla>().HasData(
                new Tarla { Id = 1, IdUAT = 120496, Denumire = "119,3" },
                new Tarla { Id = 2, IdUAT = 120496, Denumire = "339,1" },
                new Tarla { Id = 3, IdUAT = 120496, Denumire = "94,1" },
                new Tarla { Id = 4, IdUAT = 120496, Denumire = "79" },
                new Tarla { Id = 5, IdUAT = 120496, Denumire = "105,6" },
                new Tarla { Id = 6, IdUAT = 120496, Denumire = "393,3" },
                new Tarla { Id = 7, IdUAT = 120496, Denumire = "420,1" },
                new Tarla { Id = 8, IdUAT = 120496, Denumire = "160,1" },
                new Tarla { Id = 9, IdUAT = 120496, Denumire = "117" },
                new Tarla { Id = 10, IdUAT = 120496, Denumire = "480,1" },
                new Tarla { Id = 11, IdUAT = 120496, Denumire = "110" },
                new Tarla { Id = 12, IdUAT = 120496, Denumire = "161,1" },
                new Tarla { Id = 13, IdUAT = 120496, Denumire = "267,6" },
                new Tarla { Id = 14, IdUAT = 120496, Denumire = "72,1" },
                new Tarla { Id = 15, IdUAT = 120496, Denumire = "133,1" },
                new Tarla { Id = 16, IdUAT = 120496, Denumire = "267,4" },
                new Tarla { Id = 17, IdUAT = 120496, Denumire = "480,4" },
                new Tarla { Id = 18, IdUAT = 120496, Denumire = "161,3" },
                new Tarla { Id = 19, IdUAT = 120496, Denumire = "499,1" },
                new Tarla { Id = 20, IdUAT = 120496, Denumire = "75,1" },
                new Tarla { Id = 21, IdUAT = 120496, Denumire = "104,2" },
                new Tarla { Id = 22, IdUAT = 120496, Denumire = "341" },
                new Tarla { Id = 23, IdUAT = 120496, Denumire = "474,2" },
                new Tarla { Id = 24, IdUAT = 120496, Denumire = "162,2" },
                new Tarla { Id = 25, IdUAT = 120496, Denumire = "162,4" },
                new Tarla { Id = 26, IdUAT = 120496, Denumire = "169,1" },
                new Tarla { Id = 27, IdUAT = 120496, Denumire = "297.1.5" },
                new Tarla { Id = 28, IdUAT = 120496, Denumire = "479,1" },
                new Tarla { Id = 29, IdUAT = 120496, Denumire = "177,1" },
                new Tarla { Id = 30, IdUAT = 120496, Denumire = "134,1" },
                new Tarla { Id = 31, IdUAT = 120496, Denumire = "483,4" },
                new Tarla { Id = 32, IdUAT = 120496, Denumire = "383,1" },
                new Tarla { Id = 33, IdUAT = 120496, Denumire = "64" },
                new Tarla { Id = 34, IdUAT = 120496, Denumire = "140" },
                new Tarla { Id = 35, IdUAT = 120496, Denumire = "345,1" },
                new Tarla { Id = 36, IdUAT = 120496, Denumire = "445,1" },
                new Tarla { Id = 37, IdUAT = 120496, Denumire = "112,2" },
                new Tarla { Id = 38, IdUAT = 120496, Denumire = "114,1" },
                new Tarla { Id = 39, IdUAT = 120496, Denumire = "297.1.2" },
                new Tarla { Id = 40, IdUAT = 120496, Denumire = "474,1" },
                new Tarla { Id = 41, IdUAT = 120496, Denumire = "112,3" },
                new Tarla { Id = 42, IdUAT = 120496, Denumire = "105,3" },
                new Tarla { Id = 43, IdUAT = 120496, Denumire = "82,2" },
                new Tarla { Id = 44, IdUAT = 120496, Denumire = "479,2" },
                new Tarla { Id = 45, IdUAT = 120496, Denumire = "100" },
                new Tarla { Id = 46, IdUAT = 120496, Denumire = "76,1" },
                new Tarla { Id = 47, IdUAT = 120496, Denumire = "483,6" },
                new Tarla { Id = 48, IdUAT = 120496, Denumire = "51,5" },
                new Tarla { Id = 49, IdUAT = 120496, Denumire = "383,2" },
                new Tarla { Id = 50, IdUAT = 120496, Denumire = "297.1.6" },
                new Tarla { Id = 51, IdUAT = 120496, Denumire = "422,6" },
                new Tarla { Id = 52, IdUAT = 120496, Denumire = "134,2" },
                new Tarla { Id = 53, IdUAT = 120496, Denumire = "445,2" },
                new Tarla { Id = 54, IdUAT = 120496, Denumire = "483,5" },
                new Tarla { Id = 55, IdUAT = 120496, Denumire = "72,3" },
                new Tarla { Id = 56, IdUAT = 120496, Denumire = "304,2" },
                new Tarla { Id = 57, IdUAT = 120496, Denumire = "479,5" },
                new Tarla { Id = 58, IdUAT = 120496, Denumire = "133,2" },
                new Tarla { Id = 59, IdUAT = 120496, Denumire = "72,4" },
                new Tarla { Id = 60, IdUAT = 120496, Denumire = "303,2" },
                new Tarla { Id = 61, IdUAT = 120496, Denumire = "102,1" },
                new Tarla { Id = 62, IdUAT = 120496, Denumire = "114,2" },
                new Tarla { Id = 63, IdUAT = 120496, Denumire = "51,7" },
                new Tarla { Id = 64, IdUAT = 120496, Denumire = "119,5" },
                new Tarla { Id = 65, IdUAT = 120496, Denumire = "2420" },
                new Tarla { Id = 66, IdUAT = 120496, Denumire = "104,1" },
                new Tarla { Id = 67, IdUAT = 120496, Denumire = "112,1" },
                new Tarla { Id = 68, IdUAT = 120496, Denumire = "129" },
                new Tarla { Id = 69, IdUAT = 120496, Denumire = "161,6" },
                new Tarla { Id = 70, IdUAT = 120496, Denumire = "102,2" },
                new Tarla { Id = 71, IdUAT = 120496, Denumire = "393,2" },
                new Tarla { Id = 72, IdUAT = 120496, Denumire = "179,2" },
                new Tarla { Id = 73, IdUAT = 120496, Denumire = "140,4" },
                new Tarla { Id = 74, IdUAT = 120496, Denumire = "267.1.6" },
                new Tarla { Id = 75, IdUAT = 120496, Denumire = "422,4" },
                new Tarla { Id = 76, IdUAT = 120496, Denumire = "367,6" },
                new Tarla { Id = 77, IdUAT = 120496, Denumire = "136,2" },
                new Tarla { Id = 78, IdUAT = 120496, Denumire = "520,1" },
                new Tarla { Id = 79, IdUAT = 120496, Denumire = "119,1" },
                new Tarla { Id = 80, IdUAT = 120496, Denumire = "119,4" },
                new Tarla { Id = 81, IdUAT = 120496, Denumire = "134,3" },
                new Tarla { Id = 82, IdUAT = 120496, Denumire = "297,1,2" },
                new Tarla { Id = 83, IdUAT = 120496, Denumire = "329" },
                new Tarla { Id = 84, IdUAT = 120496, Denumire = "303,6" },
                new Tarla { Id = 85, IdUAT = 120496, Denumire = "51,6" },
                new Tarla { Id = 86, IdUAT = 120496, Denumire = "291.1.2" },
                new Tarla { Id = 87, IdUAT = 120496, Denumire = "422,1" },
                new Tarla { Id = 88, IdUAT = 120496, Denumire = "420,2" },
                new Tarla { Id = 89, IdUAT = 120496, Denumire = "520,4" },
                new Tarla { Id = 90, IdUAT = 120496, Denumire = "263,2" },
                new Tarla { Id = 91, IdUAT = 120496, Denumire = "483,8" },
                new Tarla { Id = 92, IdUAT = 120496, Denumire = "420,4" },
                new Tarla { Id = 93, IdUAT = 120496, Denumire = "119,2" },
                new Tarla { Id = 94, IdUAT = 120496, Denumire = "72,2" },
                new Tarla { Id = 95, IdUAT = 120496, Denumire = "105,2" },
                new Tarla { Id = 96, IdUAT = 120496, Denumire = "276,6" },
                new Tarla { Id = 97, IdUAT = 120496, Denumire = "445" },
                new Tarla { Id = 98, IdUAT = 120496, Denumire = "104" },
                new Tarla { Id = 99, IdUAT = 120496, Denumire = "383,3" },
                new Tarla { Id = 100, IdUAT = 120496, Denumire = "297,1,6" },
                new Tarla { Id = 101, IdUAT = 120496, Denumire = "383,5" },
                new Tarla { Id = 102, IdUAT = 120496, Denumire = "161,4" },
                new Tarla { Id = 103, IdUAT = 120496, Denumire = "179,1" },
                new Tarla { Id = 104, IdUAT = 120496, Denumire = "471,2" },
                new Tarla { Id = 105, IdUAT = 120496, Denumire = "161,5" },
                new Tarla { Id = 106, IdUAT = 120496, Denumire = "56,7" },
                new Tarla { Id = 107, IdUAT = 120496, Denumire = "267.3.4" },
                new Tarla { Id = 108, IdUAT = 120496, Denumire = "341,2" },
                new Tarla { Id = 109, IdUAT = 120496, Denumire = "76,2" },
                new Tarla { Id = 110, IdUAT = 120496, Denumire = "393,1" },
                new Tarla { Id = 111, IdUAT = 120496, Denumire = "479,3" }
            );

            CadSysContextExtensions.DictionaryTemplateAdditions(modelBuilder);
            CadSysContextExtensions.JudeteTemplateAdditions(modelBuilder);
            CadSysContextExtensions.UATTemplateAdditions(modelBuilder);
            CadSysContextExtensions.LocalitateTemplateAdditions(modelBuilder);
            CadSysContextExtensions.TipDreptTemplateAdditions(modelBuilder);
        }

    }

}
