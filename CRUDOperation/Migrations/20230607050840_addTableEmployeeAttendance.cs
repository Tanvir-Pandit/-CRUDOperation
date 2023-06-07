using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUDOperation.Migrations
{
    /// <inheritdoc />
    public partial class addTableEmployeeAttendance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeAttendances",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    employeeId = table.Column<int>(type: "int", nullable: false),
                    attendanceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isPresent = table.Column<bool>(type: "bit", nullable: false),
                    isAbsent = table.Column<bool>(type: "bit", nullable: false),
                    isOffday = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAttendances", x => x.id);
                    table.ForeignKey(
                        name: "FK_EmployeeAttendances_Employee_employeeId",
                        column: x => x.employeeId,
                        principalTable: "Employee",
                        principalColumn: "employeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAttendances_employeeId",
                table: "EmployeeAttendances",
                column: "employeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeAttendances");
        }
    }
}
