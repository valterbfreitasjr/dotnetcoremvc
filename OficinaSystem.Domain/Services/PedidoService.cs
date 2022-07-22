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

        public void AdicionarPedidos(Pedido pedido)
        {
            var valorTotal = pedido.Produtos.Any() ? pedido.Produtos.Sum(s => s.Preco) : 0;
            valorTotal += pedido.Servicos.Any() ? pedido.Servicos.Sum(s => s.Preco) : 0;

            pedido.ValorTotal = valorTotal;
 
            var resultPedido = _pedidoRepositorie.Adicionar(pedido);

            if(resultPedido > 0) 
            {
                if(pedido.Produtos.Any())
                    _pedidoRepositorie.AdicionarPedidoProduto(pedido.Produtos, resultPedido);

                if(pedido.Servicos.Any())
                    _pedidoRepositorie.AdicionarPedidoServico(pedido.Servicos, resultPedido);
            }
        }

    }
}
