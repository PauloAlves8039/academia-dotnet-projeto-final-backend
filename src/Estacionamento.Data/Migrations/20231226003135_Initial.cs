using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estacionamento.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Enderecos",
                columns: table => new
                {
                    CodigoEndereco = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Logradouro = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Numero = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Complemento = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    Bairro = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Cidade = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Estado = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Cep = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Endereco__ECFD9712B78FD219", x => x.CodigoEndereco);
                });

            migrationBuilder.CreateTable(
                name: "Veiculos",
                columns: table => new
                {
                    CodigoVeiculo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Marca = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Modelo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Cor = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Ano = table.Column<int>(type: "int", nullable: false),
                    Observacoes = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Veiculos__39A965987907ACCA", x => x.CodigoVeiculo);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    CodigoCliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "date", nullable: false),
                    Cpf = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Telefone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    CodigoEndereco = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Clientes__E0DD7E71544D746E", x => x.CodigoCliente);
                    table.ForeignKey(
                        name: "FK__Clientes__Codigo__398D8EEE",
                        column: x => x.CodigoEndereco,
                        principalTable: "Enderecos",
                        principalColumn: "CodigoEndereco");
                });

            migrationBuilder.CreateTable(
                name: "ClienteVeiculos",
                columns: table => new
                {
                    CodigoClienteVeiculo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteId = table.Column<int>(type: "int", nullable: true),
                    VeiculoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ClienteV__67D54C7981DF8910", x => x.CodigoClienteVeiculo);
                    table.ForeignKey(
                        name: "FK__ClienteVe__Clien__3F466844",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "CodigoCliente");
                    table.ForeignKey(
                        name: "FK__ClienteVe__Veicu__403A8C7D",
                        column: x => x.VeiculoId,
                        principalTable: "Veiculos",
                        principalColumn: "CodigoVeiculo");
                });

            migrationBuilder.CreateTable(
                name: "Permanencias",
                columns: table => new
                {
                    CodigoPermanencia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteVeiculoId = table.Column<int>(type: "int", nullable: true),
                    Placa = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    DataEntrada = table.Column<DateTime>(type: "datetime", nullable: true),
                    DataSaida = table.Column<DateTime>(type: "datetime", nullable: true),
                    TaxaPorHora = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    EstadoPermanencia = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Permanen__089416FDE4EF7B8B", x => x.CodigoPermanencia);
                    table.ForeignKey(
                        name: "FK__Permanenc__Clien__440B1D61",
                        column: x => x.ClienteVeiculoId,
                        principalTable: "ClienteVeiculos",
                        principalColumn: "CodigoClienteVeiculo");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_CodigoEndereco",
                table: "Clientes",
                column: "CodigoEndereco");

            migrationBuilder.CreateIndex(
                name: "UQ__Clientes__C1FF93099270D3CD",
                table: "Clientes",
                column: "Cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClienteVeiculos_ClienteId",
                table: "ClienteVeiculos",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_ClienteVeiculos_VeiculoId",
                table: "ClienteVeiculos",
                column: "VeiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_Permanencias_ClienteVeiculoId",
                table: "Permanencias",
                column: "ClienteVeiculoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Permanencias");

            migrationBuilder.DropTable(
                name: "ClienteVeiculos");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Veiculos");

            migrationBuilder.DropTable(
                name: "Enderecos");
        }
    }
}
