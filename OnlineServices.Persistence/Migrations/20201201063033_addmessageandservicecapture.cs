using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineServices.Persistence.Migrations
{
    public partial class addmessageandservicecapture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServiceCapture",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ServiceRequestId = table.Column<Guid>(nullable: false),
                    FileTypeBaseId = table.Column<int>(nullable: false),
                    Extension = table.Column<string>(type: "varchar(5)", nullable: false),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    IsDeletedFromServer = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCapture", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceCapture_Base_FileTypeBaseId",
                        column: x => x.FileTypeBaseId,
                        principalTable: "Base",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceCapture_ServiceRequest_ServiceRequestId",
                        column: x => x.ServiceRequestId,
                        principalTable: "ServiceRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceRequestId = table.Column<Guid>(nullable: true),
                    FromPersonId = table.Column<Guid>(nullable: false),
                    ToPersonId = table.Column<Guid>(nullable: true),
                    Body = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    ServiceCaptureId = table.Column<Guid>(nullable: true),
                    AllowResponse = table.Column<bool>(nullable: false),
                    IsRead = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Message_Person_FromPersonId",
                        column: x => x.FromPersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Message_ServiceCapture_ServiceCaptureId",
                        column: x => x.ServiceCaptureId,
                        principalTable: "ServiceCapture",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Message_ServiceRequest_ServiceRequestId",
                        column: x => x.ServiceRequestId,
                        principalTable: "ServiceRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Message_Person_ToPersonId",
                        column: x => x.ToPersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Message_FromPersonId",
                table: "Message",
                column: "FromPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_ServiceCaptureId",
                table: "Message",
                column: "ServiceCaptureId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_ServiceRequestId",
                table: "Message",
                column: "ServiceRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_ToPersonId",
                table: "Message",
                column: "ToPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCapture_FileTypeBaseId",
                table: "ServiceCapture",
                column: "FileTypeBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCapture_ServiceRequestId",
                table: "ServiceCapture",
                column: "ServiceRequestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "ServiceCapture");
        }
    }
}
