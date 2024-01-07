using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estacionamento.Data.Migrations
{
    public partial class AddRemoveEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__ClienteVe__Clien__3F466844",
                table: "ClienteVeiculos");

            migrationBuilder.DropForeignKey(
                name: "FK__ClienteVe__Veicu__403A8C7D",
                table: "ClienteVeiculos");

            migrationBuilder.AddForeignKey(
                name: "FK__ClienteVe__Clien__3F466844",
                table: "ClienteVeiculos",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "CodigoCliente",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__ClienteVe__Veicu__403A8C7D",
                table: "ClienteVeiculos",
                column: "VeiculoId",
                principalTable: "Veiculos",
                principalColumn: "CodigoVeiculo",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__ClienteVe__Clien__3F466844",
                table: "ClienteVeiculos");

            migrationBuilder.DropForeignKey(
                name: "FK__ClienteVe__Veicu__403A8C7D",
                table: "ClienteVeiculos");

            migrationBuilder.AddForeignKey(
                name: "FK__ClienteVe__Clien__3F466844",
                table: "ClienteVeiculos",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "CodigoCliente");

            migrationBuilder.AddForeignKey(
                name: "FK__ClienteVe__Veicu__403A8C7D",
                table: "ClienteVeiculos",
                column: "VeiculoId",
                principalTable: "Veiculos",
                principalColumn: "CodigoVeiculo");
        }
    }
}
