using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CotecnaB.Persistance.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BaseEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<DateTime>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Customer = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Observations = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InspectionInspector",
                columns: table => new
                {
                    InspectionId = table.Column<Guid>(nullable: false),
                    InspectorId = table.Column<Guid>(nullable: false),
                    InspectionDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InspectionInspector", x => new { x.InspectionId, x.InspectorId, x.InspectionDate });
                    table.ForeignKey(
                        name: "FK_InspectionInspector_BaseEntity_InspectionId",
                        column: x => x.InspectionId,
                        principalTable: "BaseEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InspectionInspector_BaseEntity_InspectorId",
                        column: x => x.InspectorId,
                        principalTable: "BaseEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InspectionInspector_InspectorId",
                table: "InspectionInspector",
                column: "InspectorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InspectionInspector");

            migrationBuilder.DropTable(
                name: "BaseEntity");
        }
    }
}
