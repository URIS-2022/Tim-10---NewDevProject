using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Document.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StatusOfDocumentEntity",
                columns: table => new
                {
                    statusOfDocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    adopted = table.Column<bool>(type: "bit", nullable: false),
                    rejected = table.Column<bool>(type: "bit", nullable: false),
                    opened = table.Column<bool>(type: "bit", nullable: false),
                    modified = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusOfDocumentEntity", x => x.statusOfDocumentId);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfDocumentEntity",
                columns: table => new
                {
                    typeOfDocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    typeOfDocumentName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfDocumentEntity", x => x.typeOfDocumentId);
                });

            migrationBuilder.CreateTable(
                name: "DocumentEntity",
                columns: table => new
                {
                    documentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    statusOfDocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    typeOfDocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    referenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateAdoptionDocument = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentEntity", x => x.documentId);
                    table.ForeignKey(
                        name: "FK_DocumentEntity_StatusOfDocumentEntity_statusOfDocumentId",
                        column: x => x.statusOfDocumentId,
                        principalTable: "StatusOfDocumentEntity",
                        principalColumn: "statusOfDocumentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentEntity_TypeOfDocumentEntity_typeOfDocumentId",
                        column: x => x.typeOfDocumentId,
                        principalTable: "TypeOfDocumentEntity",
                        principalColumn: "typeOfDocumentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "StatusOfDocumentEntity",
                columns: new[] { "statusOfDocumentId", "adopted", "modified", "opened", "rejected" },
                values: new object[,]
                {
                    { new Guid("2f73e247-1181-4be5-bb27-d644bdf97026"), false, true, true, false },
                    { new Guid("f7097b00-a82f-4a74-9ea5-9b97d57ada4a"), true, false, false, false }
                });

            migrationBuilder.InsertData(
                table: "TypeOfDocumentEntity",
                columns: new[] { "typeOfDocumentId", "typeOfDocumentName" },
                values: new object[,]
                {
                    { new Guid("0e6e43af-d3e6-463f-89a2-ec35a45413e7"), "Rešenje o obrazovanju stručne komisije" },
                    { new Guid("55f97234-d821-4f3a-89eb-2f8171b302b6"), "Rešenje o obrazovanju Komisije za sprovođenje postupaka davanje poljoprivrednog zemljišta u zakup" },
                    { new Guid("94f2c14d-c3a4-4310-9b24-448afcaa2b81"), "Predlog godišnjeg Programa zaštite" },
                    { new Guid("d1c95cd9-5018-4b23-85bc-9af26063f80c"), "Predlog odluke o davanju u zakup" },
                    { new Guid("efe8e9aa-caf5-4969-8941-d02c05031d07"), "Saglasnost Ministarstva" }
                });

            migrationBuilder.InsertData(
                table: "DocumentEntity",
                columns: new[] { "documentId", "date", "dateAdoptionDocument", "referenceNumber", "statusOfDocumentId", "typeOfDocumentId", "userId" },
                values: new object[,]
                {
                    { new Guid("1391ad03-80d4-4b47-a2fd-79802aa870aa"), new DateTime(2021, 11, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 12, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), "15548/RS7", new Guid("f7097b00-a82f-4a74-9ea5-9b97d57ada4a"), new Guid("0e6e43af-d3e6-463f-89a2-ec35a45413e7"), new Guid("6ccf941e-b3bb-41a0-bac4-b11b0f27b4c3") },
                    { new Guid("cfe84b37-bb6d-498d-a546-5dee8758ed1a"), new DateTime(2019, 11, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 12, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), "17748/RS7", new Guid("2f73e247-1181-4be5-bb27-d644bdf97026"), new Guid("94f2c14d-c3a4-4310-9b24-448afcaa2b81"), new Guid("6ccf941e-b3bb-41a0-bac4-b11b0f27b4c3") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentEntity_statusOfDocumentId",
                table: "DocumentEntity",
                column: "statusOfDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentEntity_typeOfDocumentId",
                table: "DocumentEntity",
                column: "typeOfDocumentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentEntity");

            migrationBuilder.DropTable(
                name: "StatusOfDocumentEntity");

            migrationBuilder.DropTable(
                name: "TypeOfDocumentEntity");
        }
    }
}
