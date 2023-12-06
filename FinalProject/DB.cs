using MySql.Data.MySqlClient;
using Tmds.DBus.Protocol;

namespace FinalProject;

public class DB
{
    public MySqlConnection _connection =
        new MySqlConnection("Server=sql12.freesqldatabase.com;DataBase=sql12659906;UID=sql12659906;Password=fkfP8wwq9S");

    public void OpenConnection()
    {
        if (_connection.State == System.Data.ConnectionState.Closed)
        {
            _connection.Open();
        }
    }

    public void CloseConnection()
    {
            _connection.Close();
    }

    public MySqlConnection GetConnection()
    {
        return _connection;
    }
}