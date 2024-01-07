using AutoMapper;
using Estacionamento.Data.Interfaces;
using Estacionamento.Model.Models;
using Estacionamento.Service.Dtos;
using Estacionamento.Service.Interfaces;

namespace Estacionamento.Service.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;

        public ClienteService(IClienteRepository clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClienteDto>> PesquisarListaDeClientes()
        {
            var clientes = await _clienteRepository.GetAll();
            return _mapper.Map<IEnumerable<ClienteDto>>(clientes);
        }

        public async Task<ClienteDto> PesquisarClientePorCodigo(int codigo)
        {
            var cliente = await _clienteRepository.GetById(codigo);
            return _mapper.Map<ClienteDto>(cliente);
        }

        public async Task<ClienteDto> AdicionarCliente(ClienteDto clienteDto)
        {
            var cliente = _mapper.Map<Cliente>(clienteDto);
            await _clienteRepository.Add(cliente);
            return clienteDto;
        }

        public async Task<ClienteDto> AtualizarCliente(ClienteDto clienteDto)
        {
            var cliente = _mapper.Map<Cliente>(clienteDto);
            await _clienteRepository.Update(cliente);
            return clienteDto;
        }

        public async Task RemoverCliente(int codigo)
        {
            var cliente = await _clienteRepository.GetById(codigo);

            if (cliente == null)
            {
                throw new InvalidOperationException("Cliente não encontrado(a).");
            }

            await _clienteRepository.Delete(codigo);
        }
    }
}
