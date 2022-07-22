using OficinaSystem.Domain.Entity;
using OficinaSystem.Domain.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace OficinaSystema.Infra.Repositories
{
    public class ServicoRepositorie : IServicoRepositorie
    {
        private readonly IConnection _connection;

        public ServicoRepositorie(IConnection connection)
        {
            _connection = connection;
        }

        public Servico Adicionar(Servico servico)
        {
            using (SqlCommand _command = _connection.CreateCommand())
            {
                string sql = "INSERT INTO Servico(Preco, Descricao) VALUES(@Preco,@Descricao);SELECT @@IDENTITY;";
                _command.CommandText = sql;
                _command.Parameters.Add("@Preco", SqlDbType.Decimal).Value = servico.Preco;
                _command.Parameters.Add("@Descricao", SqlDbType.VarChar).Value = servico.Descricao;
                int id = 0;
                if (int.TryParse(_command.ExecuteScalar().ToString(), out id))
                {
                    servico.Id = id;
                    return servico;
                }
            }
            return null;
        }

        public List<Servico> ObterTodos()
        {
            List<Servico> lista = new();
            using (SqlCommand _command = _connection.CreateCommand())
            {
                _command.CommandText = "SELECT Id, Preco, Descricao FROM Servico ORDER BY Id";
                using (SqlDataReader reader = _command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var servico = new Servico(reader.GetInt32(0), reader.GetDecimal(1), reader.GetString(2));
                            lista.Add(servico);
                            //yield return new Produto(reader.GetInt32(0), reader.GetDouble(1), reader.GetString(2));
                        }
                    }
                }
            }
            return lista;
        }

        public Servico ObterServico(int id)
        {
            using (SqlCommand _command = _connection.CreateCommand())
            {
                _command.CommandText = "SELECT Id, Preco, Descricao  FROM Servico WHERE Id=@Id";
                _command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                using (SqlDataReader reader = _command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        var servico = new Servico(reader.GetInt32(0), reader.GetDecimal(1), reader.GetString(2));
                        return servico;
                    }
                }
            }
            return null;
        }

        public bool EditarServico(Servico servico)
        {
            bool ret = false;
            using (SqlCommand _command = _connection.CreateCommand())
            {
                _command.CommandText = "UPDATE Servico SET Preco=@Preco,Descricao=@Descricao WHERE Id=@Id";
                _command.Parameters.Add("@Descricao", SqlDbType.VarChar, 50).Value = servico.Descricao;
                _command.Parameters.Add("@Id", SqlDbType.Int).Value = servico.Id;
                _command.Parameters.Add("@Preco", SqlDbType.Float).Value = servico.Preco;
                ret = _command.ExecuteNonQuery() > 0;
            }
            return ret;
        }

        public bool Delete(int id)
        {
            bool ret = false;
            using (SqlCommand _command = _connection.CreateCommand())
            {
                _command.CommandText = "DELETE FROM Servico WHERE Id=@Id";
                _command.Parameters.Add("@Id", SqlDbType.Int).Value = (int)id;
                ret = _command.ExecuteNonQuery() > 0;
            }
            return ret;
        }
    }
}
