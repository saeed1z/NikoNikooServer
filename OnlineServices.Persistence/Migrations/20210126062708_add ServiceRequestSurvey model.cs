using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineServices.Persistence.Migrations
{
    public partial class addServiceRequestSurveymodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServiceRequestSurvey",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ServiceRequestId = table.Column<Guid>(nullable: false),
                    ServiceTypeQuestionId = table.Column<int>(nullable: false),
                    Score = table.Column<byte>(nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedUserId = table.Column<Guid>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRequestSurvey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceRequestSurvey_ServiceRequest_ServiceRequestId",
                        column: x => x.ServiceRequestId,
                        principalTable: "ServiceRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceRequestSurvey_ServiceTypeQuestion_ServiceTypeQuestionId",
                        column: x => x.ServiceTypeQuestionId,
                        principalTable: "ServiceTypeQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequestSurvey_ServiceRequestId",
                table: "ServiceRequestSurvey",
                column: "ServiceRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequestSurvey_ServiceTypeQuestionId",
                table: "ServiceRequestSurvey",
                column: "ServiceTypeQuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceRequestSurvey");
        }
    }
}
