using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace User1.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    userId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    userTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    salt = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "UserType",
                columns: table => new
                {
                    userTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserType", x => x.userTypeId);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "userId", "name", "password", "salt", "surname", "userTypeId", "username" },
                values: new object[,]
                {
                    { new Guid("18a38124-3eb4-44dc-941a-1d164661b615"), "Una", "123456", "", "Obradovic", new Guid("95d791cc-a0c9-4ebd-a598-9ccad0022a78"), "UUna" },
                    { new Guid("90fa6cde-79e2-4b82-b0a5-28d70c66e2dd"), "Dusan", "123456", "", "Markovic", new Guid("50d0d37e-b01a-4d48-aa12-be3acc5cf379"), "MMarkovic" }
                });

            migrationBuilder.InsertData(
                table: "UserType",
                columns: new[] { "userTypeId", "role" },
                values: new object[] { new Guid("17f97a34-89b3-48fa-a6c0-265d15a18d3c"), "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "UserType");
        }
    }
}
