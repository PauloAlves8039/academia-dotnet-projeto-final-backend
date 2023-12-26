using Estacionamento.Model.Models;
using System.Text.Json.Serialization;

namespace Estacionamento.Service.Dtos
{
    public class PermanenciaDto
    {
        public int CodigoPermanencia { get; set; }
        public int? ClienteVeiculoId { get; set; }
        public string Placa { get; set; }
        public DateTime? DataEntrada { get; set; }
        public DateTime? DataSaida { get; set; }
        public decimal TaxaPorHora { get; set; }
        public decimal? ValorTotal { get; set; }
        public string EstadoPermanencia { get; set; }

        [JsonIgnore]
        public virtual ClienteVeiculo ClienteVeiculo { get; set; }
    }
}
