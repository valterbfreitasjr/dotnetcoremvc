using OficinaSystem.Domain.Entity;

namespace OficinaSystem.Domain.Interfaces
{
    public interface IServicoRepositorie
    {
        Servico Adicionar(Servico servico);
        List<Servico> ObterTodos();
        Servico ObterServico(int id);
        bool EditarServico(Servico servico);
        bool Delete(int id);
        List<Servico> ObterServicoPedido(int id);
    }
}
