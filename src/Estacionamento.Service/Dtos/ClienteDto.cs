using System.Text.Json.Serialization;

namespace Estacionamento.Service.Dtos
{
    public class ClienteDto
    {
        public int CodigoCliente { get; set; }
        public string Nome { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public int? CodigoEndereco { get; set; }

        [JsonIgnore]
        public virtual EnderecoDto Endereco { get; set; }
    }
}
