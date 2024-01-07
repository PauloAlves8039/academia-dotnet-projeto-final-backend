using Estacionamento.Data.Context;
using Estacionamento.Data.Interfaces;
using Estacionamento.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Estacionamento.Data.Repositories
{
    public class ClienteRepository : BaseRepository<Cliente>, IClienteRepository
    {
        private readonly ILogger<ClienteRepository> _logger;
        private string _errorMessage = "";

        public ClienteRepository(EstacionamentoContext context, ILogger<ClienteRepository> logger) : base(context)
        {
            _logger = logger;
        }

        public override async Task<IEnumerable<Cliente>> GetAll()
        {
            try
            {
                return await _contexto.Clientes.ToListAsync();
            }
            catch (Exception ex)
            {
                _errorMessage = $"Erro ao buscar lista de clientes: {ex.Message}";
                _logger.LogError(ex, _errorMessage);
                throw;
            }
        }

        public override async Task<Cliente> GetById(int id)
        {
            try
            {
                return await _contexto.Clientes.FirstOrDefaultAsync(c => c.CodigoCliente == id);
            }
            catch (Exception ex)
            {
                _errorMessage = $"Erro ao buscar cliente por código: {ex.Message}";
                _logger.LogError(ex, _errorMessage);
                throw;
            }
        }

        public override async Task<Cliente> Add(Cliente entity)
        {
            try
            {
                _contexto.Clientes.Add(entity);
                await _contexto.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                _errorMessage = $"Erro ao adicionar cliente: {ex.Message}";
                _logger.LogError(ex, _errorMessage);
                throw;
            }
        }

        public override async Task<Cliente> Update(Cliente entity)
        {
            try
            {
                var clienteExistente = await _contexto.Clientes
                    .FirstOrDefaultAsync(c => c.CodigoCliente == entity.CodigoCliente);

                if (clienteExistente == null)
                {
                    throw new Exception($"Cliente com código {entity.CodigoCliente} não encontrado");
                }

                _contexto.Entry(clienteExistente).CurrentValues.SetValues(entity);
                await _contexto.SaveChangesAsync();

                return clienteExistente;
            }
            catch (Exception ex)
            {
                _errorMessage = $"Erro ao atualizar cliente: {ex.Message}";
                _logger.LogError(ex, _errorMessage);
                throw;
            }
        }

    }
}
