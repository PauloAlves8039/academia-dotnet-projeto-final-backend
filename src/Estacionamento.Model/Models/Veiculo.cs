namespace Estacionamento.Model.Models
{
    public partial class Veiculo
    {
        public Veiculo()
        {
            ClienteVeiculos = new HashSet<ClienteVeiculo>();
        }

        public int CodigoVeiculo { get; set; }
        public string Tipo { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Cor { get; set; }
        public int Ano { get; set; }
        public string? Observacoes { get; set; }

        public virtual ICollection<ClienteVeiculo> ClienteVeiculos { get; set; }
    }
}
