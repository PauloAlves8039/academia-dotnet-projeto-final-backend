namespace Estacionamento.Service.Dtos
{
    public class VeiculoDto
    {
        public int CodigoVeiculo { get; set; }
        public string Tipo { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Cor { get; set; }
        public int Ano { get; set; }
        public string? Observacoes { get; set; }
    }
}
