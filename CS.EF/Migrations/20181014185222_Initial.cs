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
                name: "Dictionar",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Denumire = table.Column<string>(nullable: false),
                    Descriere = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dictionar", x => x.Id);
                });

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
                    Denumire = table.Column<string>(nullable: false),
                    Descriere = table.Column<string>(nullable: true)
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
                    Denumire = table.Column<string>(nullable: false),
                    Descriere = table.Column<string>(nullable: true)
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
                    Denumire = table.Column<string>(nullable: false),
                    Descriere = table.Column<string>(nullable: true)
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
                    Descriere = table.Column<string>(nullable: true),
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
                    Descriere = table.Column<string>(nullable: true),
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
                    CotaParte = table.Column<string>(nullable: true),
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
                table: "Dictionar",
                columns: new[] { "Id", "Denumire", "Descriere", "Discriminator" },
                values: new object[,]
                {
                    { 1, "CA", "constructii anexa", "BUILDDEST" },
                    { 246, "TZ", "Tanzania, United Republic Of", "COUNTRY" },
                    { 245, "TW", "Taiwan, Province Of China", "COUNTRY" },
                    { 244, "TV", "Tuvalu", "COUNTRY" },
                    { 243, "TT", "Trinidad And Tobago", "COUNTRY" },
                    { 242, "TR", "Turkey", "COUNTRY" },
                    { 241, "TO", "Tonga", "COUNTRY" },
                    { 240, "TN", "Tunisia", "COUNTRY" },
                    { 239, "TM", "Turkmenistan", "COUNTRY" },
                    { 247, "UA", "Ukraine", "COUNTRY" },
                    { 238, "TL", "Timor-Leste", "COUNTRY" },
                    { 236, "TJ", "Tajikistan", "COUNTRY" },
                    { 235, "TH", "Thailand", "COUNTRY" },
                    { 234, "TG", "Togo", "COUNTRY" },
                    { 233, "TF", "French Southern Territories", "COUNTRY" },
                    { 232, "TD", "Chad", "COUNTRY" },
                    { 231, "TC", "Turks And Caicos Islands", "COUNTRY" },
                    { 230, "SZ", "Swaziland", "COUNTRY" },
                    { 229, "SY", "Syrian Arab Republic", "COUNTRY" },
                    { 237, "TK", "Tokelau", "COUNTRY" },
                    { 248, "UG", "Uganda", "COUNTRY" },
                    { 249, "UM", "United States Minor Outlying Islands", "COUNTRY" },
                    { 250, "US", "United States", "COUNTRY" },
                    { 269, "CHF", "CHF", "CURRENCY" },
                    { 268, "CAD", "CAD", "CURRENCY" },
                    { 267, "ATS", "ATS", "CURRENCY" },
                    { 266, "ZW", "Zimbabwe", "COUNTRY" },
                    { 265, "ZM", "Zambia", "COUNTRY" },
                    { 264, "ZA", "South Africa", "COUNTRY" },
                    { 263, "YT", "Mayotte", "COUNTRY" },
                    { 262, "YE", "Yemen", "COUNTRY" },
                    { 261, "WS", "Samoa", "COUNTRY" },
                    { 260, "WF", "Wallis And Futuna", "COUNTRY" },
                    { 259, "VU", "Vanuatu", "COUNTRY" },
                    { 258, "VN", "Viet Nam", "COUNTRY" },
                    { 257, "VI", "Virgin Islands, U.S.", "COUNTRY" },
                    { 256, "VG", "Virgin Islands, British", "COUNTRY" },
                    { 255, "VE", "Venezuela", "COUNTRY" },
                    { 254, "VC", "Saint Vincent And The Grenadines", "COUNTRY" },
                    { 253, "VA", "Holy See (Vatican City State)", "COUNTRY" },
                    { 252, "UZ", "Uzbekistan", "COUNTRY" },
                    { 251, "UY", "Uruguay", "COUNTRY" },
                    { 228, "SV", "El Salvador", "COUNTRY" },
                    { 270, "DEM", "DEM", "CURRENCY" },
                    { 227, "ST", "Sao Tome And Principe", "COUNTRY" },
                    { 225, "SO", "Somalia", "COUNTRY" },
                    { 201, "PN", "Pitcairn", "COUNTRY" },
                    { 200, "PM", "Saint Pierre And Miquelon", "COUNTRY" },
                    { 199, "PL", "Poland", "COUNTRY" },
                    { 198, "PK", "Pakistan", "COUNTRY" },
                    { 197, "PH", "Philippines", "COUNTRY" },
                    { 196, "PG", "Papua New Guinea", "COUNTRY" },
                    { 195, "PF", "French Polynesia", "COUNTRY" },
                    { 194, "PE", "Peru", "COUNTRY" },
                    { 202, "PR", "Puerto Rico", "COUNTRY" },
                    { 193, "PA", "Panama", "COUNTRY" },
                    { 361, "PD", "padure", "USECAT" },
                    { 190, "NU", "Niue", "COUNTRY" },
                    { 189, "NR", "Nauru", "COUNTRY" },
                    { 188, "NP", "Nepal", "COUNTRY" },
                    { 187, "NO", "Norway", "COUNTRY" },
                    { 186, "NL", "Netherlands", "COUNTRY" },
                    { 185, "NI", "Nicaragua", "COUNTRY" },
                    { 184, "NG", "Nigeria", "COUNTRY" },
                    { 192, "OM", "Oman", "COUNTRY" },
                    { 203, "PS", "Palestinian Territory, Occupied", "COUNTRY" },
                    { 204, "PT", "Portugal", "COUNTRY" },
                    { 205, "PW", "Palau", "COUNTRY" },
                    { 224, "SN", "Senegal", "COUNTRY" },
                    { 223, "SM", "San Marino", "COUNTRY" },
                    { 222, "SL", "Sierra Leone", "COUNTRY" },
                    { 221, "SK", "Slovakia", "COUNTRY" },
                    { 220, "SJ", "Svalbard And Jan Mayen", "COUNTRY" },
                    { 219, "SI", "Slovenia", "COUNTRY" },
                    { 218, "SH", "Saint Helena ", "COUNTRY" },
                    { 217, "SG", "Singapore", "COUNTRY" },
                    { 216, "SE", "Sweden", "COUNTRY" },
                    { 215, "SD", "Sudan", "COUNTRY" },
                    { 214, "SC", "Seychelles", "COUNTRY" },
                    { 213, "SB", "Solomon Islands", "COUNTRY" },
                    { 212, "SA", "Saudi Arabia", "COUNTRY" },
                    { 211, "RW", "Rwanda", "COUNTRY" },
                    { 210, "RU", "Russian Federation", "COUNTRY" },
                    { 209, "RO", "Romania", "COUNTRY" },
                    { 208, "RE", "Réunion", "COUNTRY" },
                    { 207, "QA", "Qatar", "COUNTRY" },
                    { 206, "PY", "Paraguay", "COUNTRY" },
                    { 226, "SR", "Suriname", "COUNTRY" },
                    { 271, "DKK", "DKK", "CURRENCY" },
                    { 272, "EIR", "EIR", "CURRENCY" },
                    { 273, "EUR", "EUR", "CURRENCY" },
                    { 337, "ACCESIUNE", "Accesiune", "TITLETYPE" },
                    { 336, "ZONA", "Zona", "ST" },
                    { 335, "STRD", "Stradela", "ST" },
                    { 334, "STRADELA", "Stradela", "ST" },
                    { 333, "STR", "Strada", "ST" },
                    { 332, "SPLAIUL", "Splaiul", "ST" },
                    { 331, "SOS", "Soseaua", "ST" },
                    { 330, "PIATA", "Piata", "ST" },
                    { 338, "CONSTITUIRE", "Constituire", "TITLETYPE" },
                    { 329, "PSJ", "Pasajul", "ST" },
                    { 327, "INTR", "Intrarea", "ST" },
                    { 326, "FNDC", "Fundacul", "ST" },
                    { 325, "FND", "Fundatura", "ST" },
                    { 324, "DRUMUL", "Drumul", "ST" },
                    { 323, "CAREU", "Careul", "ST" },
                    { 322, "CALEA", "Calea", "ST" },
                    { 321, "BDUL", "Bulevardul", "ST" },
                    { 320, "AL", "Aleea", "ST" },
                    { 328, "PREL", "Prelungirea", "ST" },
                    { 339, "CONSTRUIRE", "Construire", "TITLETYPE" },
                    { 340, "CONVENTIE", "Conventie", "TITLETYPE" },
                    { 341, "EXPROPRIERE", "Expropriere", "TITLETYPE" },
                    { 360, "P", "pasune", "USECAT" },
                    { 359, "NA", "nedeterminata", "USECAT" },
                    { 358, "N", "neproductiv", "USECAT" },
                    { 357, "L", "livada", "USECAT" },
                    { 356, "HR", "ape curgatoare", "USECAT" },
                    { 355, "HB", "ape statatoare", "USECAT" },
                    { 354, "F", "faneata", "USECAT" },
                    { 353, "DR", "drum", "USECAT" },
                    { 352, "CF", "cai ferate", "USECAT" },
                    { 351, "CC", "curti constructii", "USECAT" },
                    { 350, "ALTELE", "altele", "USECAT" },
                    { 349, "A", "arabil", "USECAT" },
                    { 348, "reconstituire", "Reconstituire", "TITLETYPE" },
                    { 347, "adjudecare", "Adjudecare", "TITLETYPE" },
                    { 346, "UZUCAPIUNE", "Uzucapiune", "TITLETYPE" },
                    { 345, "SUCCESIUNE", "Succesiune", "TITLETYPE" },
                    { 344, "LEGE", "Lege", "TITLETYPE" },
                    { 343, "IESIRE_INDIVIZIUNE", "Iesire Din Indiviziune", "TITLETYPE" },
                    { 342, "HOTARARE", "Hotarare Judecatoreasca", "TITLETYPE" },
                    { 319, "PROVISIONALENTRY", "Inscrierea provizorie", "REGISTRATIONTYPE" },
                    { 318, "POSESION_REGISTATION", "Inscrierea posesiei", "REGISTRATIONTYPE" },
                    { 317, "NOTATION", "Notare", "REGISTRATIONTYPE" },
                    { 316, "INTAB", "Intabulare", "REGISTRATIONTYPE" },
                    { 292, "FISA_INTERVIU", "fisa de interviu", "DOCT" },
                    { 291, "CERTIFICAT_GREFA1", "certificat grefa", "DOCT" },
                    { 290, "ADMINISTRATIVE_1", "act administrativ", "DOCT" },
                    { 289, "ACT_NOTARIAL", "act notarial", "DOCT" },
                    { 288, "ACT_NORMATIV", "act normativ", "DOCT" },
                    { 287, "ACTIUNE_INSTANTA", "actiune in instanta", "DOCT" },
                    { 286, "MICRO", "Micro", "DISTRICT" },
                    { 285, "CVART", "Cvartal", "DISTRICT" },
                    { 284, "CART", "Cartier", "DISTRICT" },
                    { 283, "CAR", "Careul", "DISTRICT" },
                    { 282, "USD", "USD", "CURRENCY" },
                    { 281, "TRY", "TRY", "CURRENCY" },
                    { 280, "RON", "RON", "CURRENCY" },
                    { 279, "ROL", "ROL", "CURRENCY" },
                    { 278, "LEI", "LEI", "CURRENCY" },
                    { 277, "JPY", "JPY", "CURRENCY" },
                    { 276, "INT", "INT", "CURRENCY" },
                    { 275, "HUF", "HUF", "CURRENCY" },
                    { 274, "GBP", "GBP", "CURRENCY" },
                    { 293, "HOTARARE_JUDECATOREASCA", "hotarare judecatoreasca", "DOCT" },
                    { 183, "NF", "Norfolk Island", "COUNTRY" },
                    { 294, "INSCRIS_SUB_SEMNATURA_PRIVATA", "inscris sub semnatura privata", "DOCT" },
                    { 296, "SOMATIE", "somatie", "DOCT" },
                    { 315, "PERCENTAGE_QUOTA", "Cota procentuala", "QUOTA_TYPE" },
                    { 314, "FRACTION_QUOTA", "Cota fractionara", "QUOTA_TYPE" },
                    { 313, "ABSOLUTE_QUOTA", "Cota absoluta (supraf.)", "QUOTA_TYPE" },
                    { 312, "UPDATE_DATA_UI", "Actualizare date UI", "OT_CAD" },
                    { 311, "UPDATE_DATA_LAND", "Actualizare date imobil", "OT_CAD" },
                    { 310, "RECTIFY_BOUNDARIES", "Rectificare hotar", "OT_CAD" },
                    { 309, "RECREATE_UI", "Reapartamentare", "OT_CAD" },
                    { 308, "FIRST_REGISTRATION_UI", "Prima inregistrare UI", "OT_CAD" },
                    { 307, "FIRST_REGISTRATION", "Prima inregistrare imobil", "OT_CAD" },
                    { 306, "DISMEMBER_AMALGAMATION_UI", "Alipire/Dezlipire UI", "OT_CAD" },
                    { 305, "DISMEMBER", "Dezmembrare imobil", "OT_CAD" },
                    { 304, "CREATE_UI", "Apartamentare", "OT_CAD" },
                    { 303, "AMALGAMATION", "Alipire imobile", "OT_CAD" },
                    { 302, "LAND", "teren", "ITYPE" },
                    { 301, "BUILDING", "constructie", "ITYPE" },
                    { 300, "APARTMENT", "apartament", "ITYPE" },
                    { 299, "PASS", "Paşaport", "IDCARDTYPE" },
                    { 298, "CI", "Carte de identitate", "IDCARDTYPE" },
                    { 297, "BI", "Buletin de identitate", "IDCARDTYPE" },
                    { 295, "ORDONANTA", "ordonanta", "DOCT" },
                    { 182, "NE", "Niger", "COUNTRY" },
                    { 191, "NZ", "New Zealand", "COUNTRY" },
                    { 180, "NA", "Namibia", "COUNTRY" },
                    { 65, "CD", "Congo, The Democratic Republic Of The", "COUNTRY" },
                    { 64, "CC", "Cocos (Keeling) Islands", "COUNTRY" },
                    { 63, "CA", "Canada", "COUNTRY" },
                    { 62, "BZ", "Belize", "COUNTRY" },
                    { 61, "BY", "Belarus", "COUNTRY" },
                    { 60, "BW", "Botswana", "COUNTRY" },
                    { 59, "BV", "Bouvet Island", "COUNTRY" },
                    { 58, "BT", "Bhutan", "COUNTRY" },
                    { 66, "CF", "Central African Republic", "COUNTRY" },
                    { 57, "BS", "Bahamas", "COUNTRY" },
                    { 55, "BO", "Bolivia", "COUNTRY" },
                    { 54, "BN", "Brunei Darussalam", "COUNTRY" },
                    { 53, "BM", "Bermuda", "COUNTRY" },
                    { 52, "BJ", "Benin", "COUNTRY" },
                    { 51, "BI", "Burundi", "COUNTRY" },
                    { 50, "BH", "Bahrain", "COUNTRY" },
                    { 49, "BG", "Bulgaria", "COUNTRY" },
                    { 48, "BF", "Burkina Faso", "COUNTRY" },
                    { 56, "BR", "Brazil", "COUNTRY" },
                    { 67, "CG", "Congo", "COUNTRY" },
                    { 68, "CH", "Switzerland", "COUNTRY" },
                    { 69, "CI", "Côte D'Ivoire", "COUNTRY" },
                    { 88, "EC", "Ecuador", "COUNTRY" },
                    { 87, "DZ", "Algeria", "COUNTRY" },
                    { 86, "DO", "Dominican Republic", "COUNTRY" },
                    { 85, "DM", "Dominica", "COUNTRY" },
                    { 84, "DK", "Denmark", "COUNTRY" },
                    { 83, "DJ", "Djibouti", "COUNTRY" },
                    { 82, "DE", "Germany", "COUNTRY" },
                    { 181, "NC", "New Caledonia", "COUNTRY" },
                    { 80, "CY", "Cyprus", "COUNTRY" },
                    { 79, "CX", "Christmas Island", "COUNTRY" },
                    { 78, "CV", "Cape Verde", "COUNTRY" },
                    { 77, "CU", "Cuba", "COUNTRY" },
                    { 76, "CS", "Serbia And Montenegro", "COUNTRY" },
                    { 75, "CR", "Costa Rica", "COUNTRY" },
                    { 74, "CO", "Colombia", "COUNTRY" },
                    { 73, "CN", "China", "COUNTRY" },
                    { 72, "CM", "Cameroon", "COUNTRY" },
                    { 71, "CL", "Chile", "COUNTRY" },
                    { 70, "CK", "Cook Islands", "COUNTRY" },
                    { 47, "BE", "Belgium", "COUNTRY" },
                    { 46, "BD", "Bangladesh", "COUNTRY" },
                    { 45, "BB", "Barbados", "COUNTRY" },
                    { 44, "BA", "Bosnia And Herzegovina", "COUNTRY" },
                    { 20, "RAMPA_ACCES", "Rampa de acces", "COMMONPARTS" },
                    { 19, "POD", "Pod", "COMMONPARTS" },
                    { 18, "PIVNITA", "Pivnita", "COMMONPARTS" },
                    { 17, "LOGIE", "Logie", "COMMONPARTS" },
                    { 16, "HOLURI", "Holuri", "COMMONPARTS" },
                    { 15, "GHENA", "Ghena", "COMMONPARTS" },
                    { 14, "DUSURI_COMUNE", "Dusuri comune", "COMMONPARTS" },
                    { 13, "COS_FUM", "Cos de fum", "COMMONPARTS" },
                    { 12, "CENTRALA_TERMICA", "Centrala termica", "COMMONPARTS" },
                    { 11, "CASA_SCARII", "Casa scarii", "COMMONPARTS" },
                    { 10, "CASA_ASCENSORULUI", "Casa ascensorului", "COMMONPARTS" },
                    { 9, "CAMERA_TEHNICA", "Camera tehnica", "COMMONPARTS" },
                    { 8, "BOXA", "Boxa", "COMMONPARTS" },
                    { 7, "BALCON", "Balcon", "COMMONPARTS" },
                    { 6, "ALTE_SPATII", "Alte spatii comune", "COMMONPARTS" },
                    { 5, "ACOPERIS", "Acoperis", "COMMONPARTS" },
                    { 4, "CL", "constructii de locuinte", "BUILDDEST" },
                    { 3, "CIE", "constructii industriale si edilitare", "BUILDDEST" },
                    { 2, "CAS", "constructii administrative si social culturale", "BUILDDEST" },
                    { 21, "SCARA_ACCES", "Scara de acces", "COMMONPARTS" },
                    { 89, "EE", "Estonia", "COUNTRY" },
                    { 22, "SCARI_EXTERIOARE", "Scari exterioare", "COMMONPARTS" },
                    { 24, "SUBSOL", "Subsol", "COMMONPARTS" },
                    { 43, "AZ", "Azerbaijan", "COUNTRY" },
                    { 42, "AX", "Aland Islands", "COUNTRY" },
                    { 41, "AW", "Aruba", "COUNTRY" },
                    { 40, "AU", "Australia", "COUNTRY" },
                    { 39, "AT", "Austria", "COUNTRY" },
                    { 38, "AS", "American Samoa", "COUNTRY" },
                    { 37, "AR", "Argentina", "COUNTRY" },
                    { 36, "AQ", "Antarctica", "COUNTRY" },
                    { 35, "AO", "Angola", "COUNTRY" },
                    { 34, "AN", "Netherlands Antilles", "COUNTRY" },
                    { 33, "AM", "Armenia", "COUNTRY" },
                    { 32, "AL", "Albania", "COUNTRY" },
                    { 31, "AI", "Anguilla", "COUNTRY" },
                    { 30, "AG", "Antigua And Barbuda", "COUNTRY" },
                    { 29, "AF", "Afghanistan ", "COUNTRY" },
                    { 28, "AE", "United Arab Emirates", "COUNTRY" },
                    { 27, "AD", "Andorra", "COUNTRY" },
                    { 26, "USCATORIE", "Uscatorie", "COMMONPARTS" },
                    { 25, "TERASA", "Terasa", "COMMONPARTS" },
                    { 23, "SPALATORIE", "Spalatorie", "COMMONPARTS" },
                    { 90, "EG", "Egypt", "COUNTRY" },
                    { 81, "CZ", "Czech Republic", "COUNTRY" },
                    { 92, "ER", "Eritrea", "COUNTRY" },
                    { 156, "LU", "Luxembourg", "COUNTRY" },
                    { 155, "LT", "Lithuania", "COUNTRY" },
                    { 154, "LS", "Lesotho", "COUNTRY" },
                    { 153, "LR", "Liberia", "COUNTRY" },
                    { 152, "LK", "Sri Lanka", "COUNTRY" },
                    { 151, "LI", "Liechtenstein", "COUNTRY" },
                    { 150, "LC", "Saint Lucia", "COUNTRY" },
                    { 149, "LB", "Lebanon", "COUNTRY" },
                    { 148, "LA", "Lao People'S Democratic Republic ", "COUNTRY" },
                    { 147, "KZ", "Kazakhstan", "COUNTRY" },
                    { 146, "KY", "Cayman Islands", "COUNTRY" },
                    { 145, "KW", "Kuwait", "COUNTRY" },
                    { 91, "EH", "Western Sahara", "COUNTRY" },
                    { 143, "KP", "Korea, Democratic People'S Republic Of", "COUNTRY" },
                    { 142, "KN", "Saint Kitts And Nevis", "COUNTRY" },
                    { 141, "KM", "Comoros", "COUNTRY" },
                    { 140, "KI", "Kiribati", "COUNTRY" },
                    { 139, "KH", "Cambodia", "COUNTRY" },
                    { 138, "KG", "Kyrgyzstan", "COUNTRY" },
                    { 157, "LV", "Latvia", "COUNTRY" },
                    { 137, "KE", "Kenya", "COUNTRY" },
                    { 158, "LY", "Libyan Arab Jamahiriya", "COUNTRY" },
                    { 160, "MC", "Monaco", "COUNTRY" },
                    { 179, "MZ", "Mozambique", "COUNTRY" },
                    { 178, "MY", "Malaysia", "COUNTRY" },
                    { 177, "MX", "Mexico", "COUNTRY" },
                    { 176, "MW", "Malawi", "COUNTRY" },
                    { 175, "MV", "Maldives", "COUNTRY" },
                    { 174, "MU", "Mauritius", "COUNTRY" },
                    { 173, "MT", "Malta", "COUNTRY" },
                    { 172, "MS", "Montserrat", "COUNTRY" },
                    { 171, "MR", "Mauritania", "COUNTRY" },
                    { 170, "MQ", "Martinique", "COUNTRY" },
                    { 169, "MP", "Northern Mariana Islands", "COUNTRY" },
                    { 168, "MO", "Macao", "COUNTRY" },
                    { 167, "MN", "Mongolia", "COUNTRY" },
                    { 166, "MM", "Myanmar", "COUNTRY" },
                    { 165, "ML", "Mali", "COUNTRY" },
                    { 164, "MK", "Macedonia, The Former Yugoslav Republic Of", "COUNTRY" },
                    { 163, "MH", "Marshall Islands", "COUNTRY" },
                    { 162, "MG", "Madagascar", "COUNTRY" },
                    { 161, "MD", "Moldova, Republic Of", "COUNTRY" },
                    { 159, "MA", "Morocco", "COUNTRY" },
                    { 136, "JP", "Japan", "COUNTRY" },
                    { 144, "KR", "Korea, Republic Of", "COUNTRY" },
                    { 134, "JM", "Jamaica", "COUNTRY" },
                    { 115, "GT", "Guatemala", "COUNTRY" },
                    { 114, "GS", "South Georgia And The South Sandwich Islands", "COUNTRY" },
                    { 113, "GR", "Greece", "COUNTRY" },
                    { 112, "GQ", "Equatorial Guinea", "COUNTRY" },
                    { 111, "GP", "Guadeloupe", "COUNTRY" },
                    { 110, "GN", "Guinea", "COUNTRY" },
                    { 109, "GM", "Gambia", "COUNTRY" },
                    { 108, "GL", "Greenland", "COUNTRY" },
                    { 107, "GI", "Gibraltar", "COUNTRY" },
                    { 106, "GH", "Ghana", "COUNTRY" },
                    { 105, "GF", "French Guiana", "COUNTRY" },
                    { 104, "GE", "Georgia", "COUNTRY" },
                    { 103, "GD", "Grenada", "COUNTRY" },
                    { 102, "GB", "United Kingdom", "COUNTRY" },
                    { 101, "GA", "Gabon ", "COUNTRY" },
                    { 100, "FR", "France", "COUNTRY" },
                    { 99, "FO", "Faroe Islands", "COUNTRY" },
                    { 98, "FM", "Micronesia, Federated States Of", "COUNTRY" },
                    { 97, "FK", "Falkland Islands (Malvinas)", "COUNTRY" },
                    { 96, "FJ", "Fiji", "COUNTRY" },
                    { 95, "FI", "Finland", "COUNTRY" },
                    { 94, "ET", "Ethiopia", "COUNTRY" },
                    { 93, "ES", "Spain", "COUNTRY" },
                    { 116, "GU", "Guam", "COUNTRY" },
                    { 135, "JO", "Jordan", "COUNTRY" },
                    { 362, "V", "vie", "USECAT" },
                    { 119, "HK", "Hong Kong", "COUNTRY" },
                    { 118, "GY", "Guyana", "COUNTRY" },
                    { 117, "GW", "Guinea-Bissau", "COUNTRY" },
                    { 122, "HR", "Croatia", "COUNTRY" },
                    { 123, "HT", "Haiti", "COUNTRY" },
                    { 124, "HU", "Hungary", "COUNTRY" },
                    { 121, "HN", "Honduras", "COUNTRY" },
                    { 126, "IE", "Ireland", "COUNTRY" },
                    { 125, "ID", "Indonesia", "COUNTRY" },
                    { 128, "IN", "India", "COUNTRY" },
                    { 129, "IO", "British Indian Ocean Territory", "COUNTRY" },
                    { 130, "IQ", "Iraq", "COUNTRY" },
                    { 131, "IR", "Iran, Islamic Republic Of", "COUNTRY" },
                    { 132, "IS", "Iceland", "COUNTRY" },
                    { 133, "IT", "Italy", "COUNTRY" },
                    { 127, "IL", "Israel", "COUNTRY" },
                    { 120, "HM", "Heard Island And Mcdonald Islands", "COUNTRY" }
                });

            migrationBuilder.InsertData(
                table: "Judet",
                columns: new[] { "Id", "Denumire", "Descriere" },
                values: new object[] { 1, "Ialomita", null });

            migrationBuilder.InsertData(
                table: "Localitate",
                columns: new[] { "Id", "Denumire", "Descriere" },
                values: new object[] { 1, "Sarateni", null });

            migrationBuilder.InsertData(
                table: "TipuriActProprietate",
                columns: new[] { "Id", "Denumire", "Descriere" },
                values: new object[,]
                {
                    { 8, "Act de Donatie", null },
                    { 1, "Titlu Proprietate", null },
                    { 2, "Contract de Vanzare Cumparare", null },
                    { 3, "Contract de Vanzare Cumparare cu Clauza de Intretinere", null },
                    { 4, "Sentinta Civila", null },
                    { 5, "Certificat de Mostenitor", null },
                    { 17, "Certificat de Legatar Suplimentar", null },
                    { 16, "Declaratie Notariala", null },
                    { 15, "Testament", null },
                    { 14, "Contract de Partaj Imobiliar", null },
                    { 13, "Act de Partaj Voluntar", null },
                    { 6, "Certificat de Mostenitor Succesiv", null },
                    { 11, "Contract de Partaj", null },
                    { 10, "Contract de Donatie Imobiliara", null },
                    { 9, "Contract de Donatie", null },
                    { 7, "Certificat de Mostenitor Suplimentar", null },
                    { 12, "Contract de Partaj Voluntar", null }
                });

            migrationBuilder.InsertData(
                table: "UAT",
                columns: new[] { "Id", "Denumire", "Descriere", "JudetId" },
                values: new object[] { 1, "Sarateni", null, 1 });

            migrationBuilder.InsertData(
                table: "Tarlale",
                columns: new[] { "Id", "Denumire", "Descriere", "UATId" },
                values: new object[,]
                {
                    { 1, "119,3", null, 1 },
                    { 81, "134,3", null, 1 },
                    { 80, "119,4", null, 1 },
                    { 79, "119,1", null, 1 },
                    { 78, "520,1", null, 1 },
                    { 77, "136,2", null, 1 },
                    { 76, "367,6", null, 1 },
                    { 75, "422,4", null, 1 },
                    { 74, "267.1.6", null, 1 },
                    { 73, "140,4", null, 1 },
                    { 72, "179,2", null, 1 },
                    { 71, "393,2", null, 1 },
                    { 82, "297,1,2", null, 1 },
                    { 70, "102,2", null, 1 },
                    { 68, "129", null, 1 },
                    { 67, "112,1", null, 1 },
                    { 66, "104,1", null, 1 },
                    { 65, "2420", null, 1 },
                    { 64, "119,5", null, 1 },
                    { 63, "51,7", null, 1 },
                    { 62, "114,2", null, 1 },
                    { 61, "102,1", null, 1 },
                    { 60, "303,2", null, 1 },
                    { 59, "72,4", null, 1 },
                    { 58, "133,2", null, 1 },
                    { 69, "161,6", null, 1 },
                    { 57, "479,5", null, 1 },
                    { 83, "329", null, 1 },
                    { 85, "51,6", null, 1 },
                    { 109, "76,2", null, 1 },
                    { 108, "341,2", null, 1 },
                    { 107, "267.3.4", null, 1 },
                    { 106, "56,7", null, 1 },
                    { 105, "161,5", null, 1 },
                    { 104, "471,2", null, 1 },
                    { 103, "179,1", null, 1 },
                    { 102, "161,4", null, 1 },
                    { 101, "383,5", null, 1 },
                    { 100, "297,1,6", null, 1 },
                    { 99, "383,3", null, 1 },
                    { 84, "303,6", null, 1 },
                    { 98, "104", null, 1 },
                    { 96, "276,6", null, 1 },
                    { 95, "105,2", null, 1 },
                    { 94, "72,2", null, 1 },
                    { 93, "119,2", null, 1 },
                    { 92, "420,4", null, 1 },
                    { 91, "483,8", null, 1 },
                    { 90, "263,2", null, 1 },
                    { 89, "520,4", null, 1 },
                    { 88, "420,2", null, 1 },
                    { 87, "422,1", null, 1 },
                    { 86, "291.1.2", null, 1 },
                    { 97, "445", null, 1 },
                    { 110, "393,1", null, 1 },
                    { 56, "304,2", null, 1 },
                    { 54, "483,5", null, 1 },
                    { 25, "162,4", null, 1 },
                    { 24, "162,2", null, 1 },
                    { 23, "474,2", null, 1 },
                    { 22, "341", null, 1 },
                    { 21, "104,2", null, 1 },
                    { 20, "75,1", null, 1 },
                    { 19, "499,1", null, 1 },
                    { 18, "161,3", null, 1 },
                    { 17, "480,4", null, 1 },
                    { 16, "267,4", null, 1 },
                    { 15, "133,1", null, 1 },
                    { 26, "169,1", null, 1 },
                    { 14, "72,1", null, 1 },
                    { 12, "161,1", null, 1 },
                    { 11, "110", null, 1 },
                    { 10, "480,1", null, 1 },
                    { 9, "117", null, 1 },
                    { 8, "160,1", null, 1 },
                    { 7, "420,1", null, 1 },
                    { 6, "393,3", null, 1 },
                    { 5, "105,6", null, 1 },
                    { 4, "79", null, 1 },
                    { 3, "94,1", null, 1 },
                    { 2, "339,1", null, 1 },
                    { 13, "267,6", null, 1 },
                    { 55, "72,3", null, 1 },
                    { 27, "297.1.5", null, 1 },
                    { 29, "177,1", null, 1 },
                    { 53, "445,2", null, 1 },
                    { 52, "134,2", null, 1 },
                    { 51, "422,6", null, 1 },
                    { 50, "297.1.6", null, 1 },
                    { 49, "383,2", null, 1 },
                    { 48, "51,5", null, 1 },
                    { 47, "483,6", null, 1 },
                    { 46, "76,1", null, 1 },
                    { 45, "100", null, 1 },
                    { 44, "479,2", null, 1 },
                    { 43, "82,2", null, 1 },
                    { 28, "479,1", null, 1 },
                    { 42, "105,3", null, 1 },
                    { 40, "474,1", null, 1 },
                    { 39, "297.1.2", null, 1 },
                    { 38, "114,1", null, 1 },
                    { 37, "112,2", null, 1 },
                    { 36, "445,1", null, 1 },
                    { 35, "345,1", null, 1 },
                    { 34, "140", null, 1 },
                    { 33, "64", null, 1 },
                    { 32, "383,1", null, 1 },
                    { 31, "483,4", null, 1 },
                    { 30, "134,1", null, 1 },
                    { 41, "112,3", null, 1 },
                    { 111, "479,3", null, 1 }
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
                name: "Dictionar");

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
