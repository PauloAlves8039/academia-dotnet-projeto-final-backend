using AutoMapper;
using Estacionamento.Data.Interfaces;
using Estacionamento.Model.Models;
using Estacionamento.Service.Dtos;
using Estacionamento.Service.Interfaces;

namespace Estacionamento.Service.Services
{
    public class ClienteVeiculoService : IClienteVeiculoService
    {
        private readonly IClienteVeiculoRepository _clienteVeiculoRepository;
        private readonly IMapper _mapper;

        public ClienteVeiculoService(IClienteVeiculoRepository clienteVeiculoRepository, IMapper mapper)
        {
            _clienteVeiculoRepository = clienteVeiculoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClienteVeiculoDto>> PesquisarListaDeClienteMoto()
        {
            var clientesVeiculos = await _clienteVeiculoRepository.ObterListaDeClienteVeiculo();
            return _mapper.Map<IEnumerable<ClienteVeiculoDto>>(clientesVeiculos);
        }

        public async Task<IEnumerable<ClienteVeiculoDto>> PesquisarItensClienteMoto()
        {
            var clientesVeiculos = await _clienteVeiculoRepository.ObterItensClienteVeiculo();
            return _mapper.Map<IEnumerable<ClienteVeiculoDto>>(clientesVeiculos);
        }

        public async Task<ClienteVeiculoDto> PesquisarClienteMotoPorCodigo(int codigo)
        {
            var clienteVeiculo = await _clienteVeiculoRepository.ObterClienteVeiculoPorCodigo(codigo);
            return _mapper.Map<ClienteVeiculoDto>(clienteVeiculo);
        }

        public async Task<ClienteVeiculoDto> AdicionarClienteMoto(ClienteVeiculoDto clienteVeiculoDto)
        {
            var clienteVeiculo = _mapper.Map<ClienteVeiculo>(clienteVeiculoDto);
            await _clienteVeiculoRepository.CadastrarClienteVeiculo(clienteVeiculo);
            return clienteVeiculoDto;
        }
    }
}
