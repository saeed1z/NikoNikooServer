using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineServices.Persistence.Migrations
{
    public partial class addservicecenteranddetailmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServiceCenter",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    NationalCode = table.Column<string>(type: "varchar(10)", nullable: true),
                    StateId = table.Column<byte>(nullable: true),
                    CityId = table.Column<int>(nullable: true),
                    MobileNo = table.Column<string>(type: "varchar(12)", nullable: true),
                    PhoneNo = table.Column<string>(type: "varchar(12)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    PostCode = table.Column<decimal>(type: "decimal(10, 0)", nullable: true),
                    Latitude = table.Column<double>(nullable: true),
                    Longitude = table.Column<double>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Avatar = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", nullable: true),
                    IsCarwash = table.Column<bool>(nullable: false),
                    IsMechanic = table.Column<bool>(nullable: false),
                    IsService = table.Column<bool>(nullable: false),
                    IsAccessory = table.Column<bool>(nullable: false),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCenter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceCenter_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceCenter_State_StateId",
                        column: x => x.StateId,
                        principalTable: "State",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiceCenterDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ServiceCenterId = table.Column<Guid>(nullable: false),
                    ServiceDetailBaseId = table.Column<int>(nullable: false),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCenterDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceCenterDetail_ServiceCenter_ServiceCenterId",
                        column: x => x.ServiceCenterId,
                        principalTable: "ServiceCenter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceCenterDetail_Base_ServiceDetailBaseId",
                        column: x => x.ServiceDetailBaseId,
                        principalTable: "Base",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCenter_CityId",
                table: "ServiceCenter",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCenter_StateId",
                table: "ServiceCenter",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCenterDetail_ServiceCenterId",
                table: "ServiceCenterDetail",
                column: "ServiceCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCenterDetail_ServiceDetailBaseId",
                table: "ServiceCenterDetail",
                column: "ServiceDetailBaseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceCenterDetail");

            migrationBuilder.DropTable(
                name: "ServiceCenter");
        }
    }
}
