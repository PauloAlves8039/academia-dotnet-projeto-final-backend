using Estacionamento.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Estacionamento.Data.Context
{
    public partial class EstacionamentoContext : DbContext
    {
        public EstacionamentoContext() { }

        public EstacionamentoContext(DbContextOptions<EstacionamentoContext> options) : base(options) { }

        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<ClienteVeiculo> ClienteVeiculos { get; set; }
        public virtual DbSet<Endereco> Enderecos { get; set; }
        public virtual DbSet<Permanencia> Permanencias { get; set; }
        public virtual DbSet<Veiculo> Veiculos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=Estacionamento;Persist Security Info=True;User ID=sa;Password=*********");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.CodigoCliente)
                    .HasName("PK__Clientes__E0DD7E71544D746E");

                entity.HasIndex(e => e.Cpf, "UQ__Clientes__C1FF93099270D3CD")
                    .IsUnique();

                entity.Property(e => e.Cpf)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DataNascimento).HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Telefone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Endereco)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.CodigoEndereco)
                    .HasConstraintName("FK__Clientes__Codigo__398D8EEE");
            });

            modelBuilder.Entity<ClienteVeiculo>(entity =>
            {
                entity.HasKey(e => e.CodigoClienteVeiculo)
                    .HasName("PK__ClienteV__67D54C7981DF8910");

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.ClienteVeiculos)
                    .HasForeignKey(d => d.ClienteId)
                    .HasConstraintName("FK__ClienteVe__Clien__3F466844");

                entity.HasOne(d => d.Veiculo)
                    .WithMany(p => p.ClienteVeiculos)
                    .HasForeignKey(d => d.VeiculoId)
                    .HasConstraintName("FK__ClienteVe__Veicu__403A8C7D");
            });

            modelBuilder.Entity<Endereco>(entity =>
            {
                entity.HasKey(e => e.CodigoEndereco)
                    .HasName("PK__Endereco__ECFD9712B78FD219");

                entity.Property(e => e.Bairro)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Cep)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Cidade)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Complemento)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Logradouro)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Numero)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Permanencia>(entity =>
            {
                entity.HasKey(e => e.CodigoPermanencia)
                    .HasName("PK__Permanen__089416FDE4EF7B8B");

                entity.Property(e => e.DataEntrada).HasColumnType("datetime");

                entity.Property(e => e.DataSaida).HasColumnType("datetime");

                entity.Property(e => e.EstadoPermanencia)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Placa)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TaxaPorHora).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.ValorTotal).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.ClienteVeiculo)
                    .WithMany(p => p.Permanencia)
                    .HasForeignKey(d => d.ClienteVeiculoId)
                    .HasConstraintName("FK__Permanenc__Clien__440B1D61");
            });

            modelBuilder.Entity<Veiculo>(entity =>
            {
                entity.HasKey(e => e.CodigoVeiculo)
                    .HasName("PK__Veiculos__39A965987907ACCA");

                entity.Property(e => e.Cor)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Marca)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Modelo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Observacoes)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Tipo)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
