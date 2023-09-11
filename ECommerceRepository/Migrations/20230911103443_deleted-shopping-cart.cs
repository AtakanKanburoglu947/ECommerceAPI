using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceRepository.Migrations
{
    /// <inheritdoc />
    public partial class deletedshoppingcart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ShoppingCarts_ShoppingCartId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_ShoppingCarts_ShoppingCartId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "ShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_Users_ShoppingCartId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Products_ShoppingCartId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ShoppingCartId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ShoppingCartId",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShoppingCartId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShoppingCartId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ShoppingCarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalPrice = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCarts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_ShoppingCartId",
                table: "Users",
                column: "ShoppingCartId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ShoppingCartId",
                table: "Products",
                column: "ShoppingCartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ShoppingCarts_ShoppingCartId",
                table: "Products",
                column: "ShoppingCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ShoppingCarts_ShoppingCartId",
                table: "Users",
                column: "ShoppingCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id");
        }
    }
}
