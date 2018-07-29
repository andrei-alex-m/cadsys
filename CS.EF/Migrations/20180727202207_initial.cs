using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CS.EF.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    Numar = table.Column<long>(nullable: false),
                    Identificator = table.Column<long>(nullable: false),
                    Emitent = table.Column<string>(nullable: true),
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
                name: "Tara",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Denumire = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tara", x => x.Id);
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
                    TipActProprietateId = table.Column<int>(nullable: false),
                    Numar = table.Column<string>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false),
                    Emitent = table.Column<string>(nullable: true),
                    Suprafata = table.Column<decimal>(nullable: false),
                    Carnet = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActeProprietate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActeProprietate_TipuriActProprietate_TipActProprietateId",
                        column: x => x.TipActProprietateId,
                        principalTable: "TipuriActProprietate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "Parcele",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Index = table.Column<int>(nullable: false),
                    ExcelRow = table.Column<int>(nullable: false),
                    CatFol = table.Column<int>(nullable: false),
                    Denumire = table.Column<string>(nullable: false),
                    Suprafata = table.Column<decimal>(nullable: false),
                    TarlaId = table.Column<int>(nullable: false),
                    TarlaId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcele", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parcele_Tara_TarlaId",
                        column: x => x.TarlaId,
                        principalTable: "Tara",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Parcele_Tarlale_TarlaId1",
                        column: x => x.TarlaId1,
                        principalTable: "Tarlale",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Inscrieri",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Index = table.Column<int>(nullable: false),
                    ExcelRow = table.Column<int>(nullable: false),
                    IdProprietar = table.Column<int>(nullable: false),
                    IdActProprietate = table.Column<int>(nullable: false),
                    IdParcela = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inscrieri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inscrieri_ActeProprietate_IdActProprietate",
                        column: x => x.IdActProprietate,
                        principalTable: "ActeProprietate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inscrieri_Parcele_IdParcela",
                        column: x => x.IdParcela,
                        principalTable: "Parcele",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inscrieri_Proprietari_IdProprietar",
                        column: x => x.IdProprietar,
                        principalTable: "Proprietari",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    { 1, "Titlu Proptietate" },
                    { 2, "Contract de Vanzare Cumparare" },
                    { 3, "Sentinta Civila" },
                    { 4, "Certificat de Mostenitor" },
                    { 5, "Contract de Donatie" },
                    { 6, "Contract de Vanzare Cumparare cu Clauza de Intretinere" },
                    { 7, "Contract de Partaj Voluntar" },
                    { 8, "Act de Partaj Voluntar" },
                    { 9, "Contract de Partaj Imobiliar" },
                    { 10, "Act de Donatie" }
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
                    { 1, "100", 1 },
                    { 81, "471,2", 1 },
                    { 80, "445,2", 1 },
                    { 79, "445,1", 1 },
                    { 78, "445", 1 },
                    { 77, "422,6", 1 },
                    { 76, "422,4", 1 },
                    { 75, "422,1", 1 },
                    { 74, "420,4", 1 },
                    { 73, "420,2", 1 },
                    { 72, "420,1", 1 },
                    { 71, "393,3", 1 },
                    { 82, "474,1", 1 },
                    { 70, "393,2", 1 },
                    { 68, "383,5", 1 },
                    { 67, "383,3", 1 },
                    { 66, "383,2", 1 },
                    { 65, "383,1", 1 },
                    { 64, "367,6", 1 },
                    { 63, "345,1", 1 },
                    { 62, "341,2", 1 },
                    { 61, "341", 1 },
                    { 60, "339,1", 1 },
                    { 59, "329", 1 },
                    { 58, "304,2", 1 },
                    { 69, "393,1", 1 },
                    { 57, "303,6", 1 },
                    { 83, "474,2", 1 },
                    { 85, "479,2", 1 },
                    { 109, "79", 1 },
                    { 108, "76,2", 1 },
                    { 107, "76,1", 1 },
                    { 106, "75,1", 1 },
                    { 105, "72,4", 1 },
                    { 104, "72,3", 1 },
                    { 103, "72,2", 1 },
                    { 102, "72,1", 1 },
                    { 101, "64", 1 },
                    { 100, "56,7", 1 },
                    { 99, "520,4", 1 },
                    { 84, "479,1", 1 },
                    { 98, "520,1", 1 },
                    { 96, "51,6", 1 },
                    { 95, "51,5", 1 },
                    { 94, "499,1", 1 },
                    { 93, "483,8", 1 },
                    { 92, "483,6", 1 },
                    { 91, "483,5", 1 },
                    { 90, "483,4", 1 },
                    { 89, "480,4", 1 },
                    { 88, "480,1", 1 },
                    { 87, "479,5", 1 },
                    { 86, "479,3", 1 },
                    { 97, "51,7", 1 },
                    { 110, "82,2", 1 },
                    { 56, "303,2", 1 },
                    { 54, "297.1.5", 1 },
                    { 25, "134,1", 1 },
                    { 24, "133,2", 1 },
                    { 23, "133,1", 1 },
                    { 22, "129", 1 },
                    { 21, "119,5", 1 },
                    { 20, "119,4", 1 },
                    { 19, "119,3", 1 },
                    { 18, "119,2", 1 },
                    { 17, "119,1", 1 },
                    { 16, "117", 1 },
                    { 15, "114,2", 1 },
                    { 26, "134,2", 1 },
                    { 14, "114,1", 1 },
                    { 12, "112,2", 1 },
                    { 11, "112,1", 1 },
                    { 10, "110", 1 },
                    { 9, "105,6", 1 },
                    { 8, "105,3", 1 },
                    { 7, "105,2", 1 },
                    { 6, "104,2", 1 },
                    { 5, "104,1", 1 },
                    { 4, "104", 1 },
                    { 3, "102,2", 1 },
                    { 2, "102,1", 1 },
                    { 13, "112,3", 1 },
                    { 55, "297.1.6", 1 },
                    { 27, "134,3", 1 },
                    { 29, "140", 1 },
                    { 53, "297.1.2", 1 },
                    { 52, "297,1,6", 1 },
                    { 51, "297,1,2", 1 },
                    { 50, "291.1.2", 1 },
                    { 49, "276,6", 1 },
                    { 48, "267.3.4", 1 },
                    { 47, "267.1.6", 1 },
                    { 46, "267,6", 1 },
                    { 45, "267,4", 1 },
                    { 44, "263,2", 1 },
                    { 43, "2420", 1 },
                    { 28, "136,2", 1 },
                    { 42, "179,2", 1 },
                    { 40, "177,1", 1 },
                    { 39, "169,1", 1 },
                    { 38, "162,4", 1 },
                    { 37, "162,2", 1 },
                    { 36, "161,6", 1 },
                    { 35, "161,5", 1 },
                    { 34, "161,4", 1 },
                    { 33, "161,3", 1 },
                    { 32, "161,1", 1 },
                    { 31, "160,1", 1 },
                    { 30, "140,4", 1 },
                    { 41, "179,1", 1 },
                    { 111, "94,1", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActeProprietate_TipActProprietateId",
                table: "ActeProprietate",
                column: "TipActProprietateId");

            migrationBuilder.CreateIndex(
                name: "IX_Inscrieri_IdActProprietate",
                table: "Inscrieri",
                column: "IdActProprietate");

            migrationBuilder.CreateIndex(
                name: "IX_Inscrieri_IdParcela",
                table: "Inscrieri",
                column: "IdParcela");

            migrationBuilder.CreateIndex(
                name: "IX_Inscrieri_IdProprietar",
                table: "Inscrieri",
                column: "IdProprietar");

            migrationBuilder.CreateIndex(
                name: "IX_Parcele_TarlaId",
                table: "Parcele",
                column: "TarlaId");

            migrationBuilder.CreateIndex(
                name: "IX_Parcele_TarlaId1",
                table: "Parcele",
                column: "TarlaId1");

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
                name: "ActeProprietate");

            migrationBuilder.DropTable(
                name: "Parcele");

            migrationBuilder.DropTable(
                name: "Proprietari");

            migrationBuilder.DropTable(
                name: "TipuriActProprietate");

            migrationBuilder.DropTable(
                name: "Tara");

            migrationBuilder.DropTable(
                name: "Tarlale");

            migrationBuilder.DropTable(
                name: "UAT");

            migrationBuilder.DropTable(
                name: "Judet");
        }
    }
}
