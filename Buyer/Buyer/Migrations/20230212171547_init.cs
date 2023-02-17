using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Buyer.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "contactPerson",
                columns: table => new
                {
                    contactPersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    function = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contactPerson", x => x.contactPersonId);
                });

            migrationBuilder.CreateTable(
                name: "individuals",
                columns: table => new
                {
                    buyerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    individualName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    individualSurname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    individualId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    buyerType = table.Column<bool>(type: "bit", nullable: false),
                    area = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ban = table.Column<bool>(type: "bit", nullable: false),
                    banStartingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    banLasting = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    banEndingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuthorizedPersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    phoneNumber1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phoneNumber2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    emailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    accountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    addressId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    paymentId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    publicBiddingId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    priorityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_individuals", x => x.buyerId);
                });

            migrationBuilder.CreateTable(
                name: "legalEntities",
                columns: table => new
                {
                    buyerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    legalEntityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    legalEntityId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    legalEntityFax = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    contactPerson = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    buyerType = table.Column<bool>(type: "bit", nullable: false),
                    area = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ban = table.Column<bool>(type: "bit", nullable: false),
                    banStartingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    banLasting = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    banEndingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuthorizedPersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    phoneNumber1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phoneNumber2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    emailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    accountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    addressId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    paymentId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    publicBiddingId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    priorityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_legalEntities", x => x.buyerId);
                });

            migrationBuilder.CreateTable(
                name: "priorities",
                columns: table => new
                {
                    priorityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    priorityType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_priorities", x => x.priorityId);
                });

            migrationBuilder.InsertData(
                table: "contactPerson",
                columns: new[] { "contactPersonId", "function", "name", "phoneNumber", "surname" },
                values: new object[,]
                {
                    { new Guid("65979e67-38d1-4b1f-b636-2d8c09de25ea"), "function2", "Almir", "02434354224", "Salihbegovic" },
                    { new Guid("e1ed563f-e902-4d84-92c9-ae1e066952a2"), "function1", "Amila", "03245345654", "Salihbegovic" }
                });

            migrationBuilder.InsertData(
                table: "individuals",
                columns: new[] { "buyerId", "AuthorizedPersonId", "accountNumber", "addressId", "area", "ban", "banEndingDate", "banLasting", "banStartingDate", "buyerType", "emailAddress", "individualId", "individualName", "individualSurname", "paymentId", "phoneNumber1", "phoneNumber2", "priorityId", "publicBiddingId" },
                values: new object[] { new Guid("f5a13586-1a5c-4fea-adbe-0f352ca13371"), null, "2489de9e32", "addresstesttt", "15000", false, new DateTime(1900, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), "0", new DateTime(1900, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), true, "123@gmail.com", "280100798916", "Amila", "Salihbegovic", "111111111111", "2131231412", "8974839473", new Guid("12c7b642-416e-4358-90ca-9ddb67336f63"), "bidding1" });

            migrationBuilder.InsertData(
                table: "legalEntities",
                columns: new[] { "buyerId", "AuthorizedPersonId", "accountNumber", "addressId", "area", "ban", "banEndingDate", "banLasting", "banStartingDate", "buyerType", "contactPerson", "emailAddress", "legalEntityFax", "legalEntityId", "legalEntityName", "paymentId", "phoneNumber1", "phoneNumber2", "priorityId", "publicBiddingId" },
                values: new object[] { new Guid("2f108769-aaf3-4829-9263-72523bbb223e"), null, "23534234563", "addresstestno2", "155000", true, new DateTime(2023, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), "355", new DateTime(2022, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), false, new Guid("e1ed563f-e902-4d84-92c9-ae1e066952a2"), "34455@gmail.com", "fax", "12432434", "name", "vvvvvvvvvvvvvvv", "2345435675", "8974839473", new Guid("1bb9cb0a-a2ad-4ff3-bbaa-ba312e968a9b"), "bidding2" });

            migrationBuilder.InsertData(
                table: "priorities",
                columns: new[] { "priorityId", "priorityType" },
                values: new object[,]
                {
                    { new Guid("12c7b642-416e-4358-90ca-9ddb67336f63"), "Test priority type number 2" },
                    { new Guid("1bb9cb0a-a2ad-4ff3-bbaa-ba312e968a9b"), "Test priority number 1" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contactPerson");

            migrationBuilder.DropTable(
                name: "individuals");

            migrationBuilder.DropTable(
                name: "legalEntities");

            migrationBuilder.DropTable(
                name: "priorities");
        }
    }
}
