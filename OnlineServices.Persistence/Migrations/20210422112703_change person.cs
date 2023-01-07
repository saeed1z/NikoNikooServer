using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineServices.Persistence.Migrations
{
    public partial class changeperson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PersonCarId",
                table: "PersonPackage",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ActivityAddress",
                table: "Person",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Person",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EconomicCode",
                table: "Person",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Person",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InterfaceFamily",
                table: "Person",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InterfaceName",
                table: "Person",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Post",
                table: "Person",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegistrationDate",
                table: "Person",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegistrationNumber",
                table: "Person",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegistrationPlace",
                table: "Person",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebsiteUrl",
                table: "Person",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CommercialUserRequest",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<Guid>(nullable: false),
                    CompanyName = table.Column<string>(nullable: true),
                    RegistrationNumber = table.Column<string>(nullable: true),
                    RegistrationDate = table.Column<string>(nullable: true),
                    RegistrationPlace = table.Column<string>(nullable: true),
                    EconomicCode = table.Column<string>(nullable: true),
                    WebsiteUrl = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    ActivityAddress = table.Column<string>(nullable: true),
                    InterfaceName = table.Column<string>(nullable: true),
                    InterfaceFamily = table.Column<string>(nullable: true),
                    Post = table.Column<string>(nullable: true),
                    IsRejected = table.Column<bool>(nullable: false),
                    RejectedReason = table.Column<string>(nullable: true),
                    IsAccepted = table.Column<bool>(nullable: false),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedUserId = table.Column<Guid>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedUserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommercialUserRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommercialUserRequest_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommercialUserRequest_PersonId",
                table: "CommercialUserRequest",
                column: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommercialUserRequest");

            migrationBuilder.DropColumn(
                name: "ActivityAddress",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "EconomicCode",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "InterfaceFamily",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "InterfaceName",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "Post",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "RegistrationDate",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "RegistrationNumber",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "RegistrationPlace",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "WebsiteUrl",
                table: "Person");

            migrationBuilder.AlterColumn<int>(
                name: "PersonCarId",
                table: "PersonPackage",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
