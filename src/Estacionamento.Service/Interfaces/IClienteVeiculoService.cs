using Estacionamento.Service.Dtos;

namespace Estacionamento.Service.Interfaces
{
    public interface IClienteVeiculoService
    {
        Task<IEnumerable<ClienteVeiculoDto>> PesquisarListaDeClienteMoto();

        Task<IEnumerable<ClienteVeiculoDto>> PesquisarItensClienteMoto();

        Task<ClienteVeiculoDto> PesquisarClienteMotoPorCodigo(int codigo);

        Task<ClienteVeiculoDto> AdicionarClienteMoto(ClienteVeiculoDto clienteVeiculoDto);
    }
}
