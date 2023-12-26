using Estacionamento.Model.Models;

namespace Estacionamento.Data.Interfaces
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> ObterListaDeClientes();

        Task<Cliente> ObterClientePorCodigo(int codigo);

        Task<Cliente> CadastrarCliente(Cliente cliente);

        Task<Cliente> AlterarCliente(Cliente cliente);

        Task ExcluirCliente(int codigo);
    }
}
