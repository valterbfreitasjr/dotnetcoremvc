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
                string sql = "INSERT INTO Servico(Preco, Descricao) VALUES(,@Preco,@Descricao);SELECT @@IDENTITY;";
                _command.CommandText = sql;
                _command.Parameters.Add("@Preco", SqlDbType.Float).Value = servico.Preco;
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
    }
}
