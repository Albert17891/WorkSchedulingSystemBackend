using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkSchedulingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixScheduleAndJob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Schedules_AssignedUserId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "ScheduledDate",
                table: "Jobs");

            migrationBuilder.RenameColumn(
                name: "AssignedUserId",
                table: "Jobs",
                newName: "ScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_Jobs_AssignedUserId",
                table: "Jobs",
                newName: "IX_Jobs_ScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Schedules_ScheduleId",
                table: "Jobs",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Schedules_ScheduleId",
                table: "Jobs");

            migrationBuilder.RenameColumn(
                name: "ScheduleId",
                table: "Jobs",
                newName: "AssignedUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Jobs_ScheduleId",
                table: "Jobs",
                newName: "IX_Jobs_AssignedUserId");

            migrationBuilder.AddColumn<DateTime>(
                name: "ScheduledDate",
                table: "Jobs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Schedules_AssignedUserId",
                table: "Jobs",
                column: "AssignedUserId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
