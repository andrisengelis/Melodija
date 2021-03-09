using Microsoft.EntityFrameworkCore.Migrations;

namespace Melodija.Data.Migrations
{
    public partial class AddReleaseListItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReleaseListItem_ReleaseLists_ReleaseListId",
                table: "ReleaseListItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ReleaseListItem_Releases_ReleaseId",
                table: "ReleaseListItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReleaseListItem",
                table: "ReleaseListItem");

            migrationBuilder.RenameTable(
                name: "ReleaseListItem",
                newName: "ReleaseListItems");

            migrationBuilder.RenameIndex(
                name: "IX_ReleaseListItem_ReleaseListId",
                table: "ReleaseListItems",
                newName: "IX_ReleaseListItems_ReleaseListId");

            migrationBuilder.RenameIndex(
                name: "IX_ReleaseListItem_ReleaseId",
                table: "ReleaseListItems",
                newName: "IX_ReleaseListItems_ReleaseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReleaseListItems",
                table: "ReleaseListItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReleaseListItems_ReleaseLists_ReleaseListId",
                table: "ReleaseListItems",
                column: "ReleaseListId",
                principalTable: "ReleaseLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReleaseListItems_Releases_ReleaseId",
                table: "ReleaseListItems",
                column: "ReleaseId",
                principalTable: "Releases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReleaseListItems_ReleaseLists_ReleaseListId",
                table: "ReleaseListItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ReleaseListItems_Releases_ReleaseId",
                table: "ReleaseListItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReleaseListItems",
                table: "ReleaseListItems");

            migrationBuilder.RenameTable(
                name: "ReleaseListItems",
                newName: "ReleaseListItem");

            migrationBuilder.RenameIndex(
                name: "IX_ReleaseListItems_ReleaseListId",
                table: "ReleaseListItem",
                newName: "IX_ReleaseListItem_ReleaseListId");

            migrationBuilder.RenameIndex(
                name: "IX_ReleaseListItems_ReleaseId",
                table: "ReleaseListItem",
                newName: "IX_ReleaseListItem_ReleaseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReleaseListItem",
                table: "ReleaseListItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReleaseListItem_ReleaseLists_ReleaseListId",
                table: "ReleaseListItem",
                column: "ReleaseListId",
                principalTable: "ReleaseLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReleaseListItem_Releases_ReleaseId",
                table: "ReleaseListItem",
                column: "ReleaseId",
                principalTable: "Releases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
