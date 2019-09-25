using Shared;
using System.Data.SqlClient;

namespace Repository.DataContexts
{
    public class DataContext
    {
        public SqlConnection Connection { get; set; }

        public DataContext()
        {
            Connection = new SqlConnection(Settings.ConnectionString);
            Connection.Open();
        }
    }
}
