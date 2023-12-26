using Estacionamento.Model.Models;

namespace Estacionamento.Data.Interfaces
{
    public interface IClienteVeiculoRepository
    {
        Task<IEnumerable<ClienteVeiculo>> ObterListaDeClienteVeiculo();

        Task<IEnumerable<ClienteVeiculo>> ObterItensClienteVeiculo();

        Task<ClienteVeiculo> ObterClienteVeiculoPorCodigo(int codigo);

        Task<ClienteVeiculo> CadastrarClienteVeiculo(ClienteVeiculo clienteVeiculo);
    }
}
