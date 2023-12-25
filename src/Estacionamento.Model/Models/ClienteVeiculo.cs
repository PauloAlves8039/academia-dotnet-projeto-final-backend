namespace Estacionamento.Model.Models
{
    public partial class ClienteVeiculo
    {
        public ClienteVeiculo()
        {
            Permanencia = new HashSet<Permanencia>();
        }

        public int CodigoClienteVeiculo { get; set; }
        public int? ClienteId { get; set; }
        public int? VeiculoId { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual Veiculo Veiculo { get; set; }
        public virtual ICollection<Permanencia> Permanencia { get; set; }
    }
}
