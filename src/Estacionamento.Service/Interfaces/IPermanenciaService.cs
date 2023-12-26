using Estacionamento.Service.Dtos;

namespace Estacionamento.Service.Interfaces
{
    public interface IPermanenciaService
    {
        Task<IEnumerable<PermanenciaDto>> PesquisarListaDePermanencias();

        Task<PermanenciaDto> PesquisarPermanenciaPorCodigo(int codigo);

        Task<PermanenciaDto> AdicionarPermanencia(PermanenciaDto permanenciaDto);

        Task<PermanenciaDto> AtualizarPermanencia(PermanenciaDto permanenciaDto);
    }
}
