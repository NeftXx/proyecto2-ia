using Mono.Data.Sqlite;
using System.Data;
using UnityEngine;

namespace Util
{
  public class ConnectionDatabase
  {
    private IDbConnection connection;
    private IDbCommand command;
    private IDbTransaction transaction;
    private IDataReader reader;

    public ConnectionDatabase()
    {
      string path = string.Format("URI=file:{0}", Configuration.Properties.DatabasePath);
      connection = new SqliteConnection(path);
    }

    protected IDataReader ExecuteQuery(string query)
    {
      connection.Open();
      command = connection.CreateCommand();
      command.CommandText = query;
      reader = command.ExecuteReader();
      return reader;
    }

    protected int ExecuteNonQuery(string query)
    {
      connection.Open();
      command = connection.CreateCommand();
      command.CommandText = query;
      return command.ExecuteNonQuery();
    }

    protected object ExecuteScalar(string query)
    {
      connection.Open();
      command = connection.CreateCommand();
      command.CommandText = query;
      return command.ExecuteScalar();
    }

    protected void CloseConnection()
    {
      if (reader != null)
      {
        reader.Close();
      }
      command.Dispose();
      connection.Close();
    }
  }
}
