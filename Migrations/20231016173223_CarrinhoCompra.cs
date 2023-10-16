using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaDoces2.Migrations
{
    /// <inheritdoc />
    public partial class CarrinhoCompra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarrinhoItens",
                columns: table => new
                {
                    CarrinhoItemId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ItemId = table.Column<int>(type: "INTEGER", nullable: true),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false),
                    CarrinhoId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrinhoItens", x => x.CarrinhoItemId);
                    table.ForeignKey(
                        name: "FK_CarrinhoItens_Itens_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Itens",
                        principalColumn: "ItemId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhoItens_ItemId",
                table: "CarrinhoItens",
                column: "ItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarrinhoItens");
        }
    }
}
