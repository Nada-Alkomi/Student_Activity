using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialSliderAndSettin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "HomeSettings",
                keyColumn: "Id",
                keyValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "HomeSettings",
                columns: new[] { "Id", "Address", "CreatedAt", "FacebookLink", "MapLocationUrl", "PhoneNumber", "UniversityEmail", "UpdatedAt" },
                values: new object[] { 1, "6th of October City, Giza", new DateTime(2026, 4, 16, 7, 34, 37, 163, DateTimeKind.Utc).AddTicks(4789), "https://facebook.com/must", "https://goo.gl/maps/...", "16111", "info@must.edu.eg", null });
        }
    }
}
