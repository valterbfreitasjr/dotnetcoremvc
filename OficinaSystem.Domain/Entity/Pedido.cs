namespace OficinaSystem.Domain.Entity
{
    public class Pedido
    {
        public int Id { get; set; }

        public Cliente Cliente { get; set; }

        public Funcionario Funcionario { get; set; }

        public DateTime DataPedido { get; set; }

        public List<Produto>? Produtos { get; set; }

        public List<Servico>? Servicos { get; set; }

        public decimal ValorTotal { get; set; }
    }
}
