using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineServices.Persistence.Migrations
{
    public partial class addservicerequestaccept : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServiceRequestAccept",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ServiceRequestId = table.Column<Guid>(nullable: false),
                    AcceptDateTime = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    PersonId = table.Column<Guid>(nullable: true),
                    ExpertPersonId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRequestAccept", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceRequestAccept_ServiceRequest_ServiceRequestId",
                        column: x => x.ServiceRequestId,
                        principalTable: "ServiceRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequestAccept_ServiceRequestId",
                table: "ServiceRequestAccept",
                column: "ServiceRequestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceRequestAccept");
        }
    }
}
