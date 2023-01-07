using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineServices.Persistence.Migrations
{
    public partial class addparenttoservicerequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "NeedToCommute",
                table: "ServiceType",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<Guid>(
                name: "ParentServiceRequestId",
                table: "ServiceRequest",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequest_ParentServiceRequestId",
                table: "ServiceRequest",
                column: "ParentServiceRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceRequest_ServiceRequest_ParentServiceRequestId",
                table: "ServiceRequest",
                column: "ParentServiceRequestId",
                principalTable: "ServiceRequest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceRequest_ServiceRequest_ParentServiceRequestId",
                table: "ServiceRequest");

            migrationBuilder.DropIndex(
                name: "IX_ServiceRequest_ParentServiceRequestId",
                table: "ServiceRequest");

            migrationBuilder.DropColumn(
                name: "NeedToCommute",
                table: "ServiceType");

            migrationBuilder.DropColumn(
                name: "ParentServiceRequestId",
                table: "ServiceRequest");
        }
    }
}
