using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace complaint.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Action",
                columns: table => new
                {
                    actionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    actionName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Action", x => x.actionId);
                });

            migrationBuilder.CreateTable(
                name: "ComplaintStatus",
                columns: table => new
                {
                    complaintStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    statusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplaintStatus", x => x.complaintStatusId);
                });

            migrationBuilder.CreateTable(
                name: "ComplaintType",
                columns: table => new
                {
                    complaintTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    typeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplaintType", x => x.complaintTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Complaint",
                columns: table => new
                {
                    complaintId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    complaintTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    complaintDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    complaintSubmitter = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    cause = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rescriptDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    rescriptNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    complaintStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    decisionNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    complaintNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    actionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Complaint", x => x.complaintId);
                    table.ForeignKey(
                        name: "FK_Complaint_Action_actionId",
                        column: x => x.actionId,
                        principalTable: "Action",
                        principalColumn: "actionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Complaint_ComplaintStatus_complaintStatusId",
                        column: x => x.complaintStatusId,
                        principalTable: "ComplaintStatus",
                        principalColumn: "complaintStatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Complaint_ComplaintType_complaintTypeId",
                        column: x => x.complaintTypeId,
                        principalTable: "ComplaintType",
                        principalColumn: "complaintTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Action",
                columns: new[] { "actionId", "actionName" },
                values: new object[,]
                {
                    { new Guid("0ff49176-03ff-4e8e-9878-038a56e35a5b"), "JN ide u drugi krug sa novim uslovima" },
                    { new Guid("228c0094-41ed-4455-bd11-0f024dd199e9"), "JN ne ide u drugi krug" },
                    { new Guid("df859a22-1ce8-466c-b919-f4cfbea3c7a6"), "JN ide u drugi krug sa starim uslovima" }
                });

            migrationBuilder.InsertData(
                table: "ComplaintStatus",
                columns: new[] { "complaintStatusId", "statusName" },
                values: new object[,]
                {
                    { new Guid("02b27d09-4958-4245-be2c-76e434e39351"), "Otvorena" },
                    { new Guid("436b9e51-057a-404d-ab52-155a2b4d8071"), "Odbijena" },
                    { new Guid("5c416d45-715a-4432-b2b6-2df9046fe828"), "Usvojena" }
                });

            migrationBuilder.InsertData(
                table: "ComplaintType",
                columns: new[] { "complaintTypeId", "typeName" },
                values: new object[,]
                {
                    { new Guid("071849eb-3561-40fe-9dcf-1f57fa7f6ff8"), "Žalba na Odluku o davanju na korišćenje" },
                    { new Guid("53b25384-45c5-4f30-8b27-3db311e855fb"), "Žalba na Odluku o davanju u zakup" },
                    { new Guid("f98de9dc-5a4a-4ee2-bccc-fba4134dd97a"), "Žalba na tok javnog nadmetanja" }
                });

            migrationBuilder.InsertData(
                table: "Complaint",
                columns: new[] { "complaintId", "actionId", "cause", "complaintDate", "complaintNumber", "complaintStatusId", "complaintSubmitter", "complaintTypeId", "decisionNumber", "reason", "rescriptDate", "rescriptNumber" },
                values: new object[,]
                {
                    { new Guid("a6c49ae9-75f8-4685-8671-b74cc94ebfc0"), new Guid("0ff49176-03ff-4e8e-9878-038a56e35a5b"), "Krsenje pravilnika za javno nadmetanje", new DateTime(2023, 2, 15, 11, 0, 0, 0, DateTimeKind.Unspecified), "1234", new Guid("5c416d45-715a-4432-b2b6-2df9046fe828"), new Guid("702e05d2-afea-48b0-a8ae-48ac259915c1"), new Guid("f98de9dc-5a4a-4ee2-bccc-fba4134dd97a"), "1221", "Neispravnost prilikom dodeljivanja parcele", new DateTime(2023, 3, 11, 10, 0, 0, 0, DateTimeKind.Unspecified), "1035" },
                    { new Guid("b136e4a4-0009-4113-ad40-7f3a0483152b"), new Guid("df859a22-1ce8-466c-b919-f4cfbea3c7a6"), "Krsenje pravilnika za javno nadmetanje", new DateTime(2023, 2, 15, 11, 0, 0, 0, DateTimeKind.Unspecified), "1234", new Guid("5c416d45-715a-4432-b2b6-2df9046fe828"), new Guid("301ee496-b6f6-4ee4-b40e-6dd782b7e426"), new Guid("f98de9dc-5a4a-4ee2-bccc-fba4134dd97a"), "1221", "Neispravnost prilikom dodeljivanja parcele", new DateTime(2023, 3, 11, 10, 0, 0, 0, DateTimeKind.Unspecified), "1035" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Complaint_actionId",
                table: "Complaint",
                column: "actionId");

            migrationBuilder.CreateIndex(
                name: "IX_Complaint_complaintStatusId",
                table: "Complaint",
                column: "complaintStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Complaint_complaintTypeId",
                table: "Complaint",
                column: "complaintTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Complaint");

            migrationBuilder.DropTable(
                name: "Action");

            migrationBuilder.DropTable(
                name: "ComplaintStatus");

            migrationBuilder.DropTable(
                name: "ComplaintType");
        }
    }
}
