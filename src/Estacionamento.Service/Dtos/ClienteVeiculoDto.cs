using Estacionamento.Model.Models;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Estacionamento.Service.Dtos
{
    public class ClienteVeiculoDto
    {
        public int CodigoClienteVeiculo { get; set; }

        [DisplayName("Cliente")]
        public int? ClienteId { get; set; }

        [DisplayName("Veículo")]
        public int? VeiculoId { get; set; }

        [JsonIgnore]
        public virtual Cliente Cliente { get; set; }

        [JsonIgnore]
        public virtual Veiculo Veiculo { get; set; }

        [JsonIgnore]
        public virtual ICollection<Permanencia> Permanencia { get; set; }
    }
}
