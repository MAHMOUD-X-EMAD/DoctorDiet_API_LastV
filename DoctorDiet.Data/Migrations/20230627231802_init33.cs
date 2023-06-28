using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoctorDiet.Data.Migrations
{
    /// <inheritdoc />
    public partial class init33 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientNotes_Day_DayId",
                table: "PatientNotes");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.RenameColumn(
                name: "DayId",
                table: "PatientNotes",
                newName: "DayCustomPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_PatientNotes_DayId",
                table: "PatientNotes",
                newName: "IX_PatientNotes_DayCustomPlanId");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "PatientNotes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DoctorId",
                table: "PatientNotes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "DoctorNotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DayCustomPlanId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorNotes_DayCustomPlans_DayCustomPlanId",
                        column: x => x.DayCustomPlanId,
                        principalTable: "DayCustomPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorNotes_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorNotes_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientNotes_DoctorId",
                table: "PatientNotes",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorNotes_DayCustomPlanId",
                table: "DoctorNotes",
                column: "DayCustomPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorNotes_DoctorId",
                table: "DoctorNotes",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorNotes_PatientId",
                table: "DoctorNotes",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientNotes_DayCustomPlans_DayCustomPlanId",
                table: "PatientNotes",
                column: "DayCustomPlanId",
                principalTable: "DayCustomPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientNotes_Doctors_DoctorId",
                table: "PatientNotes",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientNotes_DayCustomPlans_DayCustomPlanId",
                table: "PatientNotes");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientNotes_Doctors_DoctorId",
                table: "PatientNotes");

            migrationBuilder.DropTable(
                name: "DoctorNotes");

            migrationBuilder.DropIndex(
                name: "IX_PatientNotes_DoctorId",
                table: "PatientNotes");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "PatientNotes");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "PatientNotes");

            migrationBuilder.RenameColumn(
                name: "DayCustomPlanId",
                table: "PatientNotes",
                newName: "DayId");

            migrationBuilder.RenameIndex(
                name: "IX_PatientNotes_DayCustomPlanId",
                table: "PatientNotes",
                newName: "IX_PatientNotes_DayId");

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayId = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notes_Day_DayId",
                        column: x => x.DayId,
                        principalTable: "Day",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notes_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notes_DayId",
                table: "Notes",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_DoctorId",
                table: "Notes",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientNotes_Day_DayId",
                table: "PatientNotes",
                column: "DayId",
                principalTable: "Day",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
