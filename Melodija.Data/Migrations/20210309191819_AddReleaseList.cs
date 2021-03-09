using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Melodija.Data.Migrations
{
    public partial class AddReleaseList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReleaseLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReleaseLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReleaseListItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReleaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Position = table.Column<long>(type: "bigint", nullable: false),
                    ReleaseListId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReleaseListItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReleaseListItem_ReleaseLists_ReleaseListId",
                        column: x => x.ReleaseListId,
                        principalTable: "ReleaseLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReleaseListItem_Releases_ReleaseId",
                        column: x => x.ReleaseId,
                        principalTable: "Releases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReleaseListItem_ReleaseId",
                table: "ReleaseListItem",
                column: "ReleaseId");

            migrationBuilder.CreateIndex(
                name: "IX_ReleaseListItem_ReleaseListId",
                table: "ReleaseListItem",
                column: "ReleaseListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReleaseListItem");

            migrationBuilder.DropTable(
                name: "ReleaseLists");
        }
    }
}
