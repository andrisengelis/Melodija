using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Melodija.Data.Migrations
{
    public partial class AddOwnerIdToReleaseList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "23be0619-b49e-4608-b158-d676e792d8d1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8798c338-95d9-4303-b40d-fb51d5bd6852");

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "ReleaseLists",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "ReleaseLists");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8798c338-95d9-4303-b40d-fb51d5bd6852", "fe546389-b531-4669-8242-f4fe5230a6f1", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "23be0619-b49e-4608-b158-d676e792d8d1", "5dc47c30-312b-4d5f-b1d6-b11a919c71e6", "User", "USER" });
        }
    }
}
