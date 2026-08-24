using Microsoft.EntityFrameworkCore.Migrations;

namespace SuperShop.Migrations
{
    public partial class AddProductToOrderDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OerderDetails_AspNetUsers_UserId",
                table: "OerderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OerderDetails_UserId",
                table: "OerderDetails");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "OerderDetails");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "OerderDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OerderDetails_ProductId",
                table: "OerderDetails",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OerderDetails_Products_ProductId",
                table: "OerderDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OerderDetails_Products_ProductId",
                table: "OerderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OerderDetails_ProductId",
                table: "OerderDetails");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "OerderDetails");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "OerderDetails",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OerderDetails_UserId",
                table: "OerderDetails",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OerderDetails_AspNetUsers_UserId",
                table: "OerderDetails",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
