using MySql.Data.MySqlClient;

namespace Imoblink.DAOs
{
    public class ConnectionFactory
    {
        public static MySqlConnection build()
        {
            var connectionString = "Server=localhost; Database=imoblink;Uid=root;Pwd=root;";
            return new MySqlConnection(connectionString);
        }

        
    }
}
