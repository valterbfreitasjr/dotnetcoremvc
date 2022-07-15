using OficinaSystem.Domain.Entity;

namespace OficinaSystem.API.ViewModel
{
    public class PedidoViewModel
    {
        public Cliente Cliente { get; set; }

        public Funcionario Funcionario { get; set; }

        public DateTime DataPedido { get; set; }

        public List<Produto>? Produtos { get; set; }

        public List<Servico>? Servicos { get; set; }

        public double ValorTotal { get; set; }
    }
}
