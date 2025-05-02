using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    /// <inheritdoc />
    public partial class DeleteBeheavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartProducts_Carts_CartId",
                table: "CartProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleProducts_CartProducts_CartProductId",
                table: "SaleProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleProducts_Sales_SaleId",
                table: "SaleProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Carts_CartId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_CartId",
                table: "Sales");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_CartId",
                table: "Sales",
                column: "CartId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CartProducts_Carts_CartId",
                table: "CartProducts",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleProducts_CartProducts_CartProductId",
                table: "SaleProducts",
                column: "CartProductId",
                principalTable: "CartProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleProducts_Sales_SaleId",
                table: "SaleProducts",
                column: "SaleId",
                principalTable: "Sales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Carts_CartId",
                table: "Sales",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartProducts_Carts_CartId",
                table: "CartProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleProducts_CartProducts_CartProductId",
                table: "SaleProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleProducts_Sales_SaleId",
                table: "SaleProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Carts_CartId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_CartId",
                table: "Sales");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Products",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_CartId",
                table: "Sales",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartProducts_Carts_CartId",
                table: "CartProducts",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleProducts_CartProducts_CartProductId",
                table: "SaleProducts",
                column: "CartProductId",
                principalTable: "CartProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleProducts_Sales_SaleId",
                table: "SaleProducts",
                column: "SaleId",
                principalTable: "Sales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Carts_CartId",
                table: "Sales",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
