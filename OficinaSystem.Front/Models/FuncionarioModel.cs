namespace OficinaSystem.Front.Models
{
    public class FuncionarioModel
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Cpf { get; set; }

        public string Endereco { get; set; }

        public FuncionarioModel()
        {

        }

        public FuncionarioModel(int id, string nome, string cpf, string endereco)
        {
            Id = id;
            Nome = nome;
            Cpf = cpf;
            Endereco = endereco;
        }
    }
}
