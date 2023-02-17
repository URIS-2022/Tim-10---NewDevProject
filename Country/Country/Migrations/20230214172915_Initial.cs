using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Country.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    addressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    place = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    zipCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.addressId);
                });

            migrationBuilder.CreateTable(
                name: "Country1",
                columns: table => new
                {
                    countryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nameConuntry = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country1", x => x.countryId);
                });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "addressId", "place", "street", "zipCode" },
                values: new object[] { new Guid("0dc88d24-82a7-4aec-9464-0d06b9e119ca"), "NS", "Milosa Crnjanskog 4", 21000 });

            migrationBuilder.InsertData(
                table: "Country1",
                columns: new[] { "countryId", "nameConuntry" },
                values: new object[] { new Guid("c1ba9fac-43fa-4502-9f93-d3f772dca929"), "Srbija" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Country1");
        }
    }
}
