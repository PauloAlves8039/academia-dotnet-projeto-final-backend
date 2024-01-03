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

        [Required(ErrorMessage = "A Placa é obrigatória")]
        [MinLength(7)]
        [MaxLength(10)]
        [DisplayName("Placa")]
        public string Placa { get; set; }

        [DisplayName("Data de Entrada")]
        public DateTime? DataEntrada { get; set; }

        [DisplayName("Data de Saída")]
        public DateTime? DataSaida { get; set; }

        [Required(ErrorMessage = "A Taxa Por Hora é obrigatória")]
        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [DataType(DataType.Currency)]
        [DisplayName("Taxa Por Hora")]
        public decimal TaxaPorHora { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [DataType(DataType.Currency)]
        [DisplayName("Valor Total")]
        public decimal? ValorTotal { get; set; }

        [Required(ErrorMessage = "O Estado da Permanência é obrigatório")]
        [MinLength(7)]
        [MaxLength(10)]
        [DisplayName("Estado da Permanência")]
        public string EstadoPermanencia { get; set; }

        [JsonIgnore]
        public virtual ClienteVeiculo ClienteVeiculo { get; set; }
    }
}
