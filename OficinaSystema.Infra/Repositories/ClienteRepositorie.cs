using OficinaSystem.Domain.Entity;
using OficinaSystem.Domain.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace OficinaSystema.Infra.Repositories
{
    public class ClienteRepositorie : IClienteRepositorie
    {
        private readonly IConnection _connection;

        public ClienteRepositorie(IConnection connection)
        {
            _connection = connection;
        }

        public Cliente Adicionar(Cliente cliente)
        {
            using (SqlCommand _command = _connection.CreateCommand())
            {
                string sql = "INSERT INTO Cliente(Nome, Cpf, Endereco) VALUES(@Nome,@Cpf,@Endereco);SELECT @@IDENTITY;";
                _command.CommandText = sql;
                _command.Parameters.Add("@Nome", SqlDbType.VarChar).Value = cliente.Nome;
                _command.Parameters.Add("@Cpf", SqlDbType.VarChar).Value = cliente.Cpf;
                _command.Parameters.Add("@Endereco", SqlDbType.VarChar).Value = cliente.Endereco;
                int id = 0;
                if (int.TryParse(_command.ExecuteScalar().ToString(), out id))
                {
                    cliente.Id = id;
                    return cliente;
                }
            }
            return null;
        }

        public List<Cliente> ObterTodos()
        {
            List<Cliente> lista = new();
            using (SqlCommand _command = _connection.CreateCommand())
            {
                _command.CommandText = "SELECT Id, Nome, Cpf, Endereco FROM Cliente ORDER BY Id";
                using (SqlDataReader reader = _command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var cliente = new Cliente(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));
                            lista.Add(cliente);
                            //yield return new Produto(reader.GetInt32(0), reader.GetDouble(1), reader.GetString(2));
                        }
                    }
                }
            }
            return lista;
        }

        public Cliente ObterCliente(int id)
        {
            using (SqlCommand _command = _connection.CreateCommand())
            {
                _command.CommandText = "SELECT Id, Nome, Cpf, Endereco  FROM Cliente WHERE Id=@Id";
                _command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                using (SqlDataReader reader = _command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        var cliente = new Cliente(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));
                        return cliente;
                    }
                }
            }
            return null;
        }

        public bool EditarCliente(Cliente cliente)
        {
            bool ret = false;
            using (SqlCommand _command = _connection.CreateCommand())
            {
                _command.CommandText = "UPDATE Cliente SET Nome=@Nome,Cpf=@Cpf,Endereco=@Endereco WHERE Id=@Id";
                _command.Parameters.Add("@Id", SqlDbType.Int).Value = cliente.Id;
                _command.Parameters.Add("@Nome", SqlDbType.VarChar, 100).Value = cliente.Nome;
                _command.Parameters.Add("@Cpf", SqlDbType.VarChar, 12).Value = cliente.Cpf;
                _command.Parameters.Add("@Endereco", SqlDbType.VarChar, 50).Value = cliente.Endereco;
                ret = _command.ExecuteNonQuery() > 0;
            }
            return ret;
        }

        public bool Delete(int id)
        {
            bool ret = false;
            using (SqlCommand _command = _connection.CreateCommand())
            {
                _command.CommandText = "DELETE FROM Cliente WHERE Id=@Id";
                _command.Parameters.Add("@Id", SqlDbType.Int).Value = (int)id;
                ret = _command.ExecuteNonQuery() > 0;
            }
            return ret;
        }
    }
}
