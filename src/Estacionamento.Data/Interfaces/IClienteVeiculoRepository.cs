﻿using Estacionamento.Model.Models;

namespace Estacionamento.Data.Interfaces
{
    public interface IClienteVeiculoRepository
    {
        Task<IEnumerable<ClienteVeiculo>> ObterItensClienteVeiculo();

        Task<ClienteVeiculo> ObterClienteVeiculoPorCodigo(int codigo);

        Task<ClienteVeiculo> CadastrarClienteVeiculo(ClienteVeiculo clienteVeiculo);

        Task<ClienteVeiculo> AlterarClienteVeiculo(ClienteVeiculo clienteVeiculo);

        Task ExcluirClienteVeiculo(int codigo);
    }
}
