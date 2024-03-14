using AutoMapper;
using Estacionamento.Data.Interfaces;
using Estacionamento.Model.Models;
using Estacionamento.Service.Dtos;
using Estacionamento.Service.Helpers;
using Estacionamento.Service.Interfaces;

namespace Estacionamento.Service.Services
{
    public class PermanenciaService : IPermanenciaService
    {
        private readonly IPermanenciaRepository _permanenciaRepository;
        private readonly IMapper _mapper;

        Util utilitario = new Util(); 

        public PermanenciaService(IPermanenciaRepository permanenciaRepository, IMapper mapper)
        {
            _permanenciaRepository = permanenciaRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PermanenciaDto>> PesquisarListaDePermanencias()
        {
            var permanencias = await _permanenciaRepository.GetAll();
            return _mapper.Map<IEnumerable<PermanenciaDto>>(permanencias);
        }

        public async Task<PermanenciaDto> PesquisarPermanenciaPorCodigo(int codigo)
        {
            var permanencia = await _permanenciaRepository.GetById(codigo);
            return _mapper.Map<PermanenciaDto>(permanencia);
        }

        public async Task<PermanenciaDto> AdicionarPermanencia(PermanenciaDto permanenciaDto)
        {
            var permanencia = _mapper.Map<Permanencia>(permanenciaDto);

            if (permanenciaDto.TaxaPorHora <= 0)
            {
                throw new ArgumentException("A taxa por hora deve ser maior que zero ao adicionar uma permanência.");
            }

            permanencia.TaxaPorHora = permanenciaDto.TaxaPorHora;

            await _permanenciaRepository.Add(permanencia);

            await utilitario.AtualizarEstadoPermanencia(permanencia.CodigoPermanencia, "Estacionado", _permanenciaRepository);

            return permanenciaDto;
        }

        public async Task<PermanenciaDto> AtualizarPermanencia(PermanenciaDto permanenciaDto)
        {
            permanenciaDto.DataSaida ??= DateTime.Now;

            double quantidadeHoras = utilitario.CalcularQuantidadeHorasPermanencia(permanenciaDto);

            decimal valorTotal = utilitario.CalcularValorTotal(quantidadeHoras, permanenciaDto.TaxaPorHora);

            var permanencia = _mapper.Map<Permanencia>(permanenciaDto);

            permanencia.ValorTotal = valorTotal;

            await _permanenciaRepository.Update(permanencia);

            await utilitario.AtualizarEstadoPermanencia(permanencia.CodigoPermanencia, "Retirado", _permanenciaRepository);

            return permanenciaDto;
        }

        public async Task RemoverPermanencia(int codigo)
        {
            var permanencia = await _permanenciaRepository.GetById(codigo);

            if (permanencia == null)
            {
                throw new InvalidOperationException("Permanência não encontrada.");
            }

            await _permanenciaRepository.Delete(codigo);
        }
    }
}
