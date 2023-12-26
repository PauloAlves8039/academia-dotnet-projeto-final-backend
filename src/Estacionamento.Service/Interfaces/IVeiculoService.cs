using Estacionamento.Service.Dtos;

namespace Estacionamento.Service.Interfaces
{
    public interface IVeiculoService
    {
        Task<IEnumerable<VeiculoDto>> PesquisarListaDeVeiculos();

        Task<VeiculoDto> PesquisarVeiculoPorCodigo(int codigo);

        Task<VeiculoDto> AdicionarVeiculo(VeiculoDto veiculoDto);

        Task<VeiculoDto> AtualizarVeiculo(VeiculoDto veiculoDto);

        Task RemoverVeiculo(int codigo);
    }
}
