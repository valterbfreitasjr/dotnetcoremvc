using OficinaSystem.Domain.Entity;

namespace OficinaSystem.Domain.Interfaces
{
    public interface IFuncionarioRepositorie
    {
        Funcionario Adicionar(Funcionario funcionario);
        List<Funcionario> ObterTodos();
    }
}
