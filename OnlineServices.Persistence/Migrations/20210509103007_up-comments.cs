using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineServices.Persistence.Migrations
{
    public partial class upcomments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IamgeName",
                table: "TicketComments");

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "TicketComments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "TicketComments");

            migrationBuilder.AddColumn<string>(
                name: "IamgeName",
                table: "TicketComments",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
