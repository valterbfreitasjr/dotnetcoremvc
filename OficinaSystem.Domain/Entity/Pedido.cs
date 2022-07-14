namespace OficinaSystem.Domain.Entity
{
    public class Pedido
    {
        public int Id { get; private set; }

        public Cliente Cliente { get; private set; }

        public Funcionario Funcionario { get; private set; }

        public DateTime DataPedido { get; private set; }

        public List<Produto>? Produtos { get; private set; }

        public List<Servico>? Servicos { get; private set; }

        public double ValorTotal { get; private set; }
    }
}
