using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CS.EF.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Imobil",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SectorCadastral = table.Column<string>(maxLength: 200, nullable: true),
                    SuprafataMasurata = table.Column<decimal>(nullable: false),
                    SuprafataDinActe = table.Column<decimal>(nullable: false),
                    ImobilNou = table.Column<bool>(nullable: false),
                    SuprafataDinActeConstructii = table.Column<decimal>(nullable: false),
                    ValoareImpozitare = table.Column<decimal>(nullable: false),
                    Intravilan = table.Column<bool>(nullable: false),
                    Observatii = table.Column<string>(maxLength: 2000, nullable: true),
                    NumarCadastral = table.Column<string>(maxLength: 200, nullable: true),
                    NumarCarteFunciara = table.Column<string>(maxLength: 200, nullable: true),
                    NumarTopografic = table.Column<string>(maxLength: 200, nullable: true),
                    NumarCadGeneral = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imobil", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Judet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Denumire = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Judet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Localitate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Denumire = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localitate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Proprietari",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Index = table.Column<int>(nullable: false),
                    ExcelRow = table.Column<int>(nullable: false),
                    Nume = table.Column<string>(nullable: true),
                    Initiala = table.Column<string>(nullable: true),
                    Prenume = table.Column<string>(nullable: true),
                    TipActIdentitate = table.Column<int>(nullable: true),
                    Serie = table.Column<string>(nullable: true),
                    Numar = table.Column<string>(nullable: true),
                    Identificator = table.Column<long>(nullable: true),
                    Emitent = table.Column<string>(nullable: true),
                    DataEmiterii = table.Column<DateTime>(nullable: true),
                    Adresa = table.Column<string>(nullable: true),
                    Localitate = table.Column<string>(nullable: true),
                    Judet = table.Column<string>(nullable: true),
                    Tara = table.Column<string>(nullable: true),
                    TipPersoana = table.Column<int>(nullable: true),
                    Sex = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proprietari", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipuriActProprietate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Denumire = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipuriActProprietate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InscrieriDetaliu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Index = table.Column<int>(nullable: false),
                    ExcelRow = table.Column<int>(nullable: false),
                    TipInscriere = table.Column<string>(nullable: true),
                    IdTipDrept = table.Column<int>(nullable: false),
                    ObservatiiDrept = table.Column<string>(maxLength: 2000, nullable: true),
                    Nota = table.Column<string>(maxLength: 4000, nullable: true),
                    ModDobandire = table.Column<string>(maxLength: 50, nullable: true),
                    TipCota = table.Column<string>(maxLength: 50, nullable: true),
                    CotaInitiala = table.Column<string>(maxLength: 50, nullable: true),
                    CotaActuala = table.Column<string>(maxLength: 50, nullable: true),
                    Moneda = table.Column<string>(maxLength: 50, nullable: true),
                    Valoarea = table.Column<string>(maxLength: 50, nullable: true),
                    Observatii = table.Column<string>(maxLength: 2000, nullable: true),
                    ParteaCartiiFunciare = table.Column<int>(nullable: false),
                    Pozitia = table.Column<int>(nullable: false),
                    NumarulCererii = table.Column<int>(nullable: false),
                    DataCererii = table.Column<DateTime>(nullable: false),
                    IdImobilReferinta = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InscrieriDetaliu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InscrieriDetaliu_Imobil_IdImobilReferinta",
                        column: x => x.IdImobilReferinta,
                        principalTable: "Imobil",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UAT",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Denumire = table.Column<string>(nullable: false),
                    JudetId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UAT", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UAT_Judet_JudetId",
                        column: x => x.JudetId,
                        principalTable: "Judet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActeProprietate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Index = table.Column<int>(nullable: false),
                    ExcelRow = table.Column<int>(nullable: false),
                    IdTipActProprietate = table.Column<int>(nullable: true),
                    Numar = table.Column<string>(nullable: true),
                    Data = table.Column<DateTime>(nullable: true),
                    Emitent = table.Column<string>(nullable: true),
                    Suprafata = table.Column<decimal>(nullable: false),
                    Carnet = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActeProprietate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActeProprietate_TipuriActProprietate_IdTipActProprietate",
                        column: x => x.IdTipActProprietate,
                        principalTable: "TipuriActProprietate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tarlale",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Denumire = table.Column<string>(nullable: false),
                    UATId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarlale", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tarlale_UAT_UATId",
                        column: x => x.UATId,
                        principalTable: "UAT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inscrieri",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Index = table.Column<int>(nullable: false),
                    ExcelRow = table.Column<int>(nullable: false),
                    IdInscriereDetaliu = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    IdActProprietate = table.Column<int>(nullable: true),
                    IdImobil = table.Column<int>(nullable: true),
                    IdProprietar = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inscrieri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inscrieri_ActeProprietate_IdActProprietate",
                        column: x => x.IdActProprietate,
                        principalTable: "ActeProprietate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inscrieri_InscrieriDetaliu_IdInscriereDetaliu",
                        column: x => x.IdInscriereDetaliu,
                        principalTable: "InscrieriDetaliu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inscrieri_Imobil_IdImobil",
                        column: x => x.IdImobil,
                        principalTable: "Imobil",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inscrieri_InscrieriDetaliu_IdInscriereDetaliu1",
                        column: x => x.IdInscriereDetaliu,
                        principalTable: "InscrieriDetaliu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inscrieri_InscrieriDetaliu_IdInscriereDetaliu2",
                        column: x => x.IdInscriereDetaliu,
                        principalTable: "InscrieriDetaliu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inscrieri_Proprietari_IdProprietar",
                        column: x => x.IdProprietar,
                        principalTable: "Proprietari",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Parcele",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Index = table.Column<int>(nullable: false),
                    ExcelRow = table.Column<int>(nullable: false),
                    CatFol = table.Column<int>(nullable: true),
                    Denumire = table.Column<string>(nullable: true),
                    Suprafata = table.Column<int>(nullable: true),
                    IdTarla = table.Column<int>(nullable: true),
                    IdImobil = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcele", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parcele_Imobil_IdImobil",
                        column: x => x.IdImobil,
                        principalTable: "Imobil",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Parcele_Tarlale_IdTarla",
                        column: x => x.IdTarla,
                        principalTable: "Tarlale",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Judet",
                columns: new[] { "Id", "Denumire" },
                values: new object[] { 1, "Ialomita" });

            migrationBuilder.InsertData(
                table: "Localitate",
                columns: new[] { "Id", "Denumire" },
                values: new object[] { 1, "Sarateni" });

            migrationBuilder.InsertData(
                table: "TipuriActProprietate",
                columns: new[] { "Id", "Denumire" },
                values: new object[,]
                {
                    { 15, "Testament" },
                    { 14, "Contract de Partaj Imobiliar" },
                    { 13, "Act de Partaj Voluntar" },
                    { 12, "Contract de Partaj Voluntar" },
                    { 11, "Contract de Partaj" },
                    { 10, "Contract de Donatie Imobiliara" },
                    { 9, "Contract de Donatie" },
                    { 8, "Act de Donatie" },
                    { 7, "Certificat de Mostenitor Suplimentar" },
                    { 6, "Certificat de Mostenitor Succesiv" },
                    { 5, "Certificat de Mostenitor" },
                    { 4, "Sentinta Civila" },
                    { 3, "Contract de Vanzare Cumparare cu Clauza de Intretinere" },
                    { 2, "Contract de Vanzare Cumparare" },
                    { 1, "Titlu Proprietate" },
                    { 16, "Declaratie Notariala" },
                    { 17, "Certificat de Legatar Suplimentar" }
                });

            migrationBuilder.InsertData(
                table: "UAT",
                columns: new[] { "Id", "Denumire", "JudetId" },
                values: new object[] { 1, "Sarateni", 1 });

            migrationBuilder.InsertData(
                table: "Tarlale",
                columns: new[] { "Id", "Denumire", "UATId" },
                values: new object[,]
                {
                    { 1, "119,3", 1 },
                    { 81, "134,3", 1 },
                    { 80, "119,4", 1 },
                    { 79, "119,1", 1 },
                    { 78, "520,1", 1 },
                    { 77, "136,2", 1 },
                    { 76, "367,6", 1 },
                    { 75, "422,4", 1 },
                    { 74, "267.1.6", 1 },
                    { 73, "140,4", 1 },
                    { 72, "179,2", 1 },
                    { 71, "393,2", 1 },
                    { 82, "297,1,2", 1 },
                    { 70, "102,2", 1 },
                    { 68, "129", 1 },
                    { 67, "112,1", 1 },
                    { 66, "104,1", 1 },
                    { 65, "2420", 1 },
                    { 64, "119,5", 1 },
                    { 63, "51,7", 1 },
                    { 62, "114,2", 1 },
                    { 61, "102,1", 1 },
                    { 60, "303,2", 1 },
                    { 59, "72,4", 1 },
                    { 58, "133,2", 1 },
                    { 69, "161,6", 1 },
                    { 57, "479,5", 1 },
                    { 83, "329", 1 },
                    { 85, "51,6", 1 },
                    { 109, "76,2", 1 },
                    { 108, "341,2", 1 },
                    { 107, "267.3.4", 1 },
                    { 106, "56,7", 1 },
                    { 105, "161,5", 1 },
                    { 104, "471,2", 1 },
                    { 103, "179,1", 1 },
                    { 102, "161,4", 1 },
                    { 101, "383,5", 1 },
                    { 100, "297,1,6", 1 },
                    { 99, "383,3", 1 },
                    { 84, "303,6", 1 },
                    { 98, "104", 1 },
                    { 96, "276,6", 1 },
                    { 95, "105,2", 1 },
                    { 94, "72,2", 1 },
                    { 93, "119,2", 1 },
                    { 92, "420,4", 1 },
                    { 91, "483,8", 1 },
                    { 90, "263,2", 1 },
                    { 89, "520,4", 1 },
                    { 88, "420,2", 1 },
                    { 87, "422,1", 1 },
                    { 86, "291.1.2", 1 },
                    { 97, "445", 1 },
                    { 110, "393,1", 1 },
                    { 56, "304,2", 1 },
                    { 54, "483,5", 1 },
                    { 25, "162,4", 1 },
                    { 24, "162,2", 1 },
                    { 23, "474,2", 1 },
                    { 22, "341", 1 },
                    { 21, "104,2", 1 },
                    { 20, "75,1", 1 },
                    { 19, "499,1", 1 },
                    { 18, "161,3", 1 },
                    { 17, "480,4", 1 },
                    { 16, "267,4", 1 },
                    { 15, "133,1", 1 },
                    { 26, "169,1", 1 },
                    { 14, "72,1", 1 },
                    { 12, "161,1", 1 },
                    { 11, "110", 1 },
                    { 10, "480,1", 1 },
                    { 9, "117", 1 },
                    { 8, "160,1", 1 },
                    { 7, "420,1", 1 },
                    { 6, "393,3", 1 },
                    { 5, "105,6", 1 },
                    { 4, "79", 1 },
                    { 3, "94,1", 1 },
                    { 2, "339,1", 1 },
                    { 13, "267,6", 1 },
                    { 55, "72,3", 1 },
                    { 27, "297.1.5", 1 },
                    { 29, "177,1", 1 },
                    { 53, "445,2", 1 },
                    { 52, "134,2", 1 },
                    { 51, "422,6", 1 },
                    { 50, "297.1.6", 1 },
                    { 49, "383,2", 1 },
                    { 48, "51,5", 1 },
                    { 47, "483,6", 1 },
                    { 46, "76,1", 1 },
                    { 45, "100", 1 },
                    { 44, "479,2", 1 },
                    { 43, "82,2", 1 },
                    { 28, "479,1", 1 },
                    { 42, "105,3", 1 },
                    { 40, "474,1", 1 },
                    { 39, "297.1.2", 1 },
                    { 38, "114,1", 1 },
                    { 37, "112,2", 1 },
                    { 36, "445,1", 1 },
                    { 35, "345,1", 1 },
                    { 34, "140", 1 },
                    { 33, "64", 1 },
                    { 32, "383,1", 1 },
                    { 31, "483,4", 1 },
                    { 30, "134,1", 1 },
                    { 41, "112,3", 1 },
                    { 111, "479,3", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActeProprietate_IdTipActProprietate",
                table: "ActeProprietate",
                column: "IdTipActProprietate");

            migrationBuilder.CreateIndex(
                name: "IX_Inscrieri_IdActProprietate",
                table: "Inscrieri",
                column: "IdActProprietate");

            migrationBuilder.CreateIndex(
                name: "IX_Inscrieri_IdInscriereDetaliu",
                table: "Inscrieri",
                column: "IdInscriereDetaliu");

            migrationBuilder.CreateIndex(
                name: "IX_Inscrieri_IdImobil",
                table: "Inscrieri",
                column: "IdImobil");

            migrationBuilder.CreateIndex(
                name: "IX_Inscrieri_IdInscriereDetaliu1",
                table: "Inscrieri",
                column: "IdInscriereDetaliu");

            migrationBuilder.CreateIndex(
                name: "IX_Inscrieri_IdInscriereDetaliu2",
                table: "Inscrieri",
                column: "IdInscriereDetaliu");

            migrationBuilder.CreateIndex(
                name: "IX_Inscrieri_IdProprietar",
                table: "Inscrieri",
                column: "IdProprietar");

            migrationBuilder.CreateIndex(
                name: "IX_InscrieriDetaliu_IdImobilReferinta",
                table: "InscrieriDetaliu",
                column: "IdImobilReferinta");

            migrationBuilder.CreateIndex(
                name: "IX_Parcele_IdImobil",
                table: "Parcele",
                column: "IdImobil");

            migrationBuilder.CreateIndex(
                name: "IX_Parcele_IdTarla",
                table: "Parcele",
                column: "IdTarla");

            migrationBuilder.CreateIndex(
                name: "IX_Tarlale_UATId",
                table: "Tarlale",
                column: "UATId");

            migrationBuilder.CreateIndex(
                name: "IX_UAT_JudetId",
                table: "UAT",
                column: "JudetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inscrieri");

            migrationBuilder.DropTable(
                name: "Localitate");

            migrationBuilder.DropTable(
                name: "Parcele");

            migrationBuilder.DropTable(
                name: "ActeProprietate");

            migrationBuilder.DropTable(
                name: "InscrieriDetaliu");

            migrationBuilder.DropTable(
                name: "Proprietari");

            migrationBuilder.DropTable(
                name: "Tarlale");

            migrationBuilder.DropTable(
                name: "TipuriActProprietate");

            migrationBuilder.DropTable(
                name: "Imobil");

            migrationBuilder.DropTable(
                name: "UAT");

            migrationBuilder.DropTable(
                name: "Judet");
        }
    }
}
