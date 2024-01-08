using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estacionamento.Data.Migrations
{
    public partial class RemoveDeleteCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Clientes__Codigo__398D8EEE",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK__ClienteVe__Clien__3F466844",
                table: "ClienteVeiculos");

            migrationBuilder.DropForeignKey(
                name: "FK__ClienteVe__Veicu__403A8C7D",
                table: "ClienteVeiculos");

            migrationBuilder.DropForeignKey(
                name: "FK__Permanenc__Clien__440B1D61",
                table: "Permanencias");

            migrationBuilder.AddForeignKey(
                name: "FK__Clientes__Codigo__398D8EEE",
                table: "Clientes",
                column: "CodigoEndereco",
                principalTable: "Enderecos",
                principalColumn: "CodigoEndereco");

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

            migrationBuilder.AddForeignKey(
                name: "FK__Permanenc__Clien__440B1D61",
                table: "Permanencias",
                column: "ClienteVeiculoId",
                principalTable: "ClienteVeiculos",
                principalColumn: "CodigoClienteVeiculo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Clientes__Codigo__398D8EEE",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK__ClienteVe__Clien__3F466844",
                table: "ClienteVeiculos");

            migrationBuilder.DropForeignKey(
                name: "FK__ClienteVe__Veicu__403A8C7D",
                table: "ClienteVeiculos");

            migrationBuilder.DropForeignKey(
                name: "FK__Permanenc__Clien__440B1D61",
                table: "Permanencias");

            migrationBuilder.AddForeignKey(
                name: "FK__Clientes__Codigo__398D8EEE",
                table: "Clientes",
                column: "CodigoEndereco",
                principalTable: "Enderecos",
                principalColumn: "CodigoEndereco",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK__Permanenc__Clien__440B1D61",
                table: "Permanencias",
                column: "ClienteVeiculoId",
                principalTable: "ClienteVeiculos",
                principalColumn: "CodigoClienteVeiculo",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
