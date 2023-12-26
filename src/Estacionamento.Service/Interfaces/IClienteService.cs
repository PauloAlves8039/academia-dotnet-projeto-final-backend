using Estacionamento.Service.Dtos;

namespace Estacionamento.Service.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteDto>> PesquisarListaDeClientes();

        Task<ClienteDto> PesquisarClientePorCodigo(int codigo);

        Task<ClienteDto> AdicionarCliente(ClienteDto clienteDto);

        Task<ClienteDto> AtualizarCliente(ClienteDto clienteDto);

        Task RemoverCliente(int codigo);
    }
}
