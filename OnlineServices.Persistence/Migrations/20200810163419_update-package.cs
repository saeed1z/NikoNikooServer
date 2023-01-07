using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineServices.Persistence.Migrations
{
    public partial class updatepackage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "ExpiredDuration",
                table: "PackageTemplate",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "PackageTemplate",
                type: "decimal(12, 0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RealPrice",
                table: "PackageTemplate",
                type: "decimal(12, 0)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpiredDuration",
                table: "PackageTemplate");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "PackageTemplate");

            migrationBuilder.DropColumn(
                name: "RealPrice",
                table: "PackageTemplate");
        }
    }
}
