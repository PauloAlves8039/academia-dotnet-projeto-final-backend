using Estacionamento.Data.Context;
using Estacionamento.Data.Interfaces;
using Estacionamento.Model.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Estacionamento.Data.Repositories
{
    public class VeiculoRepository : BaseRepository<Veiculo>, IVeiculoRepository
    {
        private readonly ILogger<VeiculoRepository> _logger;
        private string _errorMessage = "";

        public VeiculoRepository(EstacionamentoContext context, ILogger<VeiculoRepository> logger) : base(context)
        {
            _logger = logger;
        }

        public override async Task<IEnumerable<Veiculo>> GetAll()
        {
            try
            {
                return await _contexto.Veiculos.ToListAsync();
            }
            catch (SqlException ex)
            {
                _errorMessage = $"Erro ao buscar lista de veículos: {ex.Message}";
                _logger.LogError(ex, _errorMessage);
                throw;
            }
        }

        public override async Task<Veiculo> GetById(int id)
        {
            try
            {
                return await _contexto.Veiculos.FirstOrDefaultAsync(v => v.CodigoVeiculo == id);
            }
            catch (Exception ex)
            {
                _errorMessage = $"Erro ao buscar veículo por código: {ex.Message}";
                _logger.LogError(ex, _errorMessage);
                throw;
            }
        }

        public override async Task<Veiculo> Add(Veiculo entity)
        {
            try
            {
                _contexto.Veiculos.Add(entity);
                await _contexto.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                _errorMessage = $"Erro ao cadastrar veículo: {ex.Message}";
                _logger.LogError(ex, _errorMessage);
                throw;
            }
        }

        public override async Task<Veiculo> Update(Veiculo entity)
        {
            var veiculoExistente = await _contexto.Veiculos
                    .FirstOrDefaultAsync(v => v.CodigoVeiculo == entity.CodigoVeiculo);

            if (veiculoExistente == null)
            {
                throw new Exception($"Veículo com código {entity.CodigoVeiculo} não encontrado");
            }

            _contexto.Entry(veiculoExistente).CurrentValues.SetValues(entity);

            await _contexto.SaveChangesAsync();

            return veiculoExistente;
        }

        public override async Task Delete(int id)
        {
            try
            {
                var veiculo = await _contexto.Veiculos.FindAsync(id);

                if (veiculo != null)
                {
                    _contexto.Veiculos.Remove(veiculo);
                    await _contexto.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _errorMessage = $"Erro ao excluir veículo: {ex.Message}";
                _logger.LogError(ex, _errorMessage);
                throw;
            }
        }
    }
}
