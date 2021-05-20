using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClinicDatabaseImplement.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    F_Name = table.Column<string>(nullable: false),
                    L_Name = table.Column<string>(nullable: false),
                    Login = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    EMail = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medicines",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Class = table.Column<string>(nullable: false),
                    DrugCourseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Receipts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dose = table.Column<string>(nullable: false),
                    PerDose = table.Column<int>(nullable: false),
                    MedicineId = table.Column<int>(nullable: true),
                    SymptomaticsId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Diseases",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorId = table.Column<int>(nullable: false),
                    DiseaseName = table.Column<string>(nullable: false),
                    Class = table.Column<string>(nullable: false),
                    Difficulty = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diseases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Diseases_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DrugCourses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorId = table.Column<int>(nullable: false),
                    Length = table.Column<decimal>(nullable: false),
                    FormedDate = table.Column<DateTime>(nullable: false),
                    MedicineId = table.Column<int>(nullable: true),
                    MedicineId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugCourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DrugCourses_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DrugCourses_Medicines_MedicineId1",
                        column: x => x.MedicineId1,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Symptomatics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorId = table.Column<int>(nullable: false),
                    SymptomName = table.Column<string>(nullable: false),
                    IssueDate = table.Column<DateTime>(nullable: false),
                    Heart = table.Column<string>(nullable: false),
                    Liver = table.Column<string>(nullable: false),
                    Lungs = table.Column<string>(nullable: false),
                    ReceiptId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Symptomatics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Symptomatics_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Symptomatics_Receipts_ReceiptId",
                        column: x => x.ReceiptId,
                        principalTable: "Receipts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DrugCoursesDiseases",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DrugCourseId = table.Column<int>(nullable: false),
                    DiseaseId = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugCoursesDiseases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DrugCoursesDiseases_Diseases_DiseaseId",
                        column: x => x.DiseaseId,
                        principalTable: "Diseases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DrugCoursesDiseases_DrugCourses_DrugCourseId",
                        column: x => x.DrugCourseId,
                        principalTable: "DrugCourses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SymptomaticDiseases",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SymptomaticId = table.Column<int>(nullable: false),
                    DiseaseId = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SymptomaticDiseases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SymptomaticDiseases_Diseases_DiseaseId",
                        column: x => x.DiseaseId,
                        principalTable: "Diseases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SymptomaticDiseases_Symptomatics_SymptomaticId",
                        column: x => x.SymptomaticId,
                        principalTable: "Symptomatics",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Diseases_DoctorId",
                table: "Diseases",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DrugCourses_DoctorId",
                table: "DrugCourses",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DrugCourses_MedicineId1",
                table: "DrugCourses",
                column: "MedicineId1");

            migrationBuilder.CreateIndex(
                name: "IX_DrugCoursesDiseases_DiseaseId",
                table: "DrugCoursesDiseases",
                column: "DiseaseId");

            migrationBuilder.CreateIndex(
                name: "IX_DrugCoursesDiseases_DrugCourseId",
                table: "DrugCoursesDiseases",
                column: "DrugCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_SymptomaticDiseases_DiseaseId",
                table: "SymptomaticDiseases",
                column: "DiseaseId");

            migrationBuilder.CreateIndex(
                name: "IX_SymptomaticDiseases_SymptomaticId",
                table: "SymptomaticDiseases",
                column: "SymptomaticId");

            migrationBuilder.CreateIndex(
                name: "IX_Symptomatics_DoctorId",
                table: "Symptomatics",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Symptomatics_ReceiptId",
                table: "Symptomatics",
                column: "ReceiptId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DrugCoursesDiseases");

            migrationBuilder.DropTable(
                name: "SymptomaticDiseases");

            migrationBuilder.DropTable(
                name: "DrugCourses");

            migrationBuilder.DropTable(
                name: "Diseases");

            migrationBuilder.DropTable(
                name: "Symptomatics");

            migrationBuilder.DropTable(
                name: "Medicines");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Receipts");
        }
    }
}
