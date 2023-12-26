using Estacionamento.Model.Models;

namespace Estacionamento.Data.Interfaces
{
    public interface IVeiculoRepository
    {
        Task<IEnumerable<Veiculo>> ObterListaDeVeiculos();

        Task<Veiculo> ObterVeiculoPorCodigo(int codigo);

        Task<Veiculo> CadastrarVeiculo(Veiculo veiculo);

        Task<Veiculo> AlterarVeiculo(Veiculo veiculo);

        Task ExcluirVeiculo(int codigo);
    }
}
