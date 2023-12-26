using Estacionamento.Model.Models;

namespace Estacionamento.Data.Interfaces
{
    public interface IPermanenciaRepository
    {
        Task<IEnumerable<Permanencia>> ObterListaDePermanencias();

        Task<Permanencia> ObterPermanenciaPorCodigo(int codigo);

        Task<Permanencia> CadastrarPermanencia(Permanencia permanencia);

        Task<Permanencia> AlterarPermanencia(Permanencia permanencia);
    }
}
