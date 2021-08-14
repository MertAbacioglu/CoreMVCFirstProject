using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreMVCIntro.Migrations
{
    public partial class updatedatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Employees",
                newName: "Created Date");

            migrationBuilder.CreateTable(
                name: "EmployeeProfile",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    SpecialDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeProfile", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EmployeeProfile_Employees_ID",
                        column: x => x.ID,
                        principalTable: "Employees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeProfile");

            migrationBuilder.RenameColumn(
                name: "Yaratılma Tarihi",
                table: "Employees",
                newName: "CreatedDate");
        }
    }
}
