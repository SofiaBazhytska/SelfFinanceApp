using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SelfFinance.Core.Migrations
{
    /// <inheritdoc />
    public partial class SeedFinancialOperations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "IsIncome", "Name" },
                values: new object[,]
                {
                    { 1, true, "Salary" },
                    { 2, false, "Food" }
                });

            migrationBuilder.InsertData(
                table: "Operations",
                columns: new[] { "Id", "Amount", "CategoryId", "Date", "Description", "IsIncome" },
                values: new object[,]
                {
                    { 1, 10000.00m, 1, new DateOnly(2024, 5, 1), "Salary", true },
                    { 2, 250.50m, 2, new DateOnly(2024, 5, 2), "Food", false },
                    { 3, 100.00m, 2, new DateOnly(2024, 5, 3), "Food", false }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Operations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Operations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Operations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2);
        }
    }
}
