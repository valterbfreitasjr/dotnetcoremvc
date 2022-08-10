namespace OficinaSystem.Domain.Entity
{
    public class Funcionario
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Cpf { get; set; }

        public string Endereco { get; set; }

        public Funcionario()
        {

        }

        public Funcionario(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public Funcionario(int id, string nome, string cpf, string endereco)
        {
            Id = id;
            Nome = nome;
            Cpf = cpf;
            Endereco = endereco;
        }
    }
}
