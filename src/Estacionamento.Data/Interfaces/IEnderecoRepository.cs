using Estacionamento.Model.Models;

namespace Estacionamento.Data.Interfaces
{
    public interface IEnderecoRepository
    {
        Task<IEnumerable<Endereco>> ObterListaDeEnderecos();

        Task<Endereco> ObterEnderecoPorCodigo(int codigo);

        Task<Endereco> CadastrarEndereco(Endereco endereco);

        Task<Endereco> AlterarEndereco(Endereco endereco);

        Task ExcluirEndereco(int codigo);
    }
}
