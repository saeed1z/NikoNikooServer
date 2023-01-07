using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineServices.Persistence.Migrations
{
    public partial class upperson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Person",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "BuildingFloor",
                table: "Person",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BuildingPlate",
                table: "Person",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BuildingUnit",
                table: "Person",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CityArea",
                table: "Person",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EducationLevel",
                table: "Person",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FatherName",
                table: "Person",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Person",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "BuildingFloor",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "BuildingPlate",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "BuildingUnit",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "CityArea",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "EducationLevel",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "FatherName",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Person");
        }
    }
}
