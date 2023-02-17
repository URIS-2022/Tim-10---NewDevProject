using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace payment.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExchangeRates",
                columns: table => new
                {
                    exchangeRateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    value = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeRates", x => x.exchangeRateId);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    paymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    accountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    referenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    amount = table.Column<float>(type: "real", nullable: false),
                    paymentPurpose = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    exchangeRateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    buyerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    publicBiddingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.paymentId);
                    table.ForeignKey(
                        name: "FK_Payments_ExchangeRates_exchangeRateId",
                        column: x => x.exchangeRateId,
                        principalTable: "ExchangeRates",
                        principalColumn: "exchangeRateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ExchangeRates",
                columns: new[] { "exchangeRateId", "currency", "date", "value" },
                values: new object[,]
                {
                    { new Guid("a7170f6a-33a1-431e-9b61-267aaf398297"), "RSD", new DateTime(2023, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 4321f },
                    { new Guid("d45da337-effc-4d60-8d22-79d27f248d7f"), "RSD", new DateTime(2023, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1234f }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "paymentId", "accountNumber", "amount", "buyerId", "date", "exchangeRateId", "paymentPurpose", "publicBiddingId", "referenceNumber" },
                values: new object[,]
                {
                    { new Guid("2475979c-1afe-437a-acc1-42c749f9c900"), "236541", 4321f, new Guid("367de211-7928-4bb6-8eea-81a1e77397fe"), new DateTime(2022, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("a7170f6a-33a1-431e-9b61-267aaf398297"), "Uplata javnog nadmetanja", new Guid("dcc0aef3-2598-4b54-b3ef-853696f57488"), "147852" },
                    { new Guid("2fb18b50-a4f2-4b06-a060-c8b84e4bc349"), "236541", 4321f, new Guid("9aab5a84-057f-44a5-a382-8d066c36a342"), new DateTime(2023, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("d45da337-effc-4d60-8d22-79d27f248d7f"), "Uplata javnog nadmetanja", new Guid("dcc0aef3-2598-4b54-b3ef-853696f57488"), "147852" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_exchangeRateId",
                table: "Payments",
                column: "exchangeRateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "ExchangeRates");
        }
    }
}
