using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Estacionamento.Service.Dtos
{
    public class ClienteDto
    {
        public int CodigoCliente { get; set; }

        [Required(ErrorMessage = "O Nome é obrigatório")]
        [MinLength(5)]
        [MaxLength(100)]
        [DisplayName("Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A Data de Nascimento é obrigatória")]
        [DisplayName("Data de Nascimento")]
        [DisplayFormat(DataFormatString = "dd/mm/yyyy")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório")]
        [MinLength(11)]
        [MaxLength(14)]
        [DisplayName("CPF")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "O Telefone é obrigatório")]
        [MinLength(10)]
        [MaxLength(20)]
        [DisplayName("Telefone")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O Email é obrigatório")]
        [MinLength(10)]
        [MaxLength(100)]
        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Endereço")]
        public int CodigoEndereco { get; set; }

        [JsonIgnore]
        public virtual EnderecoDto Endereco { get; set; }
    }
}
