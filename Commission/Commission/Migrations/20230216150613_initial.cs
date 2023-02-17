using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Commission.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "President",
                columns: table => new
                {
                    presidentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    personalityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_President", x => x.presidentId);
                });

            migrationBuilder.CreateTable(
                name: "Commission",
                columns: table => new
                {
                    commissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nameOfCommission = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    presidentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commission", x => x.commissionId);
                    table.ForeignKey(
                        name: "FK_Commission_President_presidentId",
                        column: x => x.presidentId,
                        principalTable: "President",
                        principalColumn: "presidentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    memberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    personalityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    commissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.memberId);
                    table.ForeignKey(
                        name: "FK_Member_Commission_commissionId",
                        column: x => x.commissionId,
                        principalTable: "Commission",
                        principalColumn: "commissionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "President",
                columns: new[] { "presidentId", "personalityId" },
                values: new object[,]
                {
                    { new Guid("2d31d380-627e-4cd8-b29a-76d21c6c65b3"), new Guid("916c6f7b-651b-440d-b811-7fe9e35d38ab") },
                    { new Guid("99832b19-a420-4420-8fc9-10b22a7b0324"), new Guid("19e4a45d-0e40-42dc-b502-cd774f606e87") },
                    { new Guid("f5468f83-d3af-49df-8136-7d5323cad68b"), new Guid("274d5a86-9e8c-481a-bfac-7043db9ef65a") }
                });

            migrationBuilder.InsertData(
                table: "Commission",
                columns: new[] { "commissionId", "nameOfCommission", "presidentId" },
                values: new object[,]
                {
                    { new Guid("03dcf963-9569-4773-ae05-f205a97ffcc7"), "First", new Guid("f5468f83-d3af-49df-8136-7d5323cad68b") },
                    { new Guid("4b7c8b4d-bcb1-433b-bd88-38ff0032caaf"), "Third", new Guid("f5468f83-d3af-49df-8136-7d5323cad68b") },
                    { new Guid("c4e35c57-addb-4c11-b476-dc91f9ff14a2"), "Second", new Guid("f5468f83-d3af-49df-8136-7d5323cad68b") }
                });

            migrationBuilder.InsertData(
                table: "Member",
                columns: new[] { "memberId", "commissionId", "personalityId" },
                values: new object[,]
                {
                    { new Guid("1084e8c4-c92b-4ea3-9c20-0ca8bc8d5917"), new Guid("4b7c8b4d-bcb1-433b-bd88-38ff0032caaf"), new Guid("57029eb1-8c5f-4a55-824e-344a4df697ad") },
                    { new Guid("cf18da5d-07f3-4e56-90a2-03c8a9bde369"), new Guid("c4e35c57-addb-4c11-b476-dc91f9ff14a2"), new Guid("4a664b46-7959-4c8d-b830-618e3bea1aa2") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Commission_presidentId",
                table: "Commission",
                column: "presidentId");

            migrationBuilder.CreateIndex(
                name: "IX_Member_commissionId",
                table: "Member",
                column: "commissionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Member");

            migrationBuilder.DropTable(
                name: "Commission");

            migrationBuilder.DropTable(
                name: "President");
        }
    }
}
