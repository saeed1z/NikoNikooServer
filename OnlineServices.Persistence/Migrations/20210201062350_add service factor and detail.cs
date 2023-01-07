using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineServices.Persistence.Migrations
{
    public partial class addservicefactoranddetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ServiceFactorId",
                table: "Message",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ServiceFactor",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ServiceRequestId = table.Column<Guid>(nullable: false),
                    IssueDate = table.Column<DateTime>(nullable: false),
                    SalesShop = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    SalesShopAddress = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    SalesShopPhone = table.Column<string>(type: "varchar(15)", nullable: false),
                    TotalCost = table.Column<decimal>(type: "decimal(9,0)", nullable: false),
                    DiscountFee = table.Column<decimal>(type: "decimal(9,0)", nullable: false),
                    FinalFee = table.Column<decimal>(type: "decimal(9,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceFactor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceFactor_ServiceRequest_ServiceRequestId",
                        column: x => x.ServiceRequestId,
                        principalTable: "ServiceRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceFactorDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ServiceFactorId = table.Column<Guid>(nullable: false),
                    Row = table.Column<byte>(nullable: false),
                    ItemTitle = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Quantity = table.Column<byte>(nullable: false),
                    UnitFee = table.Column<decimal>(type: "decimal(9,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceFactorDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceFactorDetail_ServiceFactor_ServiceFactorId",
                        column: x => x.ServiceFactorId,
                        principalTable: "ServiceFactor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Message_ServiceFactorId",
                table: "Message",
                column: "ServiceFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceFactor_ServiceRequestId",
                table: "ServiceFactor",
                column: "ServiceRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceFactorDetail_ServiceFactorId",
                table: "ServiceFactorDetail",
                column: "ServiceFactorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_ServiceFactor_ServiceFactorId",
                table: "Message",
                column: "ServiceFactorId",
                principalTable: "ServiceFactor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_ServiceFactor_ServiceFactorId",
                table: "Message");

            migrationBuilder.DropTable(
                name: "ServiceFactorDetail");

            migrationBuilder.DropTable(
                name: "ServiceFactor");

            migrationBuilder.DropIndex(
                name: "IX_Message_ServiceFactorId",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "ServiceFactorId",
                table: "Message");
        }
    }
}
