using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Parcel.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CadastralMunicipality",
                columns: table => new
                {
                    cadastralMunicipalityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    cadastralMunicipalityName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CadastralMunicipality", x => x.cadastralMunicipalityId);
                });

            migrationBuilder.CreateTable(
                name: "Class",
                columns: table => new
                {
                    classId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    className = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Class", x => x.classId);
                });

            migrationBuilder.CreateTable(
                name: "Culture",
                columns: table => new
                {
                    cultureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    cultureName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cultureDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Culture", x => x.cultureId);
                });

            migrationBuilder.CreateTable(
                name: "Drainage",
                columns: table => new
                {
                    drainageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    drainageName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drainage", x => x.drainageId);
                });

            migrationBuilder.CreateTable(
                name: "FormOfProperty",
                columns: table => new
                {
                    formOfPropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    formOfPropertyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    formOfPropertyDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormOfProperty", x => x.formOfPropertyId);
                });

            migrationBuilder.CreateTable(
                name: "Parcel",
                columns: table => new
                {
                    parcelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    userOfParcelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    surface = table.Column<int>(type: "int", nullable: false),
                    parcelNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cadastralMunicipalityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    immovablePropertyListNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cultureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    classId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    workabilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    protectedZoneId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    formOfPropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    drainageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    workabilityRealCondition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cultureRealCondition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    classRealCondition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    protectedZoneRealCondition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    drainageRealCondition = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcel", x => x.parcelId);
                });

            migrationBuilder.CreateTable(
                name: "ProtectedZone",
                columns: table => new
                {
                    protectedZoneId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    protectedZoneName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProtectedZone", x => x.protectedZoneId);
                });

            migrationBuilder.CreateTable(
                name: "Workability",
                columns: table => new
                {
                    workabilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    workabilityName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workability", x => x.workabilityId);
                });

            migrationBuilder.InsertData(
                table: "CadastralMunicipality",
                columns: new[] { "cadastralMunicipalityId", "cadastralMunicipalityName" },
                values: new object[,]
                {
                    { new Guid("04b12cb0-5d05-4432-87da-81ca464011a2"), "Stari Grad" },
                    { new Guid("22d1f47a-75b6-4f78-add1-adbb38e009ef"), "Čantavir" },
                    { new Guid("2da53652-4366-45e7-9f4e-75b1768456f9"), "Donji Grad" },
                    { new Guid("44607cb5-c1ca-4cec-ab46-f0ef67f8656b"), "Novi Grad" },
                    { new Guid("56845d4b-4f9d-40cf-a58c-338949e3719f"), "Bajmok" },
                    { new Guid("60e4d41f-c6ce-48e5-918d-a834ea17efee"), "Žednik" },
                    { new Guid("6e079249-1322-4ebe-b336-d1d80f801fbc"), "Bački Vinogradi" },
                    { new Guid("7957db6a-ce96-4364-bce3-d712fac17292"), "Đuđin" },
                    { new Guid("8421a65c-1929-4e1b-b005-c19ce6b8084e"), "Tavankut" },
                    { new Guid("93921298-4aa8-4e64-ad44-d8dff1ed0d67"), "Bikovo" },
                    { new Guid("f427bdf0-4f91-4411-9e9f-085fc44bbdf7"), "Palić" }
                });

            migrationBuilder.InsertData(
                table: "Class",
                columns: new[] { "classId", "className" },
                values: new object[,]
                {
                    { new Guid("7af0e459-501b-47cd-9e4e-d329407b1c5b"), "II" },
                    { new Guid("9af0ab6f-c437-4959-9ea3-0c721214147b"), "I" },
                    { new Guid("a2d03f10-884e-49c8-a30e-e606a22dc2ba"), "III" },
                    { new Guid("b472b7c5-2648-40ca-a15f-37be449bc80a"), "VIII" },
                    { new Guid("c5b969b4-26ca-4a34-ac80-b26c2a2a5f17"), "VII" },
                    { new Guid("c8d44720-a933-4a4e-9e73-acc047535a5e"), "V" },
                    { new Guid("ed5e7420-3728-4829-8ac0-67c7ccdb506c"), "VI" },
                    { new Guid("fe3e5d4b-18e5-41d7-b35c-d00ec8171f5f"), "IV" }
                });

            migrationBuilder.InsertData(
                table: "Culture",
                columns: new[] { "cultureId", "cultureDescription", "cultureName" },
                values: new object[,]
                {
                    { new Guid("29b64040-ad2a-4859-a540-1d59a475652c"), "7", "Šume" },
                    { new Guid("2d9dcd0e-1705-4aca-b847-724a2c4c4877"), "4", "Vinogradi" },
                    { new Guid("4960ad3a-cd69-494e-864d-d4e410ce6094"), "1", "Njive" },
                    { new Guid("5475e615-4137-4cbc-8daf-4538eae0f37e"), "3", "Voćnjaci" },
                    { new Guid("581b4400-bcde-4fa5-ac85-72a6077af503"), "2", "Vrtovi" },
                    { new Guid("5fba88b4-c642-4e9e-a5c1-074f91f417df"), "8", "Trstici-močvare" },
                    { new Guid("f0d417fc-8e78-4d0f-a018-d6df8c270737"), "6", "Pašnjaci" },
                    { new Guid("ffcecfaa-b835-41c2-a568-57a564e7a03b"), "5", "Livade" }
                });

            migrationBuilder.InsertData(
                table: "Drainage",
                columns: new[] { "drainageId", "drainageName" },
                values: new object[,]
                {
                    { new Guid("d98745a1-7c94-417c-b31a-6efc83acaefc"), "Površinsko odvodnjavanje" },
                    { new Guid("e59f5cca-f6ab-44f4-9c04-698d0b310bc5"), "Podzemno odvodnjavanje" }
                });

            migrationBuilder.InsertData(
                table: "FormOfProperty",
                columns: new[] { "formOfPropertyId", "formOfPropertyDescription", "formOfPropertyName" },
                values: new object[,]
                {
                    { new Guid("109932fb-a5b9-4fab-8bb2-4d644df61245"), "2", "Državna svojina RS" },
                    { new Guid("2fa236f3-60d1-408d-ac04-bedbd4919bbb"), "4", "Društvena svojina" },
                    { new Guid("35636422-b81c-43c6-9a86-9486cf138c30"), "6", "Mešovita svojina" },
                    { new Guid("92351865-973d-4ec2-be9c-cdd81b849e99"), "3", "Državna svojina" },
                    { new Guid("9590edb7-593f-42ae-82b8-cf29298fe2d4"), "5", "Zadružna svojina" },
                    { new Guid("9b7b6c58-ace6-4fe8-9176-46323c00d005"), "7", "Drugi oblici" },
                    { new Guid("d54f5caa-a148-46ab-a6da-f83c9440cbab"), "1", "Privatna svojina" }
                });

            migrationBuilder.InsertData(
                table: "Parcel",
                columns: new[] { "parcelId", "cadastralMunicipalityId", "classId", "classRealCondition", "cultureId", "cultureRealCondition", "drainageId", "drainageRealCondition", "formOfPropertyId", "immovablePropertyListNumber", "parcelNumber", "protectedZoneId", "protectedZoneRealCondition", "surface", "userOfParcelId", "workabilityId", "workabilityRealCondition" },
                values: new object[] { new Guid("cf69a921-7a2f-4b8c-be71-92ee821b19ee"), new Guid("f427bdf0-4f91-4411-9e9f-085fc44bbdf7"), new Guid("7af0e459-501b-47cd-9e4e-d329407b1c5b"), "2", new Guid("ffcecfaa-b835-41c2-a568-57a564e7a03b"), "3", new Guid("d98745a1-7c94-417c-b31a-6efc83acaefc"), "1", new Guid("92351865-973d-4ec2-be9c-cdd81b849e99"), "LN101", "PC-2601", new Guid("5b1f0cbe-a20c-4747-80db-0b13af254388"), "4", 100, new Guid("9f122326-746a-426a-84d1-09501ae77664"), new Guid("69679e47-4d0b-4277-96f5-c1583a97abe8"), "5" });

            migrationBuilder.InsertData(
                table: "ProtectedZone",
                columns: new[] { "protectedZoneId", "protectedZoneName" },
                values: new object[,]
                {
                    { new Guid("12d2a98d-7890-44fa-bc30-ee3d77461a7f"), "2" },
                    { new Guid("3ee483c7-b878-4a0e-a742-8c87a287e5cd"), "4" },
                    { new Guid("5b1f0cbe-a20c-4747-80db-0b13af254388"), "1" },
                    { new Guid("a029cb37-3cb3-4b24-9db2-ef427401f1a5"), "3" }
                });

            migrationBuilder.InsertData(
                table: "Workability",
                columns: new[] { "workabilityId", "workabilityName" },
                values: new object[,]
                {
                    { new Guid("004b067a-bcbb-47d2-8869-550aeb147138"), "Ostalo" },
                    { new Guid("69679e47-4d0b-4277-96f5-c1583a97abe8"), "Obradivo" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CadastralMunicipality");

            migrationBuilder.DropTable(
                name: "Class");

            migrationBuilder.DropTable(
                name: "Culture");

            migrationBuilder.DropTable(
                name: "Drainage");

            migrationBuilder.DropTable(
                name: "FormOfProperty");

            migrationBuilder.DropTable(
                name: "Parcel");

            migrationBuilder.DropTable(
                name: "ProtectedZone");

            migrationBuilder.DropTable(
                name: "Workability");
        }
    }
}
