using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineServices.Persistence.Migrations
{
    public partial class addbaseandmodeltechnical : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Base",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BaseKindId = table.Column<short>(nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    OrderNo = table.Column<short>(nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", nullable: true),
                    IsReserved = table.Column<bool>(nullable: false),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedUserId = table.Column<Guid>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Base", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Base_BaseKind_BaseKindId",
                        column: x => x.BaseKindId,
                        principalTable: "BaseKind",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModelTechnicalInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ModelId = table.Column<int>(nullable: false),
                    BaseId = table.Column<int>(nullable: false),
                    Value = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedUserId = table.Column<Guid>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelTechnicalInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModelTechnicalInfo_Base_BaseId",
                        column: x => x.BaseId,
                        principalTable: "Base",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModelTechnicalInfo_Model_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Model",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Base_BaseKindId",
                table: "Base",
                column: "BaseKindId");

            migrationBuilder.CreateIndex(
                name: "IX_ModelTechnicalInfo_BaseId",
                table: "ModelTechnicalInfo",
                column: "BaseId");

            migrationBuilder.CreateIndex(
                name: "IX_ModelTechnicalInfo_ModelId",
                table: "ModelTechnicalInfo",
                column: "ModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModelTechnicalInfo");

            migrationBuilder.DropTable(
                name: "Base");
        }
    }
}
