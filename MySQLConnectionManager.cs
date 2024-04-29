using MySql.Data.MySqlClient;

public class MySQLConnectionManager
{
    private MySqlConnection connection;
    private string server;
    private string database;
    private string uid;
    private string password;

    public MySQLConnectionManager()
    {
        Initialize();
    }

    private void Initialize()
    {
        server = "localhost";
        database = "healthfitness";
        uid = "root";
        password = "tintin";
        string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";

        connection = new MySqlConnection(connectionString);
    }

    // Open Connection
    public bool OpenConnection()
    {
        try
        {
            connection.Open();
            return true;
        }
        catch (MySqlException )
        {
            // Handle exception
            return false;
        }
    }

    // Close Connection
    public bool CloseConnection()
    {
        try
        {
            connection.Close();
            return true;
        }
        catch (MySqlException )
        {
            // Handle exception
            return false;
        }
    }

    // Get Connection
    public MySqlConnection GetConnection()
    {
        return connection;
    }
}
