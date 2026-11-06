using System.Data;
using Domain;
using Microsoft.Data.SqlClient;

namespace Infrastructure;

public class TodoRepository(Database database) : ITodoRepository
{
    private Database _database = database;

    public Todo? GetById(int id)
    {
        using var connection = _database.GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = $"SELECT * FROM Items WHERE Id = @Id";
        command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
        var reader = command.ExecuteReader();
        return !reader.Read() ? null : Map(reader);
    }

    public List<Todo> GetAll()
    {
        using var connection = _database.GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = $"SELECT * FROM Items";

        var list = new List<Todo>();

        using var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

        while (reader.Read())
            list.Add(Map(reader));

        return list;
    }

    public Todo Create(Todo todo)
    {
        using var connection = _database.GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = $"INSERT INTO Items(Title, Done) OUTPUT INSERTED.Id VALUES(@Title, @Done)";
        command.Parameters.Add("@Title", SqlDbType.VarChar, 200).Value = todo.Title;
        command.Parameters.Add("@Done", SqlDbType.Bit).Value = todo.Done;

        var id = (int)command.ExecuteScalar();

        return new Todo
        {
            Id = id,
            Title = todo.Title,
            Done = todo.Done
        };
    }

    public bool Update(Todo todo)
    {
        using var connection = _database.GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = $"UPDATE Items SET Title = @Title, Done = @Done WHERE Id = @Id";
        command.Parameters.Add("@Title", SqlDbType.VarChar, 200).Value = todo.Title;
        command.Parameters.Add("@Done", SqlDbType.Bit).Value = todo.Done;
        command.Parameters.Add("@Id", SqlDbType.Int).Value = todo.Id;

        return command.ExecuteNonQuery() > 0;
    }

    public bool Delete(int id)
    {
        using var connection = _database.GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = $"DELETE FROM Items WHERE Id = @Id";
        command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
        return command.ExecuteNonQuery() > 0;
    }

    private static Todo Map(SqlDataReader reader) => new()
    {
        Id = reader.GetInt32(reader.GetOrdinal("Id")),
        Title = reader.GetString(reader.GetOrdinal("Title")),
        Done = reader.GetBoolean(reader.GetOrdinal("Done"))
    };
}