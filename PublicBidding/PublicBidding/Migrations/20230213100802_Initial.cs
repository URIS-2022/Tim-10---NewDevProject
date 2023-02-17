using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PublicBidding.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "statusesOfPublicBidding",
                columns: table => new
                {
                    statusOfPublicBiddingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    statusOfPublicBiddingName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_statusesOfPublicBidding", x => x.statusOfPublicBiddingId);
                });

            migrationBuilder.CreateTable(
                name: "typesOfPublicBidding",
                columns: table => new
                {
                    typePublicBiddingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    typePublicBiddingName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_typesOfPublicBidding", x => x.typePublicBiddingId);
                });

            migrationBuilder.CreateTable(
                name: "publicBiddings",
                columns: table => new
                {
                    publicBiddingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    timeOfBeginning = table.Column<DateTime>(type: "datetime2", nullable: false),
                    timeOfEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    initialPricePerHectare = table.Column<int>(type: "int", nullable: false),
                    excepted = table.Column<bool>(type: "bit", nullable: false),
                    typePublicBiddingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    auctionedPrice = table.Column<int>(type: "int", nullable: false),
                    leasePeriod = table.Column<int>(type: "int", nullable: false),
                    numberOfParticipants = table.Column<int>(type: "int", nullable: false),
                    depositTopUpAmount = table.Column<int>(type: "int", nullable: false),
                    circle = table.Column<int>(type: "int", nullable: false),
                    statusOfPublicBiddingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    addressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    userId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    authorizedBidderPersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    buyerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_publicBiddings", x => x.publicBiddingId);
                    table.ForeignKey(
                        name: "FK_publicBiddings_statusesOfPublicBidding_statusOfPublicBiddingId",
                        column: x => x.statusOfPublicBiddingId,
                        principalTable: "statusesOfPublicBidding",
                        principalColumn: "statusOfPublicBiddingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_publicBiddings_typesOfPublicBidding_typePublicBiddingId",
                        column: x => x.typePublicBiddingId,
                        principalTable: "typesOfPublicBidding",
                        principalColumn: "typePublicBiddingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "licitations",
                columns: table => new
                {
                    licitationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    number = table.Column<int>(type: "int", nullable: false),
                    year = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    restrictions = table.Column<int>(type: "int", nullable: false),
                    priceDifference = table.Column<int>(type: "int", nullable: false),
                    publicBiddingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    deadlineForSubmissionOfApplications = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_licitations", x => x.licitationId);
                    table.ForeignKey(
                        name: "FK_licitations_publicBiddings_publicBiddingId",
                        column: x => x.publicBiddingId,
                        principalTable: "publicBiddings",
                        principalColumn: "publicBiddingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "statusesOfPublicBidding",
                columns: new[] { "statusOfPublicBiddingId", "statusOfPublicBiddingName" },
                values: new object[,]
                {
                    { new Guid("8587d9ba-baa5-44d0-9e47-50e0253bfa9b"), "Prvi krug" },
                    { new Guid("97c0078f-d01d-4d14-8a9c-46b0af243ed0"), "Drugi krug sa starim uslovima" },
                    { new Guid("a3fc511f-9d3e-4da6-82bb-20e289e9702f"), "Drugi krug sa novim uslovima" }
                });

            migrationBuilder.InsertData(
                table: "typesOfPublicBidding",
                columns: new[] { "typePublicBiddingId", "typePublicBiddingName" },
                values: new object[,]
                {
                    { new Guid("98ce7dce-7f2a-4142-85ef-c261c76c76c2"), "Javno otvaranje zatvorenih ponuda" },
                    { new Guid("e7ab1800-0064-49fc-9671-82a45ddc53f2"), "Javna licitacija" }
                });

            migrationBuilder.InsertData(
                table: "publicBiddings",
                columns: new[] { "publicBiddingId", "addressId", "auctionedPrice", "authorizedBidderPersonId", "buyerId", "circle", "date", "depositTopUpAmount", "excepted", "initialPricePerHectare", "leasePeriod", "numberOfParticipants", "statusOfPublicBiddingId", "timeOfBeginning", "timeOfEnd", "typePublicBiddingId", "userId" },
                values: new object[,]
                {
                    { new Guid("35d85c17-47a3-4c2d-831e-d9dc4243a670"), new Guid("7e96df4a-2908-4f39-8cc0-ba710615b2af"), 7500, new Guid("69fdc285-dd45-4fb9-9bc8-c5e42428c9f4"), new Guid("f1053e62-7e19-47cd-afcb-4d360838793e"), 1, new DateTime(2023, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 500, false, 5000, 12, 10, new Guid("8587d9ba-baa5-44d0-9e47-50e0253bfa9b"), new DateTime(2023, 2, 17, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 2, 17, 10, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e7ab1800-0064-49fc-9671-82a45ddc53f2"), new Guid("492f1a72-7ba4-4f7b-aa25-cffb8903ed48") },
                    { new Guid("cba217a7-7e8e-48a8-813a-9404cebf8f56"), new Guid("7e96df4a-2908-4f39-8cc0-ba710615b2af"), 6000, new Guid("69fdc285-dd45-4fb9-9bc8-c5e42428c9f4"), new Guid("f1053e62-7e19-47cd-afcb-4d360838793e"), 1, new DateTime(2023, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 400, false, 4000, 12, 10, new Guid("97c0078f-d01d-4d14-8a9c-46b0af243ed0"), new DateTime(2023, 2, 18, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 2, 18, 10, 0, 0, 0, DateTimeKind.Unspecified), new Guid("98ce7dce-7f2a-4142-85ef-c261c76c76c2"), new Guid("7836e78d-26d4-441d-843f-21062cda2240") }
                });

            migrationBuilder.InsertData(
                table: "licitations",
                columns: new[] { "licitationId", "date", "deadlineForSubmissionOfApplications", "number", "priceDifference", "publicBiddingId", "restrictions", "year" },
                values: new object[,]
                {
                    { new Guid("a856fea4-557f-4923-a5dc-febffd8b7744"), new DateTime(2023, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 200, new Guid("35d85c17-47a3-4c2d-831e-d9dc4243a670"), 1, 2023 },
                    { new Guid("eb72c2d4-2159-4146-ad8d-11eb02791e8f"), new DateTime(2023, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 100, new Guid("35d85c17-47a3-4c2d-831e-d9dc4243a670"), 1, 2023 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_licitations_publicBiddingId",
                table: "licitations",
                column: "publicBiddingId");

            migrationBuilder.CreateIndex(
                name: "IX_publicBiddings_statusOfPublicBiddingId",
                table: "publicBiddings",
                column: "statusOfPublicBiddingId");

            migrationBuilder.CreateIndex(
                name: "IX_publicBiddings_typePublicBiddingId",
                table: "publicBiddings",
                column: "typePublicBiddingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "licitations");

            migrationBuilder.DropTable(
                name: "publicBiddings");

            migrationBuilder.DropTable(
                name: "statusesOfPublicBidding");

            migrationBuilder.DropTable(
                name: "typesOfPublicBidding");
        }
    }
}
