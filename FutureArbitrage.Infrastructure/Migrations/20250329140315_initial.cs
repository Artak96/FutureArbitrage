using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FutureArbitrage.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FutureContract",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Symbol = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Asset = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FutureContract", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArbitrageResult",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PriceF1 = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    PriceF2 = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    PriceDifference = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    FuturesContract1Id = table.Column<long>(type: "bigint", nullable: false),
                    FuturesContract2Id = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArbitrageResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArbitrageResult_FutureContract_FuturesContract1Id",
                        column: x => x.FuturesContract1Id,
                        principalTable: "FutureContract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ArbitrageResult_FutureContract_FuturesContract2Id",
                        column: x => x.FuturesContract2Id,
                        principalTable: "FutureContract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FuturePrice",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    FutureContractId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuturePrice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FuturePrice_FutureContract_FutureContractId",
                        column: x => x.FutureContractId,
                        principalTable: "FutureContract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArbitrageResult_FuturesContract1Id",
                table: "ArbitrageResult",
                column: "FuturesContract1Id");

            migrationBuilder.CreateIndex(
                name: "IX_ArbitrageResult_FuturesContract2Id",
                table: "ArbitrageResult",
                column: "FuturesContract2Id");

            migrationBuilder.CreateIndex(
                name: "IX_FuturePrice_FutureContractId",
                table: "FuturePrice",
                column: "FutureContractId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArbitrageResult");

            migrationBuilder.DropTable(
                name: "FuturePrice");

            migrationBuilder.DropTable(
                name: "FutureContract");
        }
    }
}
