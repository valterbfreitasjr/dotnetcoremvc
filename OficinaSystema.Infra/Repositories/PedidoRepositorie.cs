using OficinaSystem.Domain.Entity;
using OficinaSystem.Domain.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace OficinaSystema.Infra.Repositories
{
    public class PedidoRepositorie : IPedidoRepositorie
    {
        private readonly IConnection _connection;

        public PedidoRepositorie(IConnection connection)
        {
            _connection = connection;
        }

        public int AdicionarPedido(Pedido pedido)
        {
            using (SqlCommand _command = _connection.CreateCommand())
            {
                SqlConnection connection = _connection.Connect;
                SqlTransaction transaction = connection.BeginTransaction();

                _command.Connection = connection;
                _command.Transaction = transaction;

                try
                {
                    string sql = "INSERT INTO Pedido(ClienteId, FuncionarioId, DataPedido, ValorTotal) VALUES(@ClienteId,@FuncionarioId,@DataPedido,@ValorTotal);SELECT @@IDENTITY;";
                    _command.CommandText = sql;
                    _command.Parameters.Add("@ClienteId", SqlDbType.Int).Value = pedido.Cliente.Id;
                    _command.Parameters.Add("@FuncionarioId", SqlDbType.Int).Value = pedido.Funcionario.Id;
                    _command.Parameters.Add("@DataPedido", SqlDbType.DateTime).Value = pedido.DataPedido;
                    _command.Parameters.Add("@ValorTotal", SqlDbType.Decimal).Value = pedido.ValorTotal;
                    int id = 0;
                    int.TryParse(_command.ExecuteScalar().ToString(), out id);

                    if (pedido.Produtos.Any())
                    {
                        foreach (var item in pedido.Produtos)
                        {
                            sql = "INSERT INTO Pedido_X_Produto(PedidoId, ProdutoId) VALUES(@PedidoId,@ProdutoId);";
                            _command.CommandText = sql;
                            _command.Parameters.Clear();
                            _command.Parameters.Add("@PedidoId", SqlDbType.Int).Value = id;
                            _command.Parameters.Add("@ProdutoId", SqlDbType.Int).Value = item.Id;
                            _command.ExecuteNonQuery();
                        }
                        
                    }

                    if (pedido.Servicos.Any())
                    {
                        foreach (var item in pedido.Servicos)
                        {
                            sql = "INSERT INTO Pedido_X_Servico(PedidoId, ServicoId) VALUES(@PedidoId,@ServicoId);";
                            _command.CommandText = sql;
                            _command.Parameters.Clear();
                            _command.Parameters.Add("@PedidoId", SqlDbType.Int).Value = id;
                            _command.Parameters.Add("@ServicoId", SqlDbType.Int).Value = item.Id;
                            _command.ExecuteNonQuery();
                        }
                    }

                    _command.Transaction.Commit();
                    return 1;
                }

                catch (SqlException ex)
                {
                    _command.Transaction.Rollback();
                    Console.WriteLine(ex.Message);
                }
            }

            return 0;
        }

        public List<Pedido> ObterTodos()
        {
            List<Pedido> lista = new();
            using (SqlCommand _command = _connection.CreateCommand())
            {
                _command.CommandText = @"SELECT Pd.Id, Pd.DataPedido, Pd.ValorTotal, Cl.Id, Cl.Nome, Fn.Id, Fn.Nome
                                         FROM Pedido Pd
                                         INNER JOIN Cliente Cl ON Pd.ClienteId = Cl.Id
                                         INNER JOIN Funcionario Fn ON Pd.FuncionarioId = Fn.Id
                                         ORDER BY Pd.Id";
                using (SqlDataReader reader = _command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var cliente = new Cliente(reader.GetInt32(3), reader.GetString(4));
                            var funcionario = new Funcionario(reader.GetInt32(5), reader.GetString(6));
                            var pedido = new Pedido(reader.GetInt32(0), reader.GetDateTime(1), reader.GetDecimal(2), cliente, funcionario);
                            lista.Add(pedido);
                            //yield return new Produto(reader.GetInt32(0), reader.GetDouble(1), reader.GetString(2));
                        }
                    }
                }
            }
            return lista;
        }
    }


}
