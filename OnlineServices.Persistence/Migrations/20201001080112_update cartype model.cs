using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineServices.Persistence.Migrations
{
    public partial class updatecartypemodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Model_Base_CarTypeBaseId",
                table: "Model");

            migrationBuilder.AlterColumn<int>(
                name: "CarTypeBaseId",
                table: "Model",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Model_Base_CarTypeBaseId",
                table: "Model",
                column: "CarTypeBaseId",
                principalTable: "Base",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Model_Base_CarTypeBaseId",
                table: "Model");

            migrationBuilder.AlterColumn<int>(
                name: "CarTypeBaseId",
                table: "Model",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Model_Base_CarTypeBaseId",
                table: "Model",
                column: "CarTypeBaseId",
                principalTable: "Base",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
