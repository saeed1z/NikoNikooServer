using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineServices.Persistence.Migrations
{
    public partial class readdpersonpackagepersoncar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PersonCar",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<Guid>(nullable: false),
                    ModelId = table.Column<int>(nullable: false),
                    PlaqueNo = table.Column<string>(nullable: false),
                    ChassisNo = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedUserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonCar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonCar_Model_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Model",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonCar_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonPackage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<Guid>(nullable: false),
                    PackageTemplateId = table.Column<Guid>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(12, 0)", nullable: false),
                    FactorDate = table.Column<DateTime>(nullable: false),
                    ExpiredDate = table.Column<DateTime>(nullable: true),
                    FactorNumber = table.Column<int>(nullable: true),
                    BankDocument = table.Column<string>(nullable: true),
                    BankId = table.Column<string>(nullable: true),
                    BankDocumentDate = table.Column<DateTime>(nullable: true),
                    CanceledDate = table.Column<DateTime>(nullable: true),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedUserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonPackage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonPackage_PackageTemplate_PackageTemplateId",
                        column: x => x.PackageTemplateId,
                        principalTable: "PackageTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonPackage_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonPackageDetail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonPackageId = table.Column<int>(nullable: false),
                    ServiceTypeId = table.Column<int>(nullable: false),
                    Quantity = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonPackageDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonPackageDetail_PersonPackage_PersonPackageId",
                        column: x => x.PersonPackageId,
                        principalTable: "PersonPackage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonPackageDetail_ServiceType_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalTable: "ServiceType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonCar_ModelId",
                table: "PersonCar",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonCar_PersonId",
                table: "PersonCar",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonPackage_PackageTemplateId",
                table: "PersonPackage",
                column: "PackageTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonPackage_PersonId",
                table: "PersonPackage",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonPackageDetail_PersonPackageId",
                table: "PersonPackageDetail",
                column: "PersonPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonPackageDetail_ServiceTypeId",
                table: "PersonPackageDetail",
                column: "ServiceTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonCar");

            migrationBuilder.DropTable(
                name: "PersonPackageDetail");

            migrationBuilder.DropTable(
                name: "PersonPackage");
        }
    }
}
