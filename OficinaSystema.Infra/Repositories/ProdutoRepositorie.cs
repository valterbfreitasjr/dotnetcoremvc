using OficinaSystem.Domain.Entity;
using OficinaSystem.Domain.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace OficinaSystema.Infra.Repositories
{
    public class ProdutoRepositorie : IProdutoRepositorie
    {

        private readonly IConnection _connection;

        public ProdutoRepositorie(IConnection connection)
        {
            _connection = connection;
        }

        public Produto Adiconar(Produto produto)
        {
            using (SqlCommand _command = _connection.CreateCommand())
            {
                string sql = "INSERT INTO Produto(Descricao,Preco) VALUES(@Descricao,@Preco);SELECT @@IDENTITY;";
                _command.CommandText = sql;
                _command.Parameters.Add("@Descricao", SqlDbType.VarChar).Value = produto.Descricao;
                _command.Parameters.Add("@Preco", SqlDbType.Decimal).Value = produto.Preco;
                int id = 0;
                if (int.TryParse(_command.ExecuteScalar().ToString(), out id))
                {
                    produto.Id = id;
                    return produto;
                }
            }
            return null;
        }

        public List<Produto> ObterTodos()
        {
            List<Produto> lista = new ();
            using (SqlCommand _command = _connection.CreateCommand())
            {
                _command.CommandText = "SELECT Id, Preco, Descricao FROM Produto ORDER BY Id";
                using (SqlDataReader reader = _command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var produto = new Produto(reader.GetInt32(0), reader.GetDecimal(1), reader.GetString(2));
                            lista.Add(produto);
                            //yield return new Produto(reader.GetInt32(0), reader.GetDouble(1), reader.GetString(2));
                        }
                    }
                }
            }
            return lista;
        }

        public Produto ObterProduto(int id)
        {
            using (SqlCommand _command = _connection.CreateCommand())
            {
                _command.CommandText = "SELECT Id, Preco, Descricao  FROM Produto WHERE Id=@Id";
                _command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                using (SqlDataReader reader = _command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        var produto = new Produto(reader.GetInt32(0), reader.GetDecimal(1), reader.GetString(2));
                        return produto;
                    }
                }
            }
            return null;
        }

        public bool EditarProduto(Produto produto)
        {
            bool ret = false;
            using (SqlCommand _command = _connection.CreateCommand())
            {
                _command.CommandText = "UPDATE Produto SET Preco=@Preco,Descricao=@Descricao WHERE Id=@Id";
                _command.Parameters.Add("@Descricao", SqlDbType.VarChar, 50).Value = produto.Descricao;
                _command.Parameters.Add("@Id", SqlDbType.Int).Value = produto.Id;
                _command.Parameters.Add("@Preco", SqlDbType.Decimal).Value = produto.Preco;
                ret = _command.ExecuteNonQuery() > 0;
            }
            return ret;
        }

        public bool Delete(int id)
        {
            bool ret = false;
            using (SqlCommand _command = _connection.CreateCommand())
            {
                _command.CommandText = "DELETE FROM Produto WHERE Id=@Id";
                _command.Parameters.Add("@Id", SqlDbType.Int).Value = (int)id;
                ret = _command.ExecuteNonQuery() > 0;
            }
            return ret;
        }
    }
}
