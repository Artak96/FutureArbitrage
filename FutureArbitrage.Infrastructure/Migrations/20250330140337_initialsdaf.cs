using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FutureArbitrage.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initialsdaf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArbitrageResult_FutureContract_FuturesContract1Id",
                table: "ArbitrageResult");

            migrationBuilder.DropForeignKey(
                name: "FK_ArbitrageResult_FutureContract_FuturesContract2Id",
                table: "ArbitrageResult");

            migrationBuilder.DropForeignKey(
                name: "FK_FuturePrice_FutureContract_FutureContractId",
                table: "FuturePrice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FuturePrice",
                table: "FuturePrice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FutureContract",
                table: "FutureContract");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArbitrageResult",
                table: "ArbitrageResult");

            migrationBuilder.RenameTable(
                name: "FuturePrice",
                newName: "FuturePrices");

            migrationBuilder.RenameTable(
                name: "FutureContract",
                newName: "FutureContracts");

            migrationBuilder.RenameTable(
                name: "ArbitrageResult",
                newName: "ArbitrageResults");

            migrationBuilder.RenameIndex(
                name: "IX_FuturePrice_FutureContractId",
                table: "FuturePrices",
                newName: "IX_FuturePrices_FutureContractId");

            migrationBuilder.RenameIndex(
                name: "IX_ArbitrageResult_FuturesContract2Id",
                table: "ArbitrageResults",
                newName: "IX_ArbitrageResults_FuturesContract2Id");

            migrationBuilder.RenameIndex(
                name: "IX_ArbitrageResult_FuturesContract1Id",
                table: "ArbitrageResults",
                newName: "IX_ArbitrageResults_FuturesContract1Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FuturePrices",
                table: "FuturePrices",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FutureContracts",
                table: "FutureContracts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArbitrageResults",
                table: "ArbitrageResults",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArbitrageResults_FutureContracts_FuturesContract1Id",
                table: "ArbitrageResults",
                column: "FuturesContract1Id",
                principalTable: "FutureContracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ArbitrageResults_FutureContracts_FuturesContract2Id",
                table: "ArbitrageResults",
                column: "FuturesContract2Id",
                principalTable: "FutureContracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FuturePrices_FutureContracts_FutureContractId",
                table: "FuturePrices",
                column: "FutureContractId",
                principalTable: "FutureContracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArbitrageResults_FutureContracts_FuturesContract1Id",
                table: "ArbitrageResults");

            migrationBuilder.DropForeignKey(
                name: "FK_ArbitrageResults_FutureContracts_FuturesContract2Id",
                table: "ArbitrageResults");

            migrationBuilder.DropForeignKey(
                name: "FK_FuturePrices_FutureContracts_FutureContractId",
                table: "FuturePrices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FuturePrices",
                table: "FuturePrices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FutureContracts",
                table: "FutureContracts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArbitrageResults",
                table: "ArbitrageResults");

            migrationBuilder.RenameTable(
                name: "FuturePrices",
                newName: "FuturePrice");

            migrationBuilder.RenameTable(
                name: "FutureContracts",
                newName: "FutureContract");

            migrationBuilder.RenameTable(
                name: "ArbitrageResults",
                newName: "ArbitrageResult");

            migrationBuilder.RenameIndex(
                name: "IX_FuturePrices_FutureContractId",
                table: "FuturePrice",
                newName: "IX_FuturePrice_FutureContractId");

            migrationBuilder.RenameIndex(
                name: "IX_ArbitrageResults_FuturesContract2Id",
                table: "ArbitrageResult",
                newName: "IX_ArbitrageResult_FuturesContract2Id");

            migrationBuilder.RenameIndex(
                name: "IX_ArbitrageResults_FuturesContract1Id",
                table: "ArbitrageResult",
                newName: "IX_ArbitrageResult_FuturesContract1Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FuturePrice",
                table: "FuturePrice",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FutureContract",
                table: "FutureContract",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArbitrageResult",
                table: "ArbitrageResult",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArbitrageResult_FutureContract_FuturesContract1Id",
                table: "ArbitrageResult",
                column: "FuturesContract1Id",
                principalTable: "FutureContract",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ArbitrageResult_FutureContract_FuturesContract2Id",
                table: "ArbitrageResult",
                column: "FuturesContract2Id",
                principalTable: "FutureContract",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FuturePrice_FutureContract_FutureContractId",
                table: "FuturePrice",
                column: "FutureContractId",
                principalTable: "FutureContract",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
