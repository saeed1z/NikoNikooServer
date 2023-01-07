using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineServices.Persistence.Migrations
{
    public partial class addispaidtofactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceFactor_ServiceRequest_ServiceRequestId",
                table: "ServiceFactor");

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitFee",
                table: "ServiceFactorDetail",
                type: "decimal(18, 0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,0)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalCost",
                table: "ServiceFactor",
                type: "decimal(18, 0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,0)");

            migrationBuilder.AlterColumn<Guid>(
                name: "ServiceRequestId",
                table: "ServiceFactor",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<DateTime>(
                name: "IssueDate",
                table: "ServiceFactor",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<decimal>(
                name: "FinalFee",
                table: "ServiceFactor",
                type: "decimal(18, 0)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,0)");

            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountFee",
                table: "ServiceFactor",
                type: "decimal(18, 0)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,0)");

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "ServiceFactor",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceFactor_ServiceRequest_ServiceRequestId",
                table: "ServiceFactor",
                column: "ServiceRequestId",
                principalTable: "ServiceRequest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceFactor_ServiceRequest_ServiceRequestId",
                table: "ServiceFactor");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "ServiceFactor");

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitFee",
                table: "ServiceFactorDetail",
                type: "decimal(9,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 0)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalCost",
                table: "ServiceFactor",
                type: "decimal(9,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 0)");

            migrationBuilder.AlterColumn<Guid>(
                name: "ServiceRequestId",
                table: "ServiceFactor",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "IssueDate",
                table: "ServiceFactor",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<decimal>(
                name: "FinalFee",
                table: "ServiceFactor",
                type: "decimal(9,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 0)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountFee",
                table: "ServiceFactor",
                type: "decimal(9,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 0)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceFactor_ServiceRequest_ServiceRequestId",
                table: "ServiceFactor",
                column: "ServiceRequestId",
                principalTable: "ServiceRequest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
