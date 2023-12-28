using Estacionamento.Data.Context;
using Estacionamento.Data.Interfaces;
using Estacionamento.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Estacionamento.Data.Repositories
{
    public class ClienteVeiculoRepository : IClienteVeiculoRepository
    {
        private readonly EstacionamentoContext _contexto;
        private readonly ILogger<ClienteVeiculoRepository> _logger;
        private string _errorMessage = "";

        public ClienteVeiculoRepository(EstacionamentoContext contexto, ILogger<ClienteVeiculoRepository> logger)
        {
            _contexto = contexto;
            _logger = logger;
        }

        public async Task<IEnumerable<ClienteVeiculo>> ObterItensClienteVeiculo()
        {
            try
            {
                var informacoes = await _contexto.ClienteVeiculos
                    .Include(cv => cv.Cliente)
                    .Include(cv => cv.Veiculo)
                    .ToListAsync();

                foreach (var info in informacoes)
                {
                    var nomeCliente = info.Cliente.Nome;
                    var modeloVeiculo = info.Veiculo.Modelo;
                }

                return informacoes;
            }
            catch (Exception ex)
            {
                _errorMessage = $"Erro ao buscar informações de clientes e veículos: {ex.Message}";
                _logger.LogError(ex, _errorMessage);
                throw;
            }
        }

        public async Task<ClienteVeiculo> ObterClienteVeiculoPorCodigo(int codigo)
        {
            try
            {
                return await _contexto.ClienteVeiculos.FirstOrDefaultAsync(cv => cv.CodigoClienteVeiculo == codigo);
            }
            catch (Exception ex)
            {
                _errorMessage = $"Erro ao buscar cliente e veículo por código: {ex.Message}";
                _logger.LogError(ex, _errorMessage);
                throw;
            }
        }

        public async Task<ClienteVeiculo> CadastrarClienteVeiculo(ClienteVeiculo clienteVeiculo)
        {
            try
            {
                _contexto.ClienteVeiculos.Add(clienteVeiculo);
                await _contexto.SaveChangesAsync();
                return clienteVeiculo;
            }
            catch (Exception ex)
            {
                _errorMessage = $"Erro ao cadastrar cliente e veículo: {ex.Message}";
                _logger.LogError(ex, _errorMessage);
                throw;
            }
        }

        public async Task<ClienteVeiculo> AlterarClienteVeiculo(ClienteVeiculo clienteVeiculo)
        {
            try
            {
                var clienteVeiculoExistente = await _contexto.ClienteVeiculos
                    .FirstOrDefaultAsync(cv => cv.CodigoClienteVeiculo == clienteVeiculo.CodigoClienteVeiculo);

                if (clienteVeiculoExistente == null)
                {
                    throw new Exception($"Cliente e veículo associados com código {clienteVeiculo.CodigoClienteVeiculo} não encontrado");
                }

                _contexto.Entry(clienteVeiculoExistente).CurrentValues.SetValues(clienteVeiculo);

                await _contexto.SaveChangesAsync();

                return clienteVeiculoExistente;
            }
            catch (Exception ex)
            {
                _errorMessage = $"Erro ao atualizar cliente e veículo associados: {ex.Message}";
                _logger.LogError(ex, _errorMessage);
                throw;
            }
        }

        public async Task ExcluirClienteVeiculo(int codigo)
        {
            try
            {
                var clienteVeiculo = await _contexto.ClienteVeiculos.FindAsync(codigo);

                if (clienteVeiculo != null)
                {
                    _contexto.ClienteVeiculos.Remove(clienteVeiculo);
                    await _contexto.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _errorMessage = $"Erro ao excluir associção entre cliente e veículo associados: {ex.Message}";
                _logger.LogError(ex, _errorMessage);
                throw;
            }
        }
    }
}
