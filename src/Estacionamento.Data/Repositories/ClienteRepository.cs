using Estacionamento.Data.Context;
using Estacionamento.Data.Interfaces;
using Estacionamento.Model.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Estacionamento.Data.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly EstacionamentoContext _contexto;
        private readonly ILogger<ClienteRepository> _logger;
        private string _errorMessage = "";

        public ClienteRepository(EstacionamentoContext contexto, ILogger<ClienteRepository> logger)
        {
            _contexto = contexto;
            _logger = logger;
        }

        public async Task<IEnumerable<Cliente>> ObterListaDeClientes()
        {
            try
            {
                return await _contexto.Clientes.ToListAsync();
            }
            catch (SqlException ex)
            {
                _errorMessage = $"Erro ao buscar lista de clientes: {ex.Message}";
                _logger.LogError(ex, _errorMessage);
                throw;
            }
        }

        public async Task<Cliente> ObterClientePorCodigo(int codigo)
        {
            try
            {
                return await _contexto.Clientes.FirstOrDefaultAsync(c => c.CodigoCliente == codigo);
            }
            catch (Exception ex)
            {
                _errorMessage = $"Erro ao buscar cliente por código: {ex.Message}";
                _logger.LogError(ex, _errorMessage);
                throw;
            }
        }

        public async Task<Cliente> CadastrarCliente(Cliente cliente)
        {
            try
            {
                _contexto.Clientes.Add(cliente);
                await _contexto.SaveChangesAsync();
                return cliente;
            }
            catch (Exception ex)
            {
                _errorMessage = $"Erro ao cadastrar cliente: {ex.Message}";
                _logger.LogError(ex, _errorMessage);
                throw;
            }
        }

        public async Task<Cliente> AlterarCliente(Cliente cliente)
        {
            try
            {
                var clienteExistente = await _contexto.Clientes
                    .FirstOrDefaultAsync(c => c.CodigoCliente == cliente.CodigoCliente);

                if (clienteExistente == null)
                {
                    throw new Exception($"Cliente com código {cliente.CodigoCliente} não encontrado");
                }

                _contexto.Entry(clienteExistente).CurrentValues.SetValues(cliente);

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

        public async Task ExcluirCliente(int codigo)
        {
            try
            {
                var cliente = await _contexto.Clientes.FindAsync(codigo);

                if (cliente != null)
                {
                    _contexto.Clientes.Remove(cliente);
                    await _contexto.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _errorMessage = $"Erro ao excluir cliente: {ex.Message}";
                _logger.LogError(ex, _errorMessage);
                throw;
            }
        }
    }
}
