using AutoMapper;
using Estacionamento.Data.Interfaces;
using Estacionamento.Model.Models;
using Estacionamento.Service.Dtos;
using Estacionamento.Service.Interfaces;

namespace Estacionamento.Service.Services
{
    public class EnderecoService : IEnderecoService
    {
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IMapper _mapper;

        public EnderecoService(IEnderecoRepository enderecoRepository, IMapper mapper)
        {
            _enderecoRepository = enderecoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EnderecoDto>> PesquisarListaDeEnderecos()
        {
            var enderecos = await _enderecoRepository.GetAll();
            return _mapper.Map<IEnumerable<EnderecoDto>>(enderecos);
        }

        public async Task<EnderecoDto> PesquisarEnderecoPorCodigo(int codigo)
        {
            var endereco = await _enderecoRepository.GetById(codigo);
            return _mapper.Map<EnderecoDto>(endereco);
        }

        public async Task<EnderecoDto> AdicionarEndereco(EnderecoDto enderecoDto)
        {
            var endereco = _mapper.Map<Endereco>(enderecoDto);
            await _enderecoRepository.Add(endereco);
            return enderecoDto;
        }

        public async Task<EnderecoDto> AltualizarEndereco(EnderecoDto enderecoDto)
        {
            var endereco = _mapper.Map<Endereco>(enderecoDto);
            await _enderecoRepository.Update(endereco);
            return enderecoDto;
        }

        public async Task RemoverEndereco(int codigo)
        {
            var endereco = await _enderecoRepository.GetById(codigo);

            if (endereco == null)
            {
                throw new InvalidOperationException("Endereço não encontrado.");
            }

            await _enderecoRepository.Delete(codigo);
        }
    }
}
