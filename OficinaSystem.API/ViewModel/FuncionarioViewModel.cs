using System.Text.Json.Serialization;

namespace OficinaSystem.API.ViewModel
{
    public class FuncionarioViewModel
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Cpf { get; set; }

        public string Endereco { get; set; } 
    }
}
