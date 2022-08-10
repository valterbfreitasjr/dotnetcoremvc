using OficinaSystem.Domain.Entity;

namespace OficinaSystem.Domain.Interfaces
{
    public interface IProdutoRepositorie
    {
        Produto Adiconar(Produto produto);
        List<Produto> ObterTodos();
        Produto ObterProduto(int id);
        bool EditarProduto(Produto produto);
        bool Delete(int id);
        List<Produto> ObterProdutoPedido(int id);
    }
}
