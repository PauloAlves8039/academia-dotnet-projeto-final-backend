using Estacionamento.Data.Context;
using Estacionamento.Data.Interfaces;
using Estacionamento.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Estacionamento.Data.Repositories
{
    public class EnderecoRepository : BaseRepository<Endereco>, IEnderecoRepository
    {
        private readonly ILogger<EnderecoRepository> _logger;
        private string _errorMessage = "";

        public EnderecoRepository(EstacionamentoContext contexto, ILogger<EnderecoRepository> logger) : base(contexto)
        {
            _logger = logger;
        }

        public override async Task<IEnumerable<Endereco>> GetAll()
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

        public override async Task<Endereco> GetById(int id)
        {
            try
            {
                return await _contexto.Enderecos.FirstOrDefaultAsync(e => e.CodigoEndereco == id);
            }
            catch (Exception ex)
            {
                _errorMessage = $"Erro ao buscar endereço por código: {ex.Message}";
                _logger.LogError(ex, _errorMessage);
                throw;
            }
        }

        public override async Task<Endereco> Add(Endereco entity)
        {
            try
            {
                _contexto.Enderecos.Add(entity);
                await _contexto.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                _errorMessage = $"Erro ao adicionar endereço: {ex.Message}";
                _logger.LogError(ex, _errorMessage);
                throw;
            }
        }

        public override async Task<Endereco> Update(Endereco entity)
        {
            try
            {
                var enderecoExistente = await _contexto.Enderecos
                    .FirstOrDefaultAsync(e => e.CodigoEndereco == entity.CodigoEndereco);

                if (enderecoExistente == null)
                {
                    throw new Exception($"Endereço com código {entity.CodigoEndereco} não encontrado");
                }

                _contexto.Entry(enderecoExistente).CurrentValues.SetValues(entity);

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

        public override async Task Delete(int id)
        {
            try
            {
                var endereco = await _contexto.Enderecos.FindAsync(id);

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
