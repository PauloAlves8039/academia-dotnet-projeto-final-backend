IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Enderecos] (
    [CodigoEndereco] int NOT NULL IDENTITY,
    [Logradouro] varchar(100) NOT NULL,
    [Numero] varchar(10) NOT NULL,
    [Complemento] varchar(150) NULL,
    [Bairro] varchar(100) NOT NULL,
    [Cidade] varchar(100) NOT NULL,
    [Estado] varchar(100) NOT NULL,
    [Cep] varchar(10) NOT NULL,
    CONSTRAINT [PK__Endereco__ECFD9712B78FD219] PRIMARY KEY ([CodigoEndereco])
);
GO

CREATE TABLE [Veiculos] (
    [CodigoVeiculo] int NOT NULL IDENTITY,
    [Tipo] varchar(20) NULL,
    [Marca] varchar(50) NULL,
    [Modelo] varchar(50) NULL,
    [Cor] varchar(50) NOT NULL,
    [Ano] int NOT NULL,
    [Observacoes] varchar(150) NULL,
    CONSTRAINT [PK__Veiculos__39A965987907ACCA] PRIMARY KEY ([CodigoVeiculo])
);
GO

CREATE TABLE [Clientes] (
    [CodigoCliente] int NOT NULL IDENTITY,
    [Nome] varchar(100) NOT NULL,
    [DataNascimento] date NOT NULL,
    [Cpf] varchar(20) NOT NULL,
    [Telefone] varchar(20) NOT NULL,
    [Email] varchar(100) NOT NULL,
    [CodigoEndereco] int NULL,
    CONSTRAINT [PK__Clientes__E0DD7E71544D746E] PRIMARY KEY ([CodigoCliente]),
    CONSTRAINT [FK__Clientes__Codigo__398D8EEE] FOREIGN KEY ([CodigoEndereco]) REFERENCES [Enderecos] ([CodigoEndereco]) ON DELETE CASCADE
);
GO

CREATE TABLE [ClienteVeiculos] (
    [CodigoClienteVeiculo] int NOT NULL IDENTITY,
    [ClienteId] int NULL,
    [VeiculoId] int NULL,
    CONSTRAINT [PK__ClienteV__67D54C7981DF8910] PRIMARY KEY ([CodigoClienteVeiculo]),
    CONSTRAINT [FK__ClienteVe__Clien__3F466844] FOREIGN KEY ([ClienteId]) REFERENCES [Clientes] ([CodigoCliente]),
    CONSTRAINT [FK__ClienteVe__Veicu__403A8C7D] FOREIGN KEY ([VeiculoId]) REFERENCES [Veiculos] ([CodigoVeiculo])
);
GO

CREATE TABLE [Permanencias] (
    [CodigoPermanencia] int NOT NULL IDENTITY,
    [ClienteVeiculoId] int NULL,
    [Placa] varchar(10) NOT NULL,
    [DataEntrada] datetime NULL,
    [DataSaida] datetime NULL,
    [TaxaPorHora] decimal(10,2) NOT NULL,
    [ValorTotal] decimal(10,2) NULL,
    [EstadoPermanencia] varchar(20) NULL,
    CONSTRAINT [PK__Permanen__089416FDE4EF7B8B] PRIMARY KEY ([CodigoPermanencia]),
    CONSTRAINT [FK__Permanenc__Clien__440B1D61] FOREIGN KEY ([ClienteVeiculoId]) REFERENCES [ClienteVeiculos] ([CodigoClienteVeiculo])
);
GO

CREATE INDEX [IX_Clientes_CodigoEndereco] ON [Clientes] ([CodigoEndereco]);
GO

CREATE UNIQUE INDEX [UQ__Clientes__C1FF93099270D3CD] ON [Clientes] ([Cpf]);
GO

CREATE INDEX [IX_ClienteVeiculos_ClienteId] ON [ClienteVeiculos] ([ClienteId]);
GO

CREATE INDEX [IX_ClienteVeiculos_VeiculoId] ON [ClienteVeiculos] ([VeiculoId]);
GO

CREATE INDEX [IX_Permanencias_ClienteVeiculoId] ON [Permanencias] ([ClienteVeiculoId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231228143246_Initial', N'6.0.25');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [RoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(max) NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_RoleClaims] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Roles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(max) NULL,
    [NormalizedName] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [UserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(max) NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_UserClaims] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [UserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(max) NULL,
    CONSTRAINT [PK_UserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey])
);
GO

CREATE TABLE [UserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_UserRoles] PRIMARY KEY ([UserId], [RoleId])
);
GO

CREATE TABLE [Users] (
    [Id] nvarchar(450) NOT NULL,
    [UserName] nvarchar(max) NULL,
    [NormalizedUserName] nvarchar(max) NULL,
    [Email] nvarchar(max) NULL,
    [NormalizedEmail] nvarchar(max) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [UserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_UserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240102010554_AddIdentity', N'6.0.25');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [ClienteVeiculos] DROP CONSTRAINT [FK__ClienteVe__Clien__3F466844];
GO

ALTER TABLE [ClienteVeiculos] DROP CONSTRAINT [FK__ClienteVe__Veicu__403A8C7D];
GO

ALTER TABLE [ClienteVeiculos] ADD CONSTRAINT [FK__ClienteVe__Clien__3F466844] FOREIGN KEY ([ClienteId]) REFERENCES [Clientes] ([CodigoCliente]) ON DELETE CASCADE;
GO

ALTER TABLE [ClienteVeiculos] ADD CONSTRAINT [FK__ClienteVe__Veicu__403A8C7D] FOREIGN KEY ([VeiculoId]) REFERENCES [Veiculos] ([CodigoVeiculo]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240107152313_AddRemoveEntities', N'6.0.25');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Clientes] DROP CONSTRAINT [FK__Clientes__Codigo__398D8EEE];
GO

ALTER TABLE [ClienteVeiculos] DROP CONSTRAINT [FK__ClienteVe__Clien__3F466844];
GO

ALTER TABLE [ClienteVeiculos] DROP CONSTRAINT [FK__ClienteVe__Veicu__403A8C7D];
GO

ALTER TABLE [Permanencias] DROP CONSTRAINT [FK__Permanenc__Clien__440B1D61];
GO

ALTER TABLE [Clientes] ADD CONSTRAINT [FK__Clientes__Codigo__398D8EEE] FOREIGN KEY ([CodigoEndereco]) REFERENCES [Enderecos] ([CodigoEndereco]);
GO

ALTER TABLE [ClienteVeiculos] ADD CONSTRAINT [FK__ClienteVe__Clien__3F466844] FOREIGN KEY ([ClienteId]) REFERENCES [Clientes] ([CodigoCliente]);
GO

ALTER TABLE [ClienteVeiculos] ADD CONSTRAINT [FK__ClienteVe__Veicu__403A8C7D] FOREIGN KEY ([VeiculoId]) REFERENCES [Veiculos] ([CodigoVeiculo]);
GO

ALTER TABLE [Permanencias] ADD CONSTRAINT [FK__Permanenc__Clien__440B1D61] FOREIGN KEY ([ClienteVeiculoId]) REFERENCES [ClienteVeiculos] ([CodigoClienteVeiculo]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240108230604_RemoveDeleteCascade', N'6.0.25');
GO

COMMIT;
GO

