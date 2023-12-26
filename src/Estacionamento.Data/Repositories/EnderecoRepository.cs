using Estacionamento.Data.Context;
using Estacionamento.Data.Interfaces;
using Estacionamento.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Estacionamento.Data.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly EstacionamentoContext _contexto;
        private readonly ILogger<EnderecoRepository> _logger;
        private string _errorMessage = "";

        public EnderecoRepository(EstacionamentoContext contexto, ILogger<EnderecoRepository> logger)
        {
            _contexto = contexto;
            _logger = logger;
        }

        public async Task<IEnumerable<Endereco>> ObterListaDeEnderecos()
        {
            try
            {
                return await _contexto.Enderecos.ToListAsync();
            }
            catch (Exception ex)
            {
                _errorMessage = $"Erro ao buscar lista de endereços: {ex.Message}";
                _logger.LogError(ex, _errorMessage);
                throw;
            }
        }

        public async Task<Endereco> ObterEnderecoPorCodigo(int codigo)
        {
            try
            {
                return await _contexto.Enderecos.FirstOrDefaultAsync(e => e.CodigoEndereco == codigo);
            }
            catch (Exception ex)
            {
                _errorMessage = $"Erro ao buscar endereço por código: {ex.Message}";
                _logger.LogError(ex, _errorMessage);
                throw;
            }
        }

        public async Task<Endereco> CadastrarEndereco(Endereco endereco)
        {
            try
            {
                _contexto.Enderecos.Add(endereco);
                await _contexto.SaveChangesAsync();
                return endereco;
            }
            catch (Exception ex)
            {
                _errorMessage = $"Erro ao cadastrar endereço: {ex.Message}";
                _logger.LogError(ex, _errorMessage);
                throw;
            }
        }

        public async Task<Endereco> AlterarEndereco(Endereco endereco)
        {
            try
            {
                var enderecoExistente = await _contexto.Enderecos
                    .FirstOrDefaultAsync(e => e.CodigoEndereco == endereco.CodigoEndereco);

                if (enderecoExistente == null)
                {
                    throw new Exception($"Endereço com código {endereco.CodigoEndereco} não encontrado");
                }

                _contexto.Entry(enderecoExistente).CurrentValues.SetValues(endereco);

                await _contexto.SaveChangesAsync();

                return enderecoExistente;
            }
            catch (Exception ex)
            {
                _errorMessage = $"Erro ao atualizar endereço: {ex.Message}";
                _logger.LogError(ex, _errorMessage);
                throw;
            }
        }

        public async Task ExcluirEndereco(int codigo)
        {
            try
            {
                var endereco = await _contexto.Enderecos.FindAsync(codigo);

                if (endereco != null)
                {
                    _contexto.Enderecos.Remove(endereco);
                    await _contexto.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _errorMessage = $"Erro ao excluir endereço: {ex.Message}";
                _logger.LogError(ex, _errorMessage);
                throw;
            }
        }
    }
}
