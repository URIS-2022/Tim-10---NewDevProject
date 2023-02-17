using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Personality.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Personalities",
                columns: table => new
                {
                    personalityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    function = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personalities", x => x.personalityId);
                });

            migrationBuilder.InsertData(
                table: "Personalities",
                columns: new[] { "personalityId", "function", "name", "surname" },
                values: new object[,]
                {
                    { new Guid("1f6f2144-1655-410c-9e6a-adb3662a46f8"), "funkcija", "Ivana", "Ivanovic" },
                    { new Guid("e5ee8b4d-110f-4066-81f2-a03144fbcaaf"), "test", "Pera", "Peric" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Personalities");
        }
    }
}
