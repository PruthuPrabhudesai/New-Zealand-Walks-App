using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalksAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDataforDifficultiesandRegions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("8c5a8a49-fc1f-46cd-ae4c-630db279c7cb"), "Medium" },
                    { new Guid("a99aaeef-419c-45c0-ad1c-874869be0491"), "Easy" },
                    { new Guid("f155af42-d7b4-4fc7-ada3-3b312e19b5a0"), "Hard" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("0c14408a-bc92-471f-8aad-92fb0a86f333"), "WLG", "Wellington", "https://example.com/region4.jpg" },
                    { new Guid("49546d38-2cfa-4970-8f91-a44ec708133b"), "NTL", "Northland", "https://example.com/region2.jpg" },
                    { new Guid("54b2e727-6470-47eb-826e-69b71d76e281"), "AKL", "Aukland", "https://example.com/aukland.jpg" },
                    { new Guid("9d006462-fe84-4157-8b82-02e9f0e06c8b"), "BOP", "Bay of Plenty", "https://example.com/region3.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("8c5a8a49-fc1f-46cd-ae4c-630db279c7cb"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("a99aaeef-419c-45c0-ad1c-874869be0491"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("f155af42-d7b4-4fc7-ada3-3b312e19b5a0"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("0c14408a-bc92-471f-8aad-92fb0a86f333"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("49546d38-2cfa-4970-8f91-a44ec708133b"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("54b2e727-6470-47eb-826e-69b71d76e281"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("9d006462-fe84-4157-8b82-02e9f0e06c8b"));
        }
    }
}
