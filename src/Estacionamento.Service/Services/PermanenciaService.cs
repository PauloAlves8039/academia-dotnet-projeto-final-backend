﻿using AutoMapper;
using Estacionamento.Data.Interfaces;
using Estacionamento.Model.Models;
using Estacionamento.Service.Dtos;
using Estacionamento.Service.Interfaces;

namespace Estacionamento.Service.Services
{
    public class PermanenciaService : IPermanenciaService
    {
        private readonly IPermanenciaRepository _permanenciaRepository;
        private readonly IMapper _mapper;

        public PermanenciaService(IPermanenciaRepository permanenciaRepository, IMapper mapper)
        {
            _permanenciaRepository = permanenciaRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PermanenciaDto>> PesquisarListaDePermanencias()
        {
            var permanencias = await _permanenciaRepository.ObterListaDePermanencias();
            return _mapper.Map<IEnumerable<PermanenciaDto>>(permanencias);
        }

        public async Task<PermanenciaDto> PesquisarPermanenciaPorCodigo(int codigo)
        {
            var permanencia = await _permanenciaRepository.ObterPermanenciaPorCodigo(codigo);
            return _mapper.Map<PermanenciaDto>(permanencia);
        }

        public async Task<PermanenciaDto> AdicionarPermanencia(PermanenciaDto permanenciaDto)
        {
            var permanencia = _mapper.Map<Permanencia>(permanenciaDto);

            permanencia.TaxaPorHora = 3.00m;

            await _permanenciaRepository.CadastrarPermanencia(permanencia);

            await AtualizarEstadoPermanencia(permanencia.CodigoPermanencia, "Estacionado");

            return permanenciaDto;
        }

        public async Task<PermanenciaDto> AtualizarPermanencia(PermanenciaDto permanenciaDto)
        {
            permanenciaDto.DataSaida ??= DateTime.Now;

            double quantidadeHoras = CalcularQuantidadeHorasPermanencia(permanenciaDto);

            decimal valorTotal = CalcularValorTotal(quantidadeHoras);

            var permanencia = _mapper.Map<Permanencia>(permanenciaDto);

            permanencia.TaxaPorHora = 3.00m;
            permanencia.ValorTotal = valorTotal;

            await _permanenciaRepository.AlterarPermanencia(permanencia);

            await AtualizarEstadoPermanencia(permanencia.CodigoPermanencia, "Retirado");

            return permanenciaDto;
        }

        private double CalcularQuantidadeHorasPermanencia(PermanenciaDto permanenciaDto)
        {
            if (permanenciaDto.DataEntrada == null || permanenciaDto.DataSaida == null)
            {
                return 0;
            }

            TimeSpan diferencaDeHoras = permanenciaDto.DataSaida.Value - permanenciaDto.DataEntrada.Value;
            return diferencaDeHoras.TotalHours;
        }

        private async Task AtualizarEstadoPermanencia(int codigoPermanencia, string novoEstado)
        {
            var permanenciaExistente = await _permanenciaRepository.ObterPermanenciaPorCodigo(codigoPermanencia);

            if (permanenciaExistente != null)
            {
                permanenciaExistente.EstadoPermanencia = novoEstado;
                await _permanenciaRepository.AlterarPermanencia(permanenciaExistente);
            }
        }

        private decimal CalcularValorTotal(double quantidadeHoras)
        {
            decimal taxaPorHora = 3.00m;

            decimal valorTotal = (decimal)quantidadeHoras * taxaPorHora;

            return valorTotal;
        }
    }
}