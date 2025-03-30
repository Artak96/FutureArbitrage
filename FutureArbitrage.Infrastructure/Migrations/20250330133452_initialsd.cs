using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FutureArbitrage.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initialsd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Asset",
                table: "FutureContract",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveryDate",
                table: "FutureContract",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Asset",
                table: "FutureContract");

            migrationBuilder.DropColumn(
                name: "DeliveryDate",
                table: "FutureContract");
        }
    }
}
