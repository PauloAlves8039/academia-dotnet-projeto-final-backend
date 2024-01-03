using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Estacionamento.Service.Dtos
{
    public class VeiculoDto
    {
        public int CodigoVeiculo { get; set; }

        [Required(ErrorMessage = "O Tipo é obrigatório")]
        [MinLength(2)]
        [MaxLength(20)]
        [DisplayName("Tipo")]
        public string Tipo { get; set; }

        [Required(ErrorMessage = "A Marca é obrigatória")]
        [MinLength(3)]
        [MaxLength(50)]
        [DisplayName("Marca")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "O Modelo é obrigatório")]
        [MinLength(3)]
        [MaxLength(50)]
        [DisplayName("Modelo")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "A Cor é obrigatória")]
        [MinLength(3)]
        [MaxLength(50)]
        [DisplayName("Cor")]
        public string Cor { get; set; }

        [Required(ErrorMessage = "O Ano é obrigatório")]
        [Range(1, 9999)]
        [DisplayName("Ano")]
        public int Ano { get; set; }

        [DisplayName("Observações")]
        [MaxLength(150)]
        public string? Observacoes { get; set; }
    }
}
