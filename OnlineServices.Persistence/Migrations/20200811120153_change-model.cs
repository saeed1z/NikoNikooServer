using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineServices.Persistence.Migrations
{
    public partial class changemodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Brand");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Model",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUserId",
                table: "Model",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Model",
                type: "decimal(18, 0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Model",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedUserId",
                table: "Model",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "Model",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Brand",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUserId",
                table: "Brand",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Brand",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedUserId",
                table: "Brand",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "Brand",
                rowVersion: true,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Model");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "Model");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Model");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Model");

            migrationBuilder.DropColumn(
                name: "UpdatedUserId",
                table: "Model");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Model");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Brand");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "Brand");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Brand");

            migrationBuilder.DropColumn(
                name: "UpdatedUserId",
                table: "Brand");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Brand");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Brand",
                type: "decimal(18, 0)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
