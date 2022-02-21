using Microsoft.EntityFrameworkCore.Migrations;

namespace ForumDAL.Migrations
{
    public partial class Update9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4b0db370-e97a-40ef-8327-05b5f28bf8f0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a969bcae-a279-468b-9489-162c134b147b");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Topics",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "10cfdcb6-57ef-480b-9e29-5110dcdc4d32", "40f87367-7a02-48c9-befb-c3704904fe6e", "user", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d9af550d-8e6c-40e7-b9e8-594fbcb84b00", "e1ccb103-f552-4013-b3f6-7eab62a5c142", "admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_Topics_UserId",
                table: "Topics",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Topics_AspNetUsers_UserId",
                table: "Topics",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Topics_AspNetUsers_UserId",
                table: "Topics");

            migrationBuilder.DropIndex(
                name: "IX_Topics_UserId",
                table: "Topics");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "10cfdcb6-57ef-480b-9e29-5110dcdc4d32");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d9af550d-8e6c-40e7-b9e8-594fbcb84b00");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Topics");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3c5da287-956b-4f87-95ae-98fef927db14", "6a1be6d1-e520-41ff-a669-304ca295687e", "user", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "63b3f8e1-c23f-4bcf-944e-65c47aaa59bc", "0c5d9ef6-0754-4f32-aeb4-fa21a2bfb8df", "admin", "ADMIN" });
        }
    }
}
