using Microsoft.EntityFrameworkCore.Migrations;

namespace ForumDAL.Migrations
{
    public partial class Update7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData(
            //    table: "AspNetRoles",
            //    keyColumn: "Id",
            //    keyValue: "5b1dbc00-48c3-4470-8048-ee6d50dd7ec0");

            //migrationBuilder.DeleteData(
            //    table: "AspNetRoles",
            //    keyColumn: "Id",
            //    keyValue: "5c64f270-69a9-4ca6-83bf-c17fb15266d9");

            //migrationBuilder.AddColumn<string>(
            //    name: "UserId",
            //    table: "Messages",
            //    nullable: true);

            //migrationBuilder.InsertData(
            //    table: "AspNetRoles",
            //    columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
            //    values: new object[] { "3c5da287-956b-4f87-95ae-98fef927db14", "6a1be6d1-e520-41ff-a669-304ca295687e", "user", "USER" });

            //migrationBuilder.InsertData(
            //    table: "AspNetRoles",
            //    columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
            //    values: new object[] { "63b3f8e1-c23f-4bcf-944e-65c47aaa59bc", "0c5d9ef6-0754-4f32-aeb4-fa21a2bfb8df", "admin", "ADMIN" });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Messages_UserId",
            //    table: "Messages",
            //    column: "UserId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Messages_AspNetUsers_UserId",
            //    table: "Messages",
            //    column: "UserId",
            //    principalTable: "AspNetUsers",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_UserId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_UserId",
                table: "Messages");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c5da287-956b-4f87-95ae-98fef927db14");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "63b3f8e1-c23f-4bcf-944e-65c47aaa59bc");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Messages");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5c64f270-69a9-4ca6-83bf-c17fb15266d9", "04e3cffc-105d-487d-b1e7-f35287e3196a", "user", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5b1dbc00-48c3-4470-8048-ee6d50dd7ec0", "ac7d2267-fc6b-43ce-9e38-f0fb6c0755c7", "admin", "ADMIN" });
        }
    }
}
