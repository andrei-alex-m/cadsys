using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CS.EF.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TipPersoana",
                table: "Proprietari",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Numar",
                table: "Proprietari",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataEmiterii",
                table: "Proprietari",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataEmiterii",
                table: "Proprietari");

            migrationBuilder.AlterColumn<int>(
                name: "TipPersoana",
                table: "Proprietari",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "Numar",
                table: "Proprietari",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
