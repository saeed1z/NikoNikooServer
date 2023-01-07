using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineServices.Persistence.Migrations
{
    public partial class addstatusandupdateentities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "ServiceRequest",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "LastStatusId",
                table: "ServiceRequest",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModelId",
                table: "ServiceRequest",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<byte>(nullable: false),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequest_BrandId",
                table: "ServiceRequest",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequest_LastStatusId",
                table: "ServiceRequest",
                column: "LastStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequest_ModelId",
                table: "ServiceRequest",
                column: "ModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceRequest_Brand_BrandId",
                table: "ServiceRequest",
                column: "BrandId",
                principalTable: "Brand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceRequest_Status_LastStatusId",
                table: "ServiceRequest",
                column: "LastStatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceRequest_Model_ModelId",
                table: "ServiceRequest",
                column: "ModelId",
                principalTable: "Model",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceRequest_Brand_BrandId",
                table: "ServiceRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceRequest_Status_LastStatusId",
                table: "ServiceRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceRequest_Model_ModelId",
                table: "ServiceRequest");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropIndex(
                name: "IX_ServiceRequest_BrandId",
                table: "ServiceRequest");

            migrationBuilder.DropIndex(
                name: "IX_ServiceRequest_LastStatusId",
                table: "ServiceRequest");

            migrationBuilder.DropIndex(
                name: "IX_ServiceRequest_ModelId",
                table: "ServiceRequest");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "ServiceRequest");

            migrationBuilder.DropColumn(
                name: "LastStatusId",
                table: "ServiceRequest");

            migrationBuilder.DropColumn(
                name: "ModelId",
                table: "ServiceRequest");
        }
    }
}
