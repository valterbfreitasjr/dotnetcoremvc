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

        public void SetarPedido(decimal valorTotal)
        {
            DataPedido = DateTime.Now;
            ValorTotal = valorTotal;
        }

        public Pedido(int id, DateTime dataPedido, decimal valorTotal, Cliente cliente, Funcionario funcionario)
        {
            Id = id;
            Cliente = cliente;
            Funcionario = funcionario;
            DataPedido = dataPedido;
            ValorTotal = valorTotal;
        }
    }
}
