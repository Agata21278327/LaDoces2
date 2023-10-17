using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaDoces2.Migrations
{
    /// <inheritdoc />
    public partial class pedido1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidoItens_Itens_ItemId",
                table: "PedidoItens");

            migrationBuilder.DropColumn(
                name: "MovelId",
                table: "PedidoItens");

            migrationBuilder.AlterColumn<int>(
                name: "ItemId",
                table: "PedidoItens",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoItens_Itens_ItemId",
                table: "PedidoItens",
                column: "ItemId",
                principalTable: "Itens",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidoItens_Itens_ItemId",
                table: "PedidoItens");

            migrationBuilder.AlterColumn<int>(
                name: "ItemId",
                table: "PedidoItens",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "MovelId",
                table: "PedidoItens",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoItens_Itens_ItemId",
                table: "PedidoItens",
                column: "ItemId",
                principalTable: "Itens",
                principalColumn: "ItemId");
        }
    }
}
