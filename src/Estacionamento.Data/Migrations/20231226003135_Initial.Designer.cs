﻿// <auto-generated />
using System;
using Estacionamento.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Estacionamento.Data.Migrations
{
    [DbContext(typeof(EstacionamentoContext))]
    [Migration("20231226003135_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Estacionamento.Model.Models.Cliente", b =>
                {
                    b.Property<int>("CodigoCliente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CodigoCliente"), 1L, 1);

                    b.Property<int?>("CodigoEndereco")
                        .HasColumnType("int");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.HasKey("CodigoCliente")
                        .HasName("PK__Clientes__E0DD7E71544D746E");

                    b.HasIndex("CodigoEndereco");

                    b.HasIndex(new[] { "Cpf" }, "UQ__Clientes__C1FF93099270D3CD")
                        .IsUnique();

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("Estacionamento.Model.Models.ClienteVeiculo", b =>
                {
                    b.Property<int>("CodigoClienteVeiculo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CodigoClienteVeiculo"), 1L, 1);

                    b.Property<int?>("ClienteId")
                        .HasColumnType("int");

                    b.Property<int?>("VeiculoId")
                        .HasColumnType("int");

                    b.HasKey("CodigoClienteVeiculo")
                        .HasName("PK__ClienteV__67D54C7981DF8910");

                    b.HasIndex("ClienteId");

                    b.HasIndex("VeiculoId");

                    b.ToTable("ClienteVeiculos");
                });

            modelBuilder.Entity("Estacionamento.Model.Models.Endereco", b =>
                {
                    b.Property<int>("CodigoEndereco")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CodigoEndereco"), 1L, 1);

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Complemento")
                        .HasMaxLength(150)
                        .IsUnicode(false)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Logradouro")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.HasKey("CodigoEndereco")
                        .HasName("PK__Endereco__ECFD9712B78FD219");

                    b.ToTable("Enderecos");
                });

            modelBuilder.Entity("Estacionamento.Model.Models.Permanencia", b =>
                {
                    b.Property<int>("CodigoPermanencia")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CodigoPermanencia"), 1L, 1);

                    b.Property<int?>("ClienteVeiculoId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DataEntrada")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DataSaida")
                        .HasColumnType("datetime");

                    b.Property<string>("EstadoPermanencia")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Placa")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<decimal>("TaxaPorHora")
                        .HasColumnType("decimal(10,2)");

                    b.Property<decimal?>("ValorTotal")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("CodigoPermanencia")
                        .HasName("PK__Permanen__089416FDE4EF7B8B");

                    b.HasIndex("ClienteVeiculoId");

                    b.ToTable("Permanencias");
                });

            modelBuilder.Entity("Estacionamento.Model.Models.Veiculo", b =>
                {
                    b.Property<int>("CodigoVeiculo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CodigoVeiculo"), 1L, 1);

                    b.Property<int>("Ano")
                        .HasColumnType("int");

                    b.Property<string>("Cor")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Marca")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Modelo")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Observacoes")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Tipo")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.HasKey("CodigoVeiculo")
                        .HasName("PK__Veiculos__39A965987907ACCA");

                    b.ToTable("Veiculos");
                });

            modelBuilder.Entity("Estacionamento.Model.Models.Cliente", b =>
                {
                    b.HasOne("Estacionamento.Model.Models.Endereco", "Endereco")
                        .WithMany("Clientes")
                        .HasForeignKey("CodigoEndereco")
                        .HasConstraintName("FK__Clientes__Codigo__398D8EEE");

                    b.Navigation("Endereco");
                });

            modelBuilder.Entity("Estacionamento.Model.Models.ClienteVeiculo", b =>
                {
                    b.HasOne("Estacionamento.Model.Models.Cliente", "Cliente")
                        .WithMany("ClienteVeiculos")
                        .HasForeignKey("ClienteId")
                        .HasConstraintName("FK__ClienteVe__Clien__3F466844");

                    b.HasOne("Estacionamento.Model.Models.Veiculo", "Veiculo")
                        .WithMany("ClienteVeiculos")
                        .HasForeignKey("VeiculoId")
                        .HasConstraintName("FK__ClienteVe__Veicu__403A8C7D");

                    b.Navigation("Cliente");

                    b.Navigation("Veiculo");
                });

            modelBuilder.Entity("Estacionamento.Model.Models.Permanencia", b =>
                {
                    b.HasOne("Estacionamento.Model.Models.ClienteVeiculo", "ClienteVeiculo")
                        .WithMany("Permanencia")
                        .HasForeignKey("ClienteVeiculoId")
                        .HasConstraintName("FK__Permanenc__Clien__440B1D61");

                    b.Navigation("ClienteVeiculo");
                });

            modelBuilder.Entity("Estacionamento.Model.Models.Cliente", b =>
                {
                    b.Navigation("ClienteVeiculos");
                });

            modelBuilder.Entity("Estacionamento.Model.Models.ClienteVeiculo", b =>
                {
                    b.Navigation("Permanencia");
                });

            modelBuilder.Entity("Estacionamento.Model.Models.Endereco", b =>
                {
                    b.Navigation("Clientes");
                });

            modelBuilder.Entity("Estacionamento.Model.Models.Veiculo", b =>
                {
                    b.Navigation("ClienteVeiculos");
                });
#pragma warning restore 612, 618
        }
    }
}
