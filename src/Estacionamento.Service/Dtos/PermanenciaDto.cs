using Estacionamento.Model.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Estacionamento.Service.Dtos
{
    public class PermanenciaDto
    {
        public int CodigoPermanencia { get; set; }

        [DisplayName("Cliente e Veículo")]
        public int? ClienteVeiculoId { get; set; }

        [DisplayName("Placa")]
        public string Placa { get; set; }

        [DisplayName("Data de Entrada")]
        public DateTime? DataEntrada { get; set; }

        [DisplayName("Data de Saída")]
        public DateTime? DataSaida { get; set; }

        [DisplayName("Taxa Por Hora")]
        public decimal TaxaPorHora { get; set; }

        [DisplayName("Valor Total")]
        public decimal? ValorTotal { get; set; }

        [DisplayName("Estado da Permanência")]
        public string EstadoPermanencia { get; set; }

        [JsonIgnore]
        public virtual ClienteVeiculo ClienteVeiculo { get; set; }
    }
}
