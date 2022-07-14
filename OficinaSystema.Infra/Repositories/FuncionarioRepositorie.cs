using OficinaSystem.Domain.Entity;
using OficinaSystem.Domain.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace OficinaSystema.Infra.Repositories
{
    public class FuncionarioRepositorie : IFuncionarioRepositorie
    { 

        private readonly IConnection _connection;

        public FuncionarioRepositorie(IConnection connection)
        {
            _connection = connection;
        }

        public Funcionario Adicionar(Funcionario funcionario)
        {
            using (SqlCommand _command = _connection.CreateCommand())
            {
                string sql = "INSERT INTO Funcionario(Nome, Cpf, Endereco) VALUES(@Nome,@Cpf,@Endereco);SELECT @@IDENTITY;";
                _command.CommandText = sql;
                _command.Parameters.Add("@Nome", SqlDbType.VarChar).Value = funcionario.Nome;
                _command.Parameters.Add("@Cpf", SqlDbType.VarChar).Value = funcionario.Cpf;
                _command.Parameters.Add("@Endereco", SqlDbType.VarChar).Value = funcionario.Endereco;
                int id = 0;
                if (int.TryParse(_command.ExecuteScalar().ToString(), out id))
                {
                    funcionario.Id = id;
                    return funcionario;
                }
            }
            return null;
        }

        public List<Funcionario> ObterTodos()
        {
            List<Funcionario> lista = new();
            using (SqlCommand _command = _connection.CreateCommand())
            {
                _command.CommandText = "SELECT Id, Nome, Cpf, Endereco FROM Funcionario ORDER BY Id";
                using (SqlDataReader reader = _command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var funcionario = new Funcionario(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));
                            lista.Add(funcionario);
                            //yield return new Produto(reader.GetInt32(0), reader.GetDouble(1), reader.GetString(2));
                        }
                    }
                }
            }
            return lista;
        }
    }
}
