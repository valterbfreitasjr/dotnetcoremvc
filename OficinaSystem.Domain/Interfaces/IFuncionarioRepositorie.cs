using OficinaSystem.Domain.Entity;

namespace OficinaSystem.Domain.Interfaces
{
    public interface IFuncionarioRepositorie
    {
        Funcionario Adicionar(Funcionario funcionario);
        List<Funcionario> ObterTodos();
        Funcionario ObterFuncionario(int id);
        bool EditarFuncionario(Funcionario funcionario);
        bool Delete(int id);
    }
}
