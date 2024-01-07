using Estacionamento.Data.Context;
using Estacionamento.Data.Interfaces;
using Estacionamento.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Estacionamento.Data.Repositories
{
    public class ClienteVeiculoRepository : BaseRepository<ClienteVeiculo>, IClienteVeiculoRepository
    {
        private readonly ILogger<ClienteVeiculoRepository> _logger;
        private string _errorMessage = "";

        public ClienteVeiculoRepository(EstacionamentoContext contexto, ILogger<ClienteVeiculoRepository> logger) : base(contexto)
        {
            _logger = logger;
        }

        public override async Task<IEnumerable<ClienteVeiculo>> GetAll()
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

        public override async Task<ClienteVeiculo> GetById(int id)
        {
            try
            {
                return await _contexto.ClienteVeiculos.FirstOrDefaultAsync(cv => cv.CodigoClienteVeiculo == id);
            }
            catch (Exception ex)
            {
                _errorMessage = $"Erro ao buscar cliente e veículo por código: {ex.Message}";
                _logger.LogError(ex, _errorMessage);
                throw;
            }
        }

        public override async Task<ClienteVeiculo> Add(ClienteVeiculo entity)
        {
            try
            {
                _contexto.ClienteVeiculos.Add(entity);
                await _contexto.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                _errorMessage = $"Erro ao cadastrar cliente e veículo: {ex.Message}";
                _logger.LogError(ex, _errorMessage);
                throw;
            }
        }

        public override async Task<ClienteVeiculo> Update(ClienteVeiculo entity)
        {
            try
            {
                var clienteVeiculoExistente = await _contexto.ClienteVeiculos
                    .FirstOrDefaultAsync(cv => cv.CodigoClienteVeiculo == entity.CodigoClienteVeiculo);

                if (clienteVeiculoExistente == null)
                {
                    throw new Exception($"Cliente e veículo associados com código {entity.CodigoClienteVeiculo} não encontrado");
                }

                _contexto.Entry(clienteVeiculoExistente).CurrentValues.SetValues(entity);

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

        public override async Task Delete(int id)
        {
            try
            {
                var clienteVeiculo = await _contexto.ClienteVeiculos.FindAsync(id);

                if (clienteVeiculo != null)
                {
                    _contexto.ClienteVeiculos.Remove(clienteVeiculo);
                    await _contexto.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _errorMessage = $"Erro ao excluir associção entre cliente e veículo: {ex.Message}";
                _logger.LogError(ex, _errorMessage);
                throw;
            }
        }
    }
}
