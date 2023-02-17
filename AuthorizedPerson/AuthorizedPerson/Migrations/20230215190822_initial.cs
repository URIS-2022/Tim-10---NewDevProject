using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AuthorizedPerson.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "authorizedPeople",
                columns: table => new
                {
                    authorizedPersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ducumentNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tableNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    addressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_authorizedPeople", x => x.authorizedPersonId);
                });

            migrationBuilder.InsertData(
                table: "authorizedPeople",
                columns: new[] { "authorizedPersonId", "addressId", "ducumentNumber", "name", "surname", "tableNumber" },
                values: new object[,]
                {
                    { new Guid("23f2a8ff-e5df-495b-8c11-0b64016b8551"), new Guid("fcc355e0-28c8-44f3-8e4b-3c5aff7d3903"), "2345323", "Almir", "Salihbegovic", "12345" },
                    { new Guid("6659fef1-30dc-4c5b-93e6-7f96beb1afef"), new Guid("e5a687a0-8f6e-4de4-8241-3b0feb36b0fd"), "1234453", "Amila", "Salihbegovic", "234" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "authorizedPeople");
        }
    }
}
