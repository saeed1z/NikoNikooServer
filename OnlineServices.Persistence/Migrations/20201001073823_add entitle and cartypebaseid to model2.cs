using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineServices.Persistence.Migrations
{
    public partial class addentitleandcartypebaseidtomodel2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Model_CarTypeBaseId",
                table: "Model",
                column: "CarTypeBaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Model_Base_CarTypeBaseId",
                table: "Model",
                column: "CarTypeBaseId",
                principalTable: "Base",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Model_Base_CarTypeBaseId",
                table: "Model");

            migrationBuilder.DropIndex(
                name: "IX_Model_CarTypeBaseId",
                table: "Model");
        }
    }
}
