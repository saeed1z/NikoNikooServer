using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineServices.Persistence.Migrations
{
    public partial class readdthreetale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BaseKind",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentBaseKindId = table.Column<short>(nullable: true),
                    Title = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    OrderNo = table.Column<short>(nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseKind", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceTypeRelation",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceTypeId = table.Column<int>(nullable: false),
                    RelatedServiceTypeId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    OrderNo = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTypeRelation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceTypeRelation_ServiceType_RelatedServiceTypeId",
                        column: x => x.RelatedServiceTypeId,
                        principalTable: "ServiceType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServiceTypeRelation_ServiceType_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalTable: "ServiceType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ConfirmMap",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SourceBaseKindId = table.Column<short>(nullable: true),
                    BaseKindId = table.Column<short>(nullable: false),
                    FromStatusId = table.Column<byte>(nullable: true),
                    ToStatusId = table.Column<byte>(nullable: false),
                    HasConcurrent = table.Column<bool>(nullable: false),
                    IsFinal = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfirmMap", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfirmMap_BaseKind_BaseKindId",
                        column: x => x.BaseKindId,
                        principalTable: "BaseKind",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConfirmMap_Status_FromStatusId",
                        column: x => x.FromStatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConfirmMap_BaseKind_SourceBaseKindId",
                        column: x => x.SourceBaseKindId,
                        principalTable: "BaseKind",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConfirmMap_Status_ToStatusId",
                        column: x => x.ToStatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConfirmMap_BaseKindId",
                table: "ConfirmMap",
                column: "BaseKindId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfirmMap_FromStatusId",
                table: "ConfirmMap",
                column: "FromStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfirmMap_SourceBaseKindId",
                table: "ConfirmMap",
                column: "SourceBaseKindId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfirmMap_ToStatusId",
                table: "ConfirmMap",
                column: "ToStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTypeRelation_RelatedServiceTypeId",
                table: "ServiceTypeRelation",
                column: "RelatedServiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTypeRelation_ServiceTypeId",
                table: "ServiceTypeRelation",
                column: "ServiceTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfirmMap");

            migrationBuilder.DropTable(
                name: "ServiceTypeRelation");

            migrationBuilder.DropTable(
                name: "BaseKind");
        }
    }
}
