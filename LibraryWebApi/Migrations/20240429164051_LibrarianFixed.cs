using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryWebApi.Migrations
{
    /// <inheritdoc />
    public partial class LibrarianFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4d2b262c-986e-4126-9fa6-bf39505f30e8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ff13a3cf-6868-4abc-b701-e2e7f48772ee");

            migrationBuilder.AlterColumn<string>(
                name: "RegisterLibrarianId",
                table: "Members",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "RegisterManagerId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "71a679a8-a96d-4ba7-9039-1310a0a3a04d", null, "Manager", "MANAGER" },
                    { "890ea22a-a7ba-4b17-a6e7-89bbc9639574", null, "Receptionist", "RECEPTIONIST" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "71a679a8-a96d-4ba7-9039-1310a0a3a04d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "890ea22a-a7ba-4b17-a6e7-89bbc9639574");

            migrationBuilder.AlterColumn<string>(
                name: "RegisterLibrarianId",
                table: "Members",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RegisterManagerId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4d2b262c-986e-4126-9fa6-bf39505f30e8", null, "Manager", "MANAGER" },
                    { "ff13a3cf-6868-4abc-b701-e2e7f48772ee", null, "Receptionist", "RECEPTIONIST" }
                });
        }
    }
}
