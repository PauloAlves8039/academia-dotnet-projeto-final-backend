using Estacionamento.Data.Interfaces;
using Estacionamento.Service.Dtos;

namespace Estacionamento.Service.Helpers
{
    public class Util
    {
        public async Task AtualizarEstadoPermanencia(int codigoPermanencia, string novoEstado, IPermanenciaRepository permanenciaRepository)
        {
            var permanenciaExistente = await permanenciaRepository.GetById(codigoPermanencia);

            if (permanenciaExistente != null)
            {
                permanenciaExistente.EstadoPermanencia = novoEstado;
                await permanenciaRepository.Update(permanenciaExistente);
            }
        }

        public double CalcularQuantidadeHorasPermanencia(PermanenciaDto permanenciaDto)
        {
            if (permanenciaDto.DataEntrada == null || permanenciaDto.DataSaida == null)
            {
                return 0;
            }

            TimeSpan diferencaDeHoras = permanenciaDto.DataSaida.Value - permanenciaDto.DataEntrada.Value;
            return diferencaDeHoras.TotalHours;
        }

        public decimal CalcularValorTotal(double quantidadeHoras, decimal taxaPorHora)
        {
            decimal valorTotal = (decimal)quantidadeHoras * taxaPorHora;

            return valorTotal;
        }
    }
}
