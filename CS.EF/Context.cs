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
        public DbSet<Imobil> Imobile { get; set; }
        public DbSet<Proprietar> Proprietari { get; set; }
        public DbSet<ActProprietate> ActeProprietate { get; set; }
        public DbSet<Parcela> Parcele { get; set; }
        public DbSet<Tarla> Tarlale { get; set; }
        public DbSet<Inscriere> Inscrieri { get; set; }
        public DbSet<InscriereDetaliu> InscrieriDetaliu { get; set; }
        public DbSet<InscriereAct> InscrieriActe { get; set; }
        public DbSet<InscriereProprietar> InscrieriProprietari { get; set; }
        public DbSet<InscriereImobil> InscrieriImobile { get; set; }
        public DbSet<BaseXMLDictionary> Dictionar { get; set; }
        public DbSet<ModDobandire> ModuriDobandire { get; set; }
        public DbSet<TipActProprietate> TipuriActProprietate { get; set; }
        public DbSet<TipDrept> TipuriDrept { get; set; }
        public DbSet<TipInscriere> TipuriInscriere { get; set; }
        public DbSet<TipDocument> TipuriDocument { get; set; }
        public DbSet<Judet> Judete { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TipActProprietate>().HasData(
                new TipActProprietate { Id = 1, Denumire = "Titlu Proprietate", TipDocumentId = 290, TipDreptId = 23, ModDobandireId = 344, TipInscriereId = 316, ParteaCF = 2 },
                new TipActProprietate { Id = 2, Denumire = "Contract de Vanzare Cumparare", TipDocumentId = 289, TipDreptId = 23, ModDobandireId = 340, TipInscriereId = 316, ParteaCF = 2 },
                new TipActProprietate { Id = 3, Denumire = "Sentinta Civila", TipDocumentId = 293, TipDreptId = 23, ModDobandireId = 342, TipInscriereId = 316, ParteaCF = 2 },
                new TipActProprietate { Id = 4, Denumire = "Certificat de Mostenitor", TipDocumentId = 289, TipDreptId = 23, ModDobandireId = 345, TipInscriereId = 316, ParteaCF = 2 },
                new TipActProprietate { Id = 5, Denumire = "Contract de Donatie", TipDocumentId = 289, TipDreptId = 23, ModDobandireId = 340, TipInscriereId = 316, ParteaCF = 2 },
                new TipActProprietate { Id = 6, Denumire = "Contract de Partaj", TipDocumentId = 289, TipDreptId = 23, ModDobandireId = 343, TipInscriereId = 316, ParteaCF = 2 },
                new TipActProprietate { Id = 7, Denumire = "Testament", TipDocumentId = 289, TipDreptId = 23, ModDobandireId = 345, TipInscriereId = 316 },
                new TipActProprietate { Id = 8, Denumire = "Declaratie Notariala", TipDocumentId = 289 },
                new TipActProprietate { Id = 9, Denumire = "Contract de Intretinere", TipDocumentId = 289, TipDreptId = 20, ModDobandireId = 340, TipInscriereId = 316, ParteaCF = 3 },
                new TipActProprietate { Id = 10, Denumire = "Certificat de Legatar Suplimentar" }
                );

            modelBuilder.Entity<Tarla>().HasData(
                new Tarla { Id = 1, UATId = 120496, Denumire = "119,3" },
                new Tarla { Id = 2, UATId = 120496, Denumire = "339,1" },
                new Tarla { Id = 3, UATId = 120496, Denumire = "94,1" },
                new Tarla { Id = 4, UATId = 120496, Denumire = "79" },
                new Tarla { Id = 5, UATId = 120496, Denumire = "105,6" },
                new Tarla { Id = 6, UATId = 120496, Denumire = "393,3" },
                new Tarla { Id = 7, UATId = 120496, Denumire = "420,1" },
                new Tarla { Id = 8, UATId = 120496, Denumire = "160,1" },
                new Tarla { Id = 9, UATId = 120496, Denumire = "117" },
                new Tarla { Id = 10, UATId = 120496, Denumire = "480,1" },
                new Tarla { Id = 11, UATId = 120496, Denumire = "110" },
                new Tarla { Id = 12, UATId = 120496, Denumire = "161,1" },
                new Tarla { Id = 13, UATId = 120496, Denumire = "267,6" },
                new Tarla { Id = 14, UATId = 120496, Denumire = "72,1" },
                new Tarla { Id = 15, UATId = 120496, Denumire = "133,1" },
                new Tarla { Id = 16, UATId = 120496, Denumire = "267,4" },
                new Tarla { Id = 17, UATId = 120496, Denumire = "480,4" },
                new Tarla { Id = 18, UATId = 120496, Denumire = "161,3" },
                new Tarla { Id = 19, UATId = 120496, Denumire = "499,1" },
                new Tarla { Id = 20, UATId = 120496, Denumire = "75,1" },
                new Tarla { Id = 21, UATId = 120496, Denumire = "104,2" },
                new Tarla { Id = 22, UATId = 120496, Denumire = "341" },
                new Tarla { Id = 23, UATId = 120496, Denumire = "474,2" },
                new Tarla { Id = 24, UATId = 120496, Denumire = "162,2" },
                new Tarla { Id = 25, UATId = 120496, Denumire = "162,4" },
                new Tarla { Id = 26, UATId = 120496, Denumire = "169,1" },
                new Tarla { Id = 27, UATId = 120496, Denumire = "297.1.5" },
                new Tarla { Id = 28, UATId = 120496, Denumire = "479,1" },
                new Tarla { Id = 29, UATId = 120496, Denumire = "177,1" },
                new Tarla { Id = 30, UATId = 120496, Denumire = "134,1" },
                new Tarla { Id = 31, UATId = 120496, Denumire = "483,4" },
                new Tarla { Id = 32, UATId = 120496, Denumire = "383,1" },
                new Tarla { Id = 33, UATId = 120496, Denumire = "64" },
                new Tarla { Id = 34, UATId = 120496, Denumire = "140" },
                new Tarla { Id = 35, UATId = 120496, Denumire = "345,1" },
                new Tarla { Id = 36, UATId = 120496, Denumire = "445,1" },
                new Tarla { Id = 37, UATId = 120496, Denumire = "112,2" },
                new Tarla { Id = 38, UATId = 120496, Denumire = "114,1" },
                new Tarla { Id = 39, UATId = 120496, Denumire = "297.1.2" },
                new Tarla { Id = 40, UATId = 120496, Denumire = "474,1" },
                new Tarla { Id = 41, UATId = 120496, Denumire = "112,3" },
                new Tarla { Id = 42, UATId = 120496, Denumire = "105,3" },
                new Tarla { Id = 43, UATId = 120496, Denumire = "82,2" },
                new Tarla { Id = 44, UATId = 120496, Denumire = "479,2" },
                new Tarla { Id = 45, UATId = 120496, Denumire = "100" },
                new Tarla { Id = 46, UATId = 120496, Denumire = "76,1" },
                new Tarla { Id = 47, UATId = 120496, Denumire = "483,6" },
                new Tarla { Id = 48, UATId = 120496, Denumire = "51,5" },
                new Tarla { Id = 49, UATId = 120496, Denumire = "383,2" },
                new Tarla { Id = 50, UATId = 120496, Denumire = "297.1.6" },
                new Tarla { Id = 51, UATId = 120496, Denumire = "422,6" },
                new Tarla { Id = 52, UATId = 120496, Denumire = "134,2" },
                new Tarla { Id = 53, UATId = 120496, Denumire = "445,2" },
                new Tarla { Id = 54, UATId = 120496, Denumire = "483,5" },
                new Tarla { Id = 55, UATId = 120496, Denumire = "72,3" },
                new Tarla { Id = 56, UATId = 120496, Denumire = "304,2" },
                new Tarla { Id = 57, UATId = 120496, Denumire = "479,5" },
                new Tarla { Id = 58, UATId = 120496, Denumire = "133,2" },
                new Tarla { Id = 59, UATId = 120496, Denumire = "72,4" },
                new Tarla { Id = 60, UATId = 120496, Denumire = "303,2" },
                new Tarla { Id = 61, UATId = 120496, Denumire = "102,1" },
                new Tarla { Id = 62, UATId = 120496, Denumire = "114,2" },
                new Tarla { Id = 63, UATId = 120496, Denumire = "51,7" },
                new Tarla { Id = 64, UATId = 120496, Denumire = "119,5" },
                new Tarla { Id = 65, UATId = 120496, Denumire = "2420" },
                new Tarla { Id = 66, UATId = 120496, Denumire = "104,1" },
                new Tarla { Id = 67, UATId = 120496, Denumire = "112,1" },
                new Tarla { Id = 68, UATId = 120496, Denumire = "129" },
                new Tarla { Id = 69, UATId = 120496, Denumire = "161,6" },
                new Tarla { Id = 70, UATId = 120496, Denumire = "102,2" },
                new Tarla { Id = 71, UATId = 120496, Denumire = "393,2" },
                new Tarla { Id = 72, UATId = 120496, Denumire = "179,2" },
                new Tarla { Id = 73, UATId = 120496, Denumire = "140,4" },
                new Tarla { Id = 74, UATId = 120496, Denumire = "267.1.6" },
                new Tarla { Id = 75, UATId = 120496, Denumire = "422,4" },
                new Tarla { Id = 76, UATId = 120496, Denumire = "367,6" },
                new Tarla { Id = 77, UATId = 120496, Denumire = "136,2" },
                new Tarla { Id = 78, UATId = 120496, Denumire = "520,1" },
                new Tarla { Id = 79, UATId = 120496, Denumire = "119,1" },
                new Tarla { Id = 80, UATId = 120496, Denumire = "119,4" },
                new Tarla { Id = 81, UATId = 120496, Denumire = "134,3" },
                new Tarla { Id = 82, UATId = 120496, Denumire = "297,1,2" },
                new Tarla { Id = 83, UATId = 120496, Denumire = "329" },
                new Tarla { Id = 84, UATId = 120496, Denumire = "303,6" },
                new Tarla { Id = 85, UATId = 120496, Denumire = "51,6" },
                new Tarla { Id = 86, UATId = 120496, Denumire = "291.1.2" },
                new Tarla { Id = 87, UATId = 120496, Denumire = "422,1" },
                new Tarla { Id = 88, UATId = 120496, Denumire = "420,2" },
                new Tarla { Id = 89, UATId = 120496, Denumire = "520,4" },
                new Tarla { Id = 90, UATId = 120496, Denumire = "263,2" },
                new Tarla { Id = 91, UATId = 120496, Denumire = "483,8" },
                new Tarla { Id = 92, UATId = 120496, Denumire = "420,4" },
                new Tarla { Id = 93, UATId = 120496, Denumire = "119,2" },
                new Tarla { Id = 94, UATId = 120496, Denumire = "72,2" },
                new Tarla { Id = 95, UATId = 120496, Denumire = "105,2" },
                new Tarla { Id = 96, UATId = 120496, Denumire = "276,6" },
                new Tarla { Id = 97, UATId = 120496, Denumire = "445" },
                new Tarla { Id = 98, UATId = 120496, Denumire = "104" },
                new Tarla { Id = 99, UATId = 120496, Denumire = "383,3" },
                new Tarla { Id = 100, UATId = 120496, Denumire = "297,1,6" },
                new Tarla { Id = 101, UATId = 120496, Denumire = "383,5" },
                new Tarla { Id = 102, UATId = 120496, Denumire = "161,4" },
                new Tarla { Id = 103, UATId = 120496, Denumire = "179,1" },
                new Tarla { Id = 104, UATId = 120496, Denumire = "471,2" },
                new Tarla { Id = 105, UATId = 120496, Denumire = "161,5" },
                new Tarla { Id = 106, UATId = 120496, Denumire = "56,7" },
                new Tarla { Id = 107, UATId = 120496, Denumire = "267.3.4" },
                new Tarla { Id = 108, UATId = 120496, Denumire = "341,2" },
                new Tarla { Id = 109, UATId = 120496, Denumire = "76,2" },
                new Tarla { Id = 110, UATId = 120496, Denumire = "393,1" },
                new Tarla { Id = 111, UATId = 120496, Denumire = "479,3" }
            );

            CadSysContextExtensions.DictionaryTemplateAdditions(modelBuilder);
            CadSysContextExtensions.JudeteTemplateAdditions(modelBuilder);
            CadSysContextExtensions.UATTemplateAdditions(modelBuilder);
            CadSysContextExtensions.LocalitateTemplateAdditions(modelBuilder);
            CadSysContextExtensions.TipDreptTemplateAdditions(modelBuilder);
        }

    }

}
