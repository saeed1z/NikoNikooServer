using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineServices.Persistence.Migrations
{
    public partial class servicerequestfiletableadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServiceRequestFile",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RequestFileName = table.Column<string>(nullable: false),
                    RequestFileExtension = table.Column<string>(nullable: false),
                    ServiceRequestId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRequestFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceRequestFile_ServiceRequest_ServiceRequestId",
                        column: x => x.ServiceRequestId,
                        principalTable: "ServiceRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequestFile_ServiceRequestId",
                table: "ServiceRequestFile",
                column: "ServiceRequestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceRequestFile");
        }
    }
}
