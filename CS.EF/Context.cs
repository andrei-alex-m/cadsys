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
        public DbSet<Adresa> Adrese { get; set; }
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
            modelBuilder.Entity<Tarla>().HasData(
            new Tarla { Id = 2, UATId = 120496, Denumire = "339,1", Diminuata = true, Sector = "11" },
            new Tarla { Id = 3, UATId = 120496, Denumire = "94,1", Sector = "2" },
            new Tarla { Id = 4, UATId = 120496, Denumire = "79", Diminuata = true, Sector = "2" },
            new Tarla { Id = 5, UATId = 120496, Denumire = "105,6", Sector = "4" },
            new Tarla { Id = 6, UATId = 120496, Denumire = "393,3", Diminuata = true, Sector = "11" },
            new Tarla { Id = 7, UATId = 120496, Denumire = "420,1", Sector = "12" },
            new Tarla { Id = 9, UATId = 120496, Denumire = "117", Diminuata = true, Sector = "3" },
            new Tarla { Id = 10, UATId = 120496, Denumire = "480,1", Sector = "12" },
            new Tarla { Id = 11, UATId = 120496, Denumire = "110", Sector = "4" },
            new Tarla { Id = 14, UATId = 120496, Denumire = "72,1", Diminuata = true, Sector = "1" },
            new Tarla { Id = 15, UATId = 120496, Denumire = "133,1", Sector = "3" },
            new Tarla { Id = 17, UATId = 120496, Denumire = "480,4", Sector = "12" },
            new Tarla { Id = 19, UATId = 120496, Denumire = "499,1", Diminuata = true, Sector = "11" },
            new Tarla { Id = 20, UATId = 120496, Denumire = "75,1", Sector = "1" },
            new Tarla { Id = 21, UATId = 120496, Denumire = "104,2", Diminuata = true, Sector = "3" },
            new Tarla { Id = 22, UATId = 120496, Denumire = "341", Diminuata = true, Sector = "11" },
            new Tarla { Id = 23, UATId = 120496, Denumire = "474,2", Sector = "12" },
            new Tarla { Id = 28, UATId = 120496, Denumire = "479,1", Sector = "12" },
            new Tarla { Id = 30, UATId = 120496, Denumire = "134,1", Sector = "4" },
            new Tarla { Id = 31, UATId = 120496, Denumire = "483,4", Sector = "12" },
            new Tarla { Id = 32, UATId = 120496, Denumire = "383,1", Sector = "11" },
            new Tarla { Id = 33, UATId = 120496, Denumire = "64", Diminuata = true, Sector = "1" },
            new Tarla { Id = 35, UATId = 120496, Denumire = "345,1", Sector = "11" },
            new Tarla { Id = 36, UATId = 120496, Denumire = "445,1", Diminuata = true, Sector = "12" },
            new Tarla { Id = 37, UATId = 120496, Denumire = "112,2", Sector = "4" },
            new Tarla { Id = 38, UATId = 120496, Denumire = "114,1", Sector = "3" },
            new Tarla { Id = 40, UATId = 120496, Denumire = "474,1", Sector = "12" },
            new Tarla { Id = 41, UATId = 120496, Denumire = "112,3", Sector = "4" },
            new Tarla { Id = 42, UATId = 120496, Denumire = "105,3", Sector = "4" },
            new Tarla { Id = 43, UATId = 120496, Denumire = "82,2", Sector = "2" },
            new Tarla { Id = 44, UATId = 120496, Denumire = "479,2", Sector = "12" },
            new Tarla { Id = 45, UATId = 120496, Denumire = "100", Diminuata = true, Sector = "3" },
            new Tarla { Id = 46, UATId = 120496, Denumire = "76,1", Sector = "1" },
            new Tarla { Id = 47, UATId = 120496, Denumire = "483,6", Sector = "12" },
            new Tarla { Id = 48, UATId = 120496, Denumire = "51,5", Diminuata = true, Sector = "1" },
            new Tarla { Id = 49, UATId = 120496, Denumire = "383,2", Diminuata = true, Sector = "11" },
            new Tarla { Id = 51, UATId = 120496, Denumire = "422,6", Sector = "12" },
            new Tarla { Id = 52, UATId = 120496, Denumire = "134,2", Sector = "4" },
            new Tarla { Id = 53, UATId = 120496, Denumire = "445,2", Diminuata = true, Sector = "12" },
            new Tarla { Id = 54, UATId = 120496, Denumire = "483,5", Sector = "12" },
            new Tarla { Id = 55, UATId = 120496, Denumire = "72,3", Diminuata = true, Sector = "1" },
            new Tarla { Id = 57, UATId = 120496, Denumire = "479,5", Sector = "12" },
            new Tarla { Id = 58, UATId = 120496, Denumire = "133,2", Sector = "3" },
            new Tarla { Id = 59, UATId = 120496, Denumire = "72,4", Diminuata = true, Sector = "1" },
            new Tarla { Id = 61, UATId = 120496, Denumire = "102,1", Sector = "2" },
            new Tarla { Id = 62, UATId = 120496, Denumire = "114,2", Sector = "3" },
            new Tarla { Id = 63, UATId = 120496, Denumire = "51,7", Diminuata = true, Sector = "1" },
            new Tarla { Id = 66, UATId = 120496, Denumire = "104,1", Diminuata = true, Sector = "3" },
            new Tarla { Id = 67, UATId = 120496, Denumire = "112,1", Sector = "4" },
            new Tarla { Id = 70, UATId = 120496, Denumire = "102,2", Sector = "2" },
            new Tarla { Id = 71, UATId = 120496, Denumire = "393,2", Diminuata = true, Sector = "11" },
            new Tarla { Id = 75, UATId = 120496, Denumire = "422,4", Sector = "12" },
            new Tarla { Id = 81, UATId = 120496, Denumire = "134,3", Sector = "4" },
            new Tarla { Id = 83, UATId = 120496, Denumire = "329", Sector = "12" },
            new Tarla { Id = 85, UATId = 120496, Denumire = "51,6", Diminuata = true, Sector = "1" },
            new Tarla { Id = 87, UATId = 120496, Denumire = "422,1", Sector = "12" },
            new Tarla { Id = 88, UATId = 120496, Denumire = "420,2", Sector = "12" },
            new Tarla { Id = 91, UATId = 120496, Denumire = "483,8", Sector = "12" },
            new Tarla { Id = 92, UATId = 120496, Denumire = "420,4", Sector = "12" },
            new Tarla { Id = 94, UATId = 120496, Denumire = "72,2", Diminuata = true, Sector = "1" },
            new Tarla { Id = 95, UATId = 120496, Denumire = "105,2", Sector = "4" },
            new Tarla { Id = 97, UATId = 120496, Denumire = "445", Diminuata = true, Sector = "12" },
            new Tarla { Id = 98, UATId = 120496, Denumire = "104", Diminuata = true, Sector = "3" },
            new Tarla { Id = 99, UATId = 120496, Denumire = "383,3", Sector = "11" },
            new Tarla { Id = 104, UATId = 120496, Denumire = "471,2", Sector = "12" },
            new Tarla { Id = 106, UATId = 120496, Denumire = "56,7", Diminuata = true, Sector = "1" },
            new Tarla { Id = 108, UATId = 120496, Denumire = "341,2", Diminuata = true, Sector = "11" },
            new Tarla { Id = 109, UATId = 120496, Denumire = "76,2", Sector = "1" },
            new Tarla { Id = 110, UATId = 120496, Denumire = "393,1", Sector = "11" },
            new Tarla { Id = 111, UATId = 120496, Denumire = "479,3", Sector = "12" },
            new Tarla { Id = 112, UATId = 120496, Denumire = "331", Sector = "12" },
            new Tarla { Id = 113, UATId = 120496, Denumire = "331,1", Sector = "12" },
            new Tarla { Id = 114, UATId = 120496, Denumire = "408,1", Sector = "12" },
            new Tarla { Id = 115, UATId = 120496, Denumire = "422,2", Sector = "12" },
            new Tarla { Id = 116, UATId = 120496, Denumire = "440", Sector = "12" },
            new Tarla { Id = 117, UATId = 120496, Denumire = "448", Sector = "12" },
            new Tarla { Id = 118, UATId = 120496, Denumire = "450", Sector = "12" },
            new Tarla { Id = 119, UATId = 120496, Denumire = "451", Sector = "12" },
            new Tarla { Id = 120, UATId = 120496, Denumire = "456", Sector = "12" },
            new Tarla { Id = 121, UATId = 120496, Denumire = "457", Sector = "12" },
            new Tarla { Id = 122, UATId = 120496, Denumire = "486", Sector = "12" },
            new Tarla { Id = 123, UATId = 120496, Denumire = "488", Sector = "12" },
            new Tarla { Id = 130, UATId = 120496, Denumire = "119,1", Sector = "6", Diminuata = true },
            new Tarla { Id = 131, UATId = 120496, Denumire = "119,2", Sector = "6", Diminuata = true },
            new Tarla { Id = 132, UATId = 120496, Denumire = "119,3", Sector = "6", Diminuata = true },
            new Tarla { Id = 133, UATId = 120496, Denumire = "119,4", Sector = "6", Diminuata = true },
            new Tarla { Id = 134, UATId = 120496, Denumire = "133,1", Sector = "6" },
            new Tarla { Id = 135, UATId = 120496, Denumire = "136,2,1", Sector = "6" },
            new Tarla { Id = 136, UATId = 120496, Denumire = "136,2", Sector = "6" },
            new Tarla { Id = 137, UATId = 120496, Denumire = "140", Sector = "6" },
            new Tarla { Id = 138, UATId = 120496, Denumire = "140,4", Sector = "6" },
            new Tarla { Id = 139, UATId = 120496, Denumire = "161,1", Sector = "6" },
            new Tarla { Id = 140, UATId = 120496, Denumire = "161,3", Sector = "6" },
            new Tarla { Id = 141, UATId = 120496, Denumire = "160,1", Sector = "6" },
            new Tarla { Id = 142, UATId = 120496, Denumire = "179,2", Sector = "6" },
            new Tarla { Id = 143, UATId = 120496, Denumire = "179,1", Sector = "6" },
            new Tarla { Id = 144, UATId = 120496, Denumire = "177,1", Sector = "6" },
            new Tarla { Id = 145, UATId = 120496, Denumire = "169,1", Sector = "6" },
            new Tarla { Id = 146, UATId = 120496, Denumire = "162,2", Sector = "6" },
            new Tarla { Id = 147, UATId = 120496, Denumire = "162,4", Sector = "6" },
            new Tarla { Id = 148, UATId = 120496, Denumire = "267,6", Sector = "7" },
            new Tarla { Id = 149, UATId = 120496, Denumire = "267,4", Sector = "7" },
            new Tarla { Id = 150, UATId = 120496, Denumire = "297,1,6", Sector = "9", Diminuata = true },
            new Tarla { Id = 151, UATId = 120496, Denumire = "304,2", Sector = "9" },
            new Tarla { Id = 152, UATId = 120496, Denumire = "303,6", Sector = "9" },
            new Tarla { Id = 153, UATId = 120496, Denumire = "303,2", Sector = "9" },
            new Tarla { Id = 154, UATId = 120496, Denumire = "297,1,2", Sector = "9" },
            new Tarla { Id = 155, UATId = 120496, Denumire = "297,1,5", Sector = "9" }
            );

            CadSysContextExtensions.DictionaryTemplateAdditions(modelBuilder);
            CadSysContextExtensions.JudeteTemplateAdditions(modelBuilder);
            CadSysContextExtensions.UATTemplateAdditions(modelBuilder);
            CadSysContextExtensions.LocalitateTemplateAdditions(modelBuilder);
            CadSysContextExtensions.TipDreptTemplateAdditions(modelBuilder);

            modelBuilder.Entity<TipActProprietate>().HasData(
                new TipActProprietate { Id = 1, Denumire = "Titlu Proprietate", TipDocumentId = 290, TipDreptId = 23, ModDobandireId = 344, TipInscriereId = 316, ParteaCF = 2 },
                new TipActProprietate { Id = 2, Denumire = "Contract de Vanzare Cumparare", TipDocumentId = 289, TipDreptId = 23, ModDobandireId = 340, TipInscriereId = 316, ParteaCF = 2 },
                new TipActProprietate { Id = 3, Denumire = "Sentinta Civila", TipDocumentId = 293, TipDreptId = 23, ModDobandireId = 342, TipInscriereId = 316, ParteaCF = 2 },
                new TipActProprietate { Id = 4, Denumire = "Certificat de Mostenitor", TipDocumentId = 289, TipDreptId = 23, ModDobandireId = 345, TipInscriereId = 316, ParteaCF = 2 },
                new TipActProprietate { Id = 5, Denumire = "Contract de Donatie", TipDocumentId = 289, TipDreptId = 23, ModDobandireId = 340, TipInscriereId = 316, ParteaCF = 2 },
                new TipActProprietate { Id = 6, Denumire = "Contract de Partaj", TipDocumentId = 289, TipDreptId = 23, ModDobandireId = 343, TipInscriereId = 316, ParteaCF = 2 },
                new TipActProprietate { Id = 7, Denumire = "Testament", TipDocumentId = 289, TipDreptId = 23, ModDobandireId = 345, TipInscriereId = 316, ParteaCF = 2 },
                new TipActProprietate { Id = 8, Denumire = "Declaratie Notariala", TipDocumentId = 289 },
                new TipActProprietate { Id = 9, Denumire = "Contract de Intretinere", TipDocumentId = 289, TipDreptId = 20, ModDobandireId = 340, TipInscriereId = 316, ParteaCF = 3 },
                new TipActProprietate { Id = 10, Denumire = "Certificat de Legatar Suplimentar" },
                new TipActProprietate { Id = 11, Denumire = "Ordin", TipDocumentId = 290, TipDreptId = 23, ModDobandireId = 342, TipInscriereId = 316, ParteaCF = 2 },
                new TipActProprietate { Id = 12, Denumire = "Adeverinta", TipDocumentId = 290, TipDreptId = 23, ModDobandireId = 342, TipInscriereId = 316, ParteaCF = 2 },
                new TipActProprietate { Id = 13, Denumire = "Incheiere", TipDocumentId = 290, TipInscriereId = 317, ParteaCF=2 }
            );

            modelBuilder.Entity<Adresa>().HasData(
                new Adresa
                {
                    Id = 1,
                    SIRSUP = 120496,
                    SIRUTA = 120496,
                    Intravilan = false
                }
            );
        }

    }

}
