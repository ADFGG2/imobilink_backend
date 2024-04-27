using MySql.Data.MySqlClient;

namespace Imoblink.DAOs
{
    public class ConnectionFactory
    {
        public static MySqlConnection Build()
        {
            var connectionString = "Server=localhost; Database=imoblink;Uid=root;Pwd=;";
            return new MySqlConnection(connectionString);
        }

        
    }
}
