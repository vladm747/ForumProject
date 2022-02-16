using Microsoft.EntityFrameworkCore.Migrations;

namespace ForumDAL.Migrations
{
    public partial class Update6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData(
            //    table: "AspNetRoles",
            //    keyColumn: "Id",
            //    keyValue: "1164bafb-86fc-45c5-aaa0-e48cae03d71e");

            //migrationBuilder.DeleteData(
            //    table: "AspNetRoles",
            //    keyColumn: "Id",
            //    keyValue: "d8a65cad-fe4a-43bf-8c54-457edbf582dd");

            //migrationBuilder.AlterColumn<string>(
            //    name: "Name",
            //    table: "Topics",
            //    maxLength: 40,
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(max)");

            //migrationBuilder.AlterColumn<string>(
            //    name: "Title",
            //    table: "Messages",
            //    maxLength: 40,
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(max)");

            //migrationBuilder.AlterColumn<string>(
            //    name: "Content",
            //    table: "Messages",
            //    maxLength: 300,
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(max)");

            //migrationBuilder.InsertData(
            //    table: "AspNetRoles",
            //    columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
            //    values: new object[] { "5c64f270-69a9-4ca6-83bf-c17fb15266d9", "04e3cffc-105d-487d-b1e7-f35287e3196a", "user", "USER" });

            //migrationBuilder.InsertData(
            //    table: "AspNetRoles",
            //    columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
            //    values: new object[] { "5b1dbc00-48c3-4470-8048-ee6d50dd7ec0", "ac7d2267-fc6b-43ce-9e38-f0fb6c0755c7", "admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5b1dbc00-48c3-4470-8048-ee6d50dd7ec0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5c64f270-69a9-4ca6-83bf-c17fb15266d9");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Topics",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 300);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d8a65cad-fe4a-43bf-8c54-457edbf582dd", "88666821-b2bc-444f-8ff7-18e6d6161dd0", "user", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1164bafb-86fc-45c5-aaa0-e48cae03d71e", "d39d33e8-62e0-45f4-996a-e3cd65d803dc", "admin", "ADMIN" });
        }
    }
}
