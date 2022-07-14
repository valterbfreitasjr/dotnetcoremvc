using System.Data.SqlClient;

namespace OficinaSystem.Domain.Interfaces
{
    public interface IConnection : IDisposable
    {
        SqlConnection Connect { get; }
        SqlConnection Open();
        void Close();
        SqlCommand CreateCommand();
    }
}
