using uc13_web_exercicio.Global;
using Npgsql;

namespace uc13_web_exercicio.Database
{
    public class AccessDb
    {
        Config config = new Config();

        public NpgsqlConnection OpenConnection()
        {
            string connectionString = String.Format
                ("Server = {0}; User Id = {1}; Database = {2}; Port = {3}; Password = {4};",
                Config.dbHost, Config.dbUser, Config.dbName, Config.dbPort, Config.dbPass);

            NpgsqlConnection connection = new NpgsqlConnection(connectionString);

            connection.Open();

            return connection;
        }
    }
}
