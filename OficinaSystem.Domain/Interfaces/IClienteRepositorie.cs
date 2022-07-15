using OficinaSystem.Domain.Entity;

namespace OficinaSystem.Domain.Interfaces
{
    public interface IClienteRepositorie
    {
        Cliente Adicionar(Cliente cliente);
        List<Cliente> ObterTodos();
        Cliente ObterCliente(int id);
        bool EditarCliente(Cliente cliente);
        bool Delete(int id);
    }
}
