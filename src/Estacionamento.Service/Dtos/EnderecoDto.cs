using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Estacionamento.Service.Dtos
{
    public class EnderecoDto
    {
        public int CodigoEndereco { get; set; }

        [Required(ErrorMessage = "O Logradouro é obrigatório")]
        [MinLength(3)]
        [MaxLength(100)]
        [DisplayName("Logradouro")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "O Número é obrigatório")]
        [MinLength(1)]
        [MaxLength(10)]
        [DisplayName("Número")]
        public string Numero { get; set; }

        [DisplayName("Complemento")]
        [MaxLength(150)]
        public string? Complemento { get; set; }

        [Required(ErrorMessage = "O Bairro é obrigatório")]
        [MinLength(3)]
        [MaxLength(100)]
        [DisplayName("Bairro")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "A Cidade é obrigatória")]
        [MinLength(5)]
        [MaxLength(100)]
        [DisplayName("Cidade")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "O Estado é obrigatório")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Estado")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "O CEP é obrigatório")]
        [MinLength(8)]
        [MaxLength(10)]
        [DisplayName("Estado")]
        public string Cep { get; set; }
    }
}
