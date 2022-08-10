using OficinaSystem.Domain.Entity;

namespace OficinaSystem.Domain.Interfaces
{
    public interface IPedidoRepositorie
    {
        int AdicionarPedido(Pedido pedido);
        List<Pedido> ObterTodos();
    }
}
