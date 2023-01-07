using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineServices.Persistence.Migrations
{
    public partial class reupdateservicerequest2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequest_EmployeeId",
                table: "ServiceRequest",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequest_ExpertId",
                table: "ServiceRequest",
                column: "ExpertId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceRequest_Person_EmployeeId",
                table: "ServiceRequest",
                column: "EmployeeId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceRequest_Person_ExpertId",
                table: "ServiceRequest",
                column: "ExpertId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceRequest_Person_EmployeeId",
                table: "ServiceRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceRequest_Person_ExpertId",
                table: "ServiceRequest");

            migrationBuilder.DropIndex(
                name: "IX_ServiceRequest_EmployeeId",
                table: "ServiceRequest");

            migrationBuilder.DropIndex(
                name: "IX_ServiceRequest_ExpertId",
                table: "ServiceRequest");
        }
    }
}
