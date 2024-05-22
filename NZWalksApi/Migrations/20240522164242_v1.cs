using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalksApi.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("3482f6ad-0168-47f9-ab8e-47bfe3b64caa"), "Hard" },
                    { new Guid("37424cdc-ee37-4e18-8c6a-c3220a514d3c"), "Medium" },
                    { new Guid("3795b306-830b-4856-a201-88560e737912"), "Easy" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImgUrl" },
                values: new object[,]
                {
                    { new Guid("1348229d-e55f-42ba-baf8-bf325b0318ef"), "JLF", "Jolfa", "Jolfapic.jpg" },
                    { new Guid("1590bc6d-357e-44a4-87b7-8a463661c9ad"), "HKM", "HAkim Nezami", "HakimNezamipic.jpg" },
                    { new Guid("5e8d3572-6583-4483-93c3-f1b44dab0ec2"), "NZH", "Nazhvan", "nazhvanpic.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("3482f6ad-0168-47f9-ab8e-47bfe3b64caa"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("37424cdc-ee37-4e18-8c6a-c3220a514d3c"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("3795b306-830b-4856-a201-88560e737912"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("1348229d-e55f-42ba-baf8-bf325b0318ef"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("1590bc6d-357e-44a4-87b7-8a463661c9ad"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("5e8d3572-6583-4483-93c3-f1b44dab0ec2"));
        }
    }
}
