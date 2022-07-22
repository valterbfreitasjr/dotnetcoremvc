using OficinaSystem.Domain.Entity;
using OficinaSystem.Domain.Interfaces;
using OficinaSystem.Domain.Services.Interfaces;

namespace OficinaSystem.Domain.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepositorie _pedidoRepositorie;

        public PedidoService(IPedidoRepositorie pedidoRepositorie)
        {
            _pedidoRepositorie = pedidoRepositorie;
        }

        public int AdicionarPedidos(Pedido pedido)
        {
            var valorTotal = pedido.Produtos.Any() ? pedido.Produtos.Sum(s => s.Preco) : 0;
            valorTotal += pedido.Servicos.Any() ? pedido.Servicos.Sum(s => s.Preco) : 0;

            pedido.SetarPedido(valorTotal);
            var resultPedido = _pedidoRepositorie.AdicionarPedido(pedido);

            return resultPedido;
        }

    }
}
