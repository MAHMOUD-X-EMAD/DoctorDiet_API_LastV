using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoctorDiet.Data.Migrations
{
    /// <inheritdoc />
    public partial class init55 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plans_Doctors_DoctorId",
                table: "Plans");

            migrationBuilder.RenameColumn(
                name: "DoctorId",
                table: "Plans",
                newName: "DoctorID");

            migrationBuilder.RenameIndex(
                name: "IX_Plans_DoctorId",
                table: "Plans",
                newName: "IX_Plans_DoctorID");

            migrationBuilder.AlterColumn<string>(
                name: "DoctorID",
                table: "Plans",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DoctorName",
                table: "CustomPlans",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "goal",
                table: "CustomPlans",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_Doctors_DoctorID",
                table: "Plans",
                column: "DoctorID",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plans_Doctors_DoctorID",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "DoctorName",
                table: "CustomPlans");

            migrationBuilder.DropColumn(
                name: "goal",
                table: "CustomPlans");

            migrationBuilder.RenameColumn(
                name: "DoctorID",
                table: "Plans",
                newName: "DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_Plans_DoctorID",
                table: "Plans",
                newName: "IX_Plans_DoctorId");

            migrationBuilder.AlterColumn<string>(
                name: "DoctorId",
                table: "Plans",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_Doctors_DoctorId",
                table: "Plans",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id");
        }
    }
}
