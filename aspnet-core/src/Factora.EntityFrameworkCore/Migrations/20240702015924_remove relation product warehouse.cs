using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Factora.Migrations
{
    /// <inheritdoc />
    public partial class removerelationproductwarehouse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Warehouse_WarehouseId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_WarehouseId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "Product");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "WarehouseId",
                table: "Product",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Product_WarehouseId",
                table: "Product",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Warehouse_WarehouseId",
                table: "Product",
                column: "WarehouseId",
                principalTable: "Warehouse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
