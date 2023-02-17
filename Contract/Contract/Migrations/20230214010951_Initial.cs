using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Contract.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TypeOfGuaranteeEntity",
                columns: table => new
                {
                    typeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfGuaranteeEntity", x => x.typeId);
                });

            migrationBuilder.CreateTable(
                name: "ContractEntity",
                columns: table => new
                {
                    contractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    typeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    documentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    referenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    publicBiddingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    dateOfContract = table.Column<DateTime>(type: "datetime2", nullable: false),
                    buyerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    deadline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    place = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dateOfSigning = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractEntity", x => x.contractId);
                    table.ForeignKey(
                        name: "FK_ContractEntity_TypeOfGuaranteeEntity_typeId",
                        column: x => x.typeId,
                        principalTable: "TypeOfGuaranteeEntity",
                        principalColumn: "typeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TypeOfGuaranteeEntity",
                columns: new[] { "typeId", "type" },
                values: new object[,]
                {
                    { new Guid("06cfbbbf-39e1-485c-bd54-3cb336f25242"), "Monthly" },
                    { new Guid("5e6f0201-b31a-4767-8087-910e3c91dcc4"), "Quarterly" }
                });

            migrationBuilder.InsertData(
                table: "ContractEntity",
                columns: new[] { "contractId", "buyerId", "dateOfContract", "dateOfSigning", "deadline", "documentId", "place", "publicBiddingId", "referenceNumber", "typeId" },
                values: new object[,]
                {
                    { new Guid("42889dfc-4e97-49b0-827e-80066dcf48a4"), new Guid("d14ee77f-b24c-4f06-9c6f-016552927e94"), new DateTime(2022, 12, 12, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 12, 12, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e8522d7b-5261-4588-907f-4dbba12d6aed"), "Subotica", new Guid("192cb74b-d8d9-4430-82a3-f585a7e89689"), "123/RS", new Guid("06cfbbbf-39e1-485c-bd54-3cb336f25242") },
                    { new Guid("edf365dc-83f7-4402-b1c4-ecd794952fd4"), new Guid("bbde3af2-1804-43ae-9d83-ac631a72d6f5"), new DateTime(2020, 9, 17, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 9, 17, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 27, 9, 0, 0, 0, DateTimeKind.Unspecified), new Guid("d450be56-6ca0-4624-8673-21d9b57517af"), "Novi Sad", new Guid("9128178c-b6bc-4c61-a58e-4d994ee9a4f5"), "123/RS", new Guid("5e6f0201-b31a-4767-8087-910e3c91dcc4") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractEntity_typeId",
                table: "ContractEntity",
                column: "typeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractEntity");

            migrationBuilder.DropTable(
                name: "TypeOfGuaranteeEntity");
        }
    }
}
