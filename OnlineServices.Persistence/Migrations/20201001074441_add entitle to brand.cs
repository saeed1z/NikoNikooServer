using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineServices.Persistence.Migrations
{
    public partial class addentitletobrand : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EnTitle",
                table: "Brand",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnTitle",
                table: "Brand");
        }
    }
}
