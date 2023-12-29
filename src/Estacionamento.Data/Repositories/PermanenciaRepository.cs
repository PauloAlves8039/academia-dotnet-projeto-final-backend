using Estacionamento.Data.Context;
using Estacionamento.Data.Interfaces;
using Estacionamento.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Estacionamento.Data.Repositories
{
    public class PermanenciaRepository : IPermanenciaRepository
    {
        private readonly EstacionamentoContext _contexto;
        private readonly ILogger<PermanenciaRepository> _logger;
        private string _errorMessage = "";

        public PermanenciaRepository(EstacionamentoContext contexto, ILogger<PermanenciaRepository> logger)
        {
            _contexto = contexto;
            _logger = logger;
        }

        public async Task<IEnumerable<Permanencia>> ObterListaDePermanencias()
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
                _errorMessage = $"Erro ao pesquisar permanência por itens: {ex.Message}";
                _logger.LogError(ex, _errorMessage);
                throw;
            }
        }

        public async Task<Permanencia> ObterPermanenciaPorCodigo(int codigo)
        {
            try
            {
                var permanencia = await _contexto.Permanencias
                    .Include(p => p.ClienteVeiculo)
                        .ThenInclude(cv => cv.Cliente)
                    .Include(p => p.ClienteVeiculo)
                        .ThenInclude(cv => cv.Veiculo)
                    .Where(p => p.CodigoPermanencia == codigo)
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

        public async Task<Permanencia> CadastrarPermanencia(Permanencia permanencia)
        {
            try
            {
                _contexto.Permanencias.Add(permanencia);

                await _contexto.SaveChangesAsync();

                return permanencia;
            }
            catch (Exception ex)
            {
                _errorMessage = $"Erro ao adicionar permanência: {ex.Message}";
                _logger.LogError(ex, _errorMessage);
                throw;
            }
        }


        public async Task<Permanencia> AlterarPermanencia(Permanencia permanencia)
        {
            try
            {
                var permanenciaExistente = await _contexto.Permanencias
                    .FirstOrDefaultAsync(p => p.CodigoPermanencia == permanencia.CodigoPermanencia);

                if (permanenciaExistente != null)
                {
                    permanenciaExistente.ClienteVeiculo = permanencia.ClienteVeiculo;
                    permanenciaExistente.Placa = permanencia.Placa;
                    permanenciaExistente.DataEntrada = permanencia.DataEntrada;
                    permanenciaExistente.DataSaida = permanencia.DataSaida;
                    permanenciaExistente.TaxaPorHora = permanencia.TaxaPorHora;
                    permanenciaExistente.ValorTotal = permanencia.ValorTotal;
                    permanenciaExistente.EstadoPermanencia = permanencia.EstadoPermanencia;

                    await _contexto.SaveChangesAsync();
                }

                return permanenciaExistente;
            }
            catch (Exception ex)
            {
                _errorMessage = $"Erro ao alterar permanência: {ex.Message}";
                _logger.LogError(ex, _errorMessage);
                throw;
            }
        }

        public async Task ExcluirPermanencia(int codigo)
        {
            try
            {
                var permanencia = await _contexto.Permanencias.FindAsync(codigo);

                if (permanencia != null)
                {
                    _contexto.Permanencias.Remove(permanencia);
                    await _contexto.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _errorMessage = $"Erro ao excluir Permanência: {ex.Message}";
                _logger.LogError(ex, _errorMessage);
                throw;
            }
        }
    }
}
