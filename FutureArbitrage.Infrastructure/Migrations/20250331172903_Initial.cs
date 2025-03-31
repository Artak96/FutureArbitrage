using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FutureArbitrage.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FutureContracts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Symbol = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Asset = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FutureContracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArbitrageResults",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PriceF1 = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    PriceF2 = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    PriceDifference = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    FuturesContract1Id = table.Column<long>(type: "bigint", nullable: false),
                    FuturesContract2Id = table.Column<long>(type: "bigint", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArbitrageResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArbitrageResults_FutureContracts_FuturesContract1Id",
                        column: x => x.FuturesContract1Id,
                        principalTable: "FutureContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ArbitrageResults_FutureContracts_FuturesContract2Id",
                        column: x => x.FuturesContract2Id,
                        principalTable: "FutureContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FuturePrices",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FutureContractId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuturePrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FuturePrices_FutureContracts_FutureContractId",
                        column: x => x.FutureContractId,
                        principalTable: "FutureContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArbitrageResults_FuturesContract1Id",
                table: "ArbitrageResults",
                column: "FuturesContract1Id");

            migrationBuilder.CreateIndex(
                name: "IX_ArbitrageResults_FuturesContract2Id",
                table: "ArbitrageResults",
                column: "FuturesContract2Id");

            migrationBuilder.CreateIndex(
                name: "IX_FuturePrices_FutureContractId",
                table: "FuturePrices",
                column: "FutureContractId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArbitrageResults");

            migrationBuilder.DropTable(
                name: "FuturePrices");

            migrationBuilder.DropTable(
                name: "FutureContracts");
        }
    }
}
