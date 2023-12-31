﻿using AutoMapper;
using Estacionamento.Data.Interfaces;
using Estacionamento.Model.Models;
using Estacionamento.Service.Dtos;
using Estacionamento.Service.Interfaces;

namespace Estacionamento.Service.Services
{
    public class VeiculoService : IVeiculoService
    {
        private readonly IVeiculoRepository _veiculoRepository;
        private readonly IMapper _mapper;

        public VeiculoService(IVeiculoRepository veiculoRepository, IMapper mapper)
        {
            _veiculoRepository = veiculoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VeiculoDto>> PesquisarListaDeVeiculos()
        {
            var veiculos = await _veiculoRepository.GetAll();
            return _mapper.Map<IEnumerable<VeiculoDto>>(veiculos);
        }

        public async Task<VeiculoDto> PesquisarVeiculoPorCodigo(int codigo)
        {
            var veiculo = await _veiculoRepository.GetById(codigo);
            return _mapper.Map<VeiculoDto>(veiculo);
        }

        public async Task<VeiculoDto> AdicionarVeiculo(VeiculoDto veiculoDto)
        {
            var veiculo = _mapper.Map<Veiculo>(veiculoDto);
            await _veiculoRepository.Add(veiculo);
            return veiculoDto;
        }

        public async Task<VeiculoDto> AtualizarVeiculo(VeiculoDto veiculoDto)
        {
            var veiculo = _mapper.Map<Veiculo>(veiculoDto);
            await _veiculoRepository.Update(veiculo);
            return veiculoDto;
        }

        public async Task RemoverVeiculo(int codigo)
        {
            var veiculo = await _veiculoRepository.GetById(codigo);

            if (veiculo == null) 
            {
                throw new InvalidOperationException("Veículo não encontrado.");
            }

            await _veiculoRepository.Delete(codigo);
        }
    }
}
