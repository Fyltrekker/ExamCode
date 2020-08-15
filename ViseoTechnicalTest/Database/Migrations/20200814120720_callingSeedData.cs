using Microsoft.EntityFrameworkCore.Migrations;

namespace ViseoTechnicalTest.Database.Migrations
{
    public partial class callingSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "Firstname", "Lastname", "Password", "Status", "UserType", "Username" },
                values: new object[] { 1, "admin@admin.com", "Admin", "Admin", "Password1", 1, 1, "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);
        }
    }
}
