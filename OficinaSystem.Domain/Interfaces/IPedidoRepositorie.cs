using OficinaSystem.Domain.Entity;

namespace OficinaSystem.Domain.Interfaces
{
    public interface IPedidoRepositorie
    {
        int Adicionar(Pedido pedido);
        int AdicionarPedidoServico(List<Servico> servicos, int id);
        int AdicionarPedidoProduto(List<Produto> produtos, int id);
    }
}
