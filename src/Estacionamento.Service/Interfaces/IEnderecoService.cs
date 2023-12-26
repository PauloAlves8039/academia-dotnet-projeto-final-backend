using Estacionamento.Service.Dtos;

namespace Estacionamento.Service.Interfaces
{
    public interface IEnderecoService
    {
        Task<IEnumerable<EnderecoDto>> PesquisarListaDeEnderecos();

        Task<EnderecoDto> PesquisarEnderecoPorCodigo(int codigo);

        Task<EnderecoDto> AdicionarEndereco(EnderecoDto enderecoDto);

        Task<EnderecoDto> AltualizarEndereco(EnderecoDto enderecoDto);

        Task RemoverEndereco(int codigo);
    }
}
