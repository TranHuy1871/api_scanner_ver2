using Dapper;
using MySqlConnector;

namespace ApiCamScanner.MySql;

public class ExecuteSql
{
    private readonly string _connectionString;

    public ExecuteSql(string connectionString)
    {
        _connectionString = connectionString;
    }

    public T ExecuteScalar<T>(string query, object parameters = null)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            return connection.ExecuteScalar<T>(query, parameters);
        }
    }

    public IEnumerable<T> Query<T>(string query, object parameters = null)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            return connection.Query<T>(query, parameters);
        }
    }

    public T CheckAuthenticateUser<T>(string query, object parameters = null)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            return connection.QueryFirstOrDefault<T>(query, parameters);
        }
    }

    public bool Execute(string query, object parameters = null)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            int rowsAffected = connection.Execute(query, parameters);
            return rowsAffected > 0;
        }
    }

    public bool CheckExists(string query, object parameters = null)
    {
        var count = ExecuteScalar<int>(query, parameters);
        return count > 0;
    }
}
