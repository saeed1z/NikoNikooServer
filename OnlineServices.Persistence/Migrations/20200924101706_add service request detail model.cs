using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineServices.Persistence.Migrations
{
    public partial class addservicerequestdetailmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServiceRequestDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ServiceRequestId = table.Column<Guid>(nullable: false),
                    ServiceDetailBaseId = table.Column<int>(nullable: false),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRequestDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceRequestDetail_Base_ServiceDetailBaseId",
                        column: x => x.ServiceDetailBaseId,
                        principalTable: "Base",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceRequestDetail_ServiceRequest_ServiceRequestId",
                        column: x => x.ServiceRequestId,
                        principalTable: "ServiceRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequestDetail_ServiceDetailBaseId",
                table: "ServiceRequestDetail",
                column: "ServiceDetailBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequestDetail_ServiceRequestId",
                table: "ServiceRequestDetail",
                column: "ServiceRequestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceRequestDetail");
        }
    }
}
