using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineServices.Persistence.Migrations
{
    public partial class addservicerequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServiceRequest",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PersonId = table.Column<Guid>(nullable: false),
                    RequestDateTime = table.Column<DateTime>(nullable: false),
                    ServiceTypeId = table.Column<int>(nullable: false),
                    PersonCarId = table.Column<int>(nullable: true),
                    SourceStateId = table.Column<byte>(nullable: true),
                    SourceCityId = table.Column<int>(nullable: true),
                    SourceAddress = table.Column<string>(nullable: true),
                    SourceLocation = table.Column<string>(nullable: true),
                    DestinationStateId = table.Column<byte>(nullable: true),
                    DestinationCityId = table.Column<int>(nullable: true),
                    DestinationAddress = table.Column<string>(nullable: true),
                    DestinationLocation = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedUserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceRequest_City_DestinationCityId",
                        column: x => x.DestinationCityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceRequest_State_DestinationStateId",
                        column: x => x.DestinationStateId,
                        principalTable: "State",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceRequest_PersonCar_PersonCarId",
                        column: x => x.PersonCarId,
                        principalTable: "PersonCar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceRequest_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceRequest_ServiceType_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalTable: "ServiceType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceRequest_City_SourceCityId",
                        column: x => x.SourceCityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceRequest_State_SourceStateId",
                        column: x => x.SourceStateId,
                        principalTable: "State",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequest_DestinationCityId",
                table: "ServiceRequest",
                column: "DestinationCityId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequest_DestinationStateId",
                table: "ServiceRequest",
                column: "DestinationStateId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequest_PersonCarId",
                table: "ServiceRequest",
                column: "PersonCarId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequest_PersonId",
                table: "ServiceRequest",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequest_ServiceTypeId",
                table: "ServiceRequest",
                column: "ServiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequest_SourceCityId",
                table: "ServiceRequest",
                column: "SourceCityId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequest_SourceStateId",
                table: "ServiceRequest",
                column: "SourceStateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceRequest");
        }
    }
}
