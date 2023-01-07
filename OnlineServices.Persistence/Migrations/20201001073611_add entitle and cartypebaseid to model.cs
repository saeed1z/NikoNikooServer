using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineServices.Persistence.Migrations
{
    public partial class addentitleandcartypebaseidtomodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarTypeBaseId",
                table: "Model",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnTitle",
                table: "Model",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarTypeBaseId",
                table: "Model");

            migrationBuilder.DropColumn(
                name: "EnTitle",
                table: "Model");
        }
    }
}
