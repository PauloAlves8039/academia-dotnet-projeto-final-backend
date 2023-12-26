using Estacionamento.Data.Context;
using Estacionamento.Data.Interfaces;
using Estacionamento.Model.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Estacionamento.Data.Repositories
{
    public class VeiculoRepository : IVeiculoRepository
    {
        private readonly EstacionamentoContext _contexto;
        private readonly ILogger<VeiculoRepository> _logger;
        private string _errorMessage = "";

        public VeiculoRepository(EstacionamentoContext contexto, ILogger<VeiculoRepository> logger)
        {
            _contexto = contexto;
            _logger = logger;
        }

        public async Task<IEnumerable<Veiculo>> ObterListaDeVeiculos()
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

        public async Task<Veiculo> ObterVeiculoPorCodigo(int codigo)
        {
            try
            {
                return await _contexto.Veiculos.FirstOrDefaultAsync(v => v.CodigoVeiculo == codigo);
            }
            catch (Exception ex)
            {
                _errorMessage = $"Erro ao buscar veículo por código: {ex.Message}";
                _logger.LogError(ex, _errorMessage);
                throw;
            }
        }

        public async Task<Veiculo> CadastrarVeiculo(Veiculo veiculo)
        {
            try
            {
                _contexto.Veiculos.Add(veiculo);
                await _contexto.SaveChangesAsync();
                return veiculo;
            }
            catch (Exception ex)
            {
                _errorMessage = $"Erro ao cadastrar veículo: {ex.Message}";
                _logger.LogError(ex, _errorMessage);
                throw;
            }
        }

        public async Task<Veiculo> AlterarVeiculo(Veiculo veiculo)
        {
            var veiculoExistente = await _contexto.Veiculos
                    .FirstOrDefaultAsync(v => v.CodigoVeiculo == veiculo.CodigoVeiculo);

            if (veiculoExistente == null)
            {
                throw new Exception($"Veículo com código {veiculo.CodigoVeiculo} não encontrado");
            }

            _contexto.Entry(veiculoExistente).CurrentValues.SetValues(veiculo);

            await _contexto.SaveChangesAsync();

            return veiculoExistente;
        }

        public async Task ExcluirVeiculo(int codigo)
        {
            try
            {
                var veiculo = await _contexto.Veiculos.FindAsync(codigo);

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
