using Estacionamento.Data.Context;
using Estacionamento.Data.Interfaces;
using Estacionamento.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Estacionamento.Data.Repositories
{
    public class PermanenciaRepository : BaseRepository<Permanencia>, IPermanenciaRepository
    {
        private readonly ILogger<PermanenciaRepository> _logger;
        private string _errorMessage = "";

        public PermanenciaRepository(EstacionamentoContext contexto, ILogger<PermanenciaRepository> logger) : base(contexto)
        {
            _logger = logger;
        }

        public override async Task<IEnumerable<Permanencia>> GetAll()
        {
            try
            {
                var permanencias = await _contexto.Permanencias
                    .Include(p => p.ClienteVeiculo)
                        .ThenInclude(cv => cv.Cliente)
                    .Include(p => p.ClienteVeiculo)
                        .ThenInclude(cv => cv.Veiculo)
                    .Select(p => new Permanencia
                    {
                        CodigoPermanencia = p.CodigoPermanencia,
                        ClienteVeiculoId = p.ClienteVeiculoId ?? 0,
                        Placa = p.Placa,
                        DataEntrada = p.DataEntrada,
                        DataSaida = p.DataSaida,
                        TaxaPorHora = p.TaxaPorHora,
                        ValorTotal = p.ValorTotal,
                        EstadoPermanencia = p.EstadoPermanencia,

                    }).ToListAsync();


                return permanencias ?? new List<Permanencia>();
            }
            catch (Exception ex)
            {
                _errorMessage = $"Erro ao pesquisar lista de permanências: {ex.Message}";
                _logger.LogError(ex, _errorMessage);
                throw;
            }
        }

        public override async Task<Permanencia> GetById(int id)
        {
            try
            {
                var permanencia = await _contexto.Permanencias
                    .Include(p => p.ClienteVeiculo)
                        .ThenInclude(cv => cv.Cliente)
                    .Include(p => p.ClienteVeiculo)
                        .ThenInclude(cv => cv.Veiculo)
                    .Where(p => p.CodigoPermanencia == id)
                    .Select(p => new Permanencia
                    {
                        CodigoPermanencia = p.CodigoPermanencia,
                        ClienteVeiculoId = p.ClienteVeiculoId ?? 0,
                        Placa = p.Placa,
                        DataEntrada = p.DataEntrada,
                        DataSaida = p.DataSaida,
                        TaxaPorHora = p.TaxaPorHora,
                        ValorTotal = p.ValorTotal,
                        EstadoPermanencia = p.EstadoPermanencia,
                    })
                    .FirstOrDefaultAsync();

                return permanencia;
            }
            catch (Exception ex)
            {
                _errorMessage = $"Erro ao obter permanência por código: {ex.Message}";
                _logger.LogError(ex, _errorMessage);
                throw;
            }
        }

        public override async Task<Permanencia> Add(Permanencia entity)
        {
            try
            {
                _contexto.Permanencias.Add(entity);

                await _contexto.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                _errorMessage = $"Erro ao adicionar permanência: {ex.Message}";
                _logger.LogError(ex, _errorMessage);
                throw;
            }
        }

        public override async Task<Permanencia> Update(Permanencia entity)
        {
            try
            {
                var permanenciaExistente = await _contexto.Permanencias
                    .FirstOrDefaultAsync(p => p.CodigoPermanencia == entity.CodigoPermanencia);

                if (permanenciaExistente != null)
                {
                    permanenciaExistente.ClienteVeiculo = entity.ClienteVeiculo;
                    permanenciaExistente.Placa = entity.Placa;
                    permanenciaExistente.DataEntrada = entity.DataEntrada;
                    permanenciaExistente.DataSaida = entity.DataSaida;
                    permanenciaExistente.TaxaPorHora = entity.TaxaPorHora;
                    permanenciaExistente.ValorTotal = entity.ValorTotal;
                    permanenciaExistente.EstadoPermanencia = entity.EstadoPermanencia;

                    await _contexto.SaveChangesAsync();
                }

                return permanenciaExistente;
            }
            catch (Exception ex)
            {
                _errorMessage = $"Erro ao alterar a permanência: {ex.Message}";
                _logger.LogError(ex, _errorMessage);
                throw;
            }
        }

        public override async Task Delete(int id)
        {
            try
            {
                var permanencia = await _contexto.Permanencias.FindAsync(id);

                if (permanencia != null)
                {
                    _contexto.Permanencias.Remove(permanencia);
                    await _contexto.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _errorMessage = $"Erro ao excluir permanência: {ex.Message}";
                _logger.LogError(ex, _errorMessage);
                throw;
            }
        }
    }
}
