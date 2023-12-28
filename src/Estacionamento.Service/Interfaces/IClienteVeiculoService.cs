using Estacionamento.Service.Dtos;

namespace Estacionamento.Service.Interfaces
{
    public interface IClienteVeiculoService
    {
        Task<IEnumerable<ClienteVeiculoDto>> PesquisarItensClienteVeiculo();

        Task<ClienteVeiculoDto> PesquisarClienteVeiculoPorCodigo(int codigo);

        Task<ClienteVeiculoDto> AdicionarClienteVeiculo(ClienteVeiculoDto clienteVeiculoDto);

        Task<ClienteVeiculoDto> AtualizarClienteVeiculo(ClienteVeiculoDto clienteVeiculoDto);

        Task RemoverClienteVeiculo(int codigo);
    }
}
