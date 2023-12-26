namespace Estacionamento.Model.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            ClienteVeiculos = new HashSet<ClienteVeiculo>();
        }

        public int CodigoCliente { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public int? CodigoEndereco { get; set; }

        public virtual Endereco Endereco { get; set; }
        public virtual ICollection<ClienteVeiculo> ClienteVeiculos { get; set; }
    }
}
