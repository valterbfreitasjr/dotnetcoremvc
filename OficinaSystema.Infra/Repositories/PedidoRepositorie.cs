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
    }


}
