CREATE DATABASE Estacionamento;

USE Estacionamento;

CREATE TABLE Enderecos (
    CodigoEndereco INT PRIMARY KEY IDENTITY(1,1),
    Logradouro VARCHAR(100) NOT NULL,
	Numero VARCHAR(10) NOT NULL,
	Complemento VARCHAR(150) NULL,
	Bairro VARCHAR(100) NOT NULL,
	Cidade VARCHAR(100) NOT NULL,
    Estado VARCHAR(100) NOT NULL,
    Cep VARCHAR(10) NOT NULL
);

CREATE TABLE Clientes (
    CodigoCliente INT PRIMARY KEY IDENTITY(1,1),
    Nome VARCHAR(100) NOT NULL,
    DataNascimento DATE NOT NULL,
    Cpf VARCHAR(20) UNIQUE NOT NULL,
    Telefone VARCHAR(20) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    CodigoEndereco INT,
    FOREIGN KEY (CodigoEndereco) REFERENCES Enderecos(Codigoendereco)
);

CREATE TABLE Veiculos (
    CodigoVeiculo INT PRIMARY KEY IDENTITY(1,1),
	Tipo VARCHAR(20) CHECK (Tipo IN ('Moto', 'Carro')),
    Marca VARCHAR(50),
    Modelo VARCHAR(50),
    Cor VARCHAR(50) NOT NULL,
    Ano INT NOT NULL,
    Observacoes VARCHAR(150) NULL
);

CREATE TABLE ClienteVeiculos (
    CodigoClienteVeiculo INT PRIMARY KEY IDENTITY(1,1),
    ClienteId INT,
    VeiculoId INT,
    FOREIGN KEY (ClienteId) REFERENCES Clientes(CodigoCliente),
    FOREIGN KEY (VeiculoId) REFERENCES Veiculos(CodigoVeiculo)
);

CREATE TABLE Permanencias (
    CodigoPermanencia INT PRIMARY KEY IDENTITY(1,1),
    ClienteVeiculoId INT,
    Placa VARCHAR(10) NOT NULL,
    DataEntrada DATETIME NULL,
    DataSaida DATETIME NULL,
    TaxaPorHora DECIMAL(10, 2) NOT NULL,
    ValorTotal DECIMAL(10, 2) NULL, 
    EstadoPermanencia VARCHAR(20) CHECK (EstadoPermanencia IN ('Estacionado', 'Retirado')),
    FOREIGN KEY (ClienteVeiculoId) REFERENCES ClienteVeiculos(CodigoClienteVeiculo)
);

SELECT * FROM Enderecos;

SELECT * FROM Clientes;

SELECT * FROM Veiculos;

SELECT * FROM ClienteVeiculos;

SELECT * FROM Permanencias;

SELECT
    P.CodigoPermanencia AS 'C�digo da Perman�ncia',
    C.Nome AS 'Cliente',
    p.Placa AS 'Placa da Moto',
    P.DataEntrada AS 'Data de Entada',
    P.DataSaida AS 'Data de Sa�da',
    P.TaxaPorHora AS 'Pre�o por Hora',
	p.ValorTotal AS 'Valor Total',
    P.EstadoPermanencia AS 'Estado'
FROM
    Permanencias P
JOIN
    ClienteVeiculos CM ON P.ClienteVeiculoId = CM.CodigoClienteVeiculo
JOIN
    Clientes C ON CM.ClienteId = C.CodigoCliente
JOIN
    Veiculos M ON CM.VeiculoId = M.CodigoVeiculo;
