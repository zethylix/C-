using System.Data;
using Domain;
using Microsoft.Data.SqlClient;

namespace Infrastructure;

public class TodoRepository(Database database) : ITodoRepository
{
    private Database _database = database;

    public async Task<Todo?> GetById(int id)
    {
        await using var connection = _database.GetConnection();
        await connection.OpenAsync();
        var command = connection.CreateCommand();
        command.CommandText = $"SELECT * FROM Items WHERE Id = @Id";
        command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
        var reader = await command.ExecuteReaderAsync();
        var hasData = await reader.ReadAsync();
        return !hasData ? null : Map(reader);
    }

    public async Task<List<Todo>> GetAll()
    {
        await using var connection = _database.GetConnection();
        await connection.OpenAsync();
        var command = connection.CreateCommand();
        command.CommandText = $"SELECT * FROM Items";

        var list = new List<Todo>();

        await using var reader = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);

        while (reader.Read())
            list.Add(Map(reader));

        return list;
    }

    public async Task<Todo> Create(Todo todo)
    {
        await using var connection = _database.GetConnection();
        await connection.OpenAsync();
        var command = connection.CreateCommand();
        command.CommandText = $"INSERT INTO Items(Title, Done) OUTPUT INSERTED.Id VALUES(@Title, @Done)";
        command.Parameters.Add("@Title", SqlDbType.VarChar, 200).Value = todo.Title;
        command.Parameters.Add("@Done", SqlDbType.Bit).Value = todo.Done;

        var id = await command.ExecuteScalarAsync();

        return new Todo
        {
            Id = (int) id!,
            Title = todo.Title,
            Done = todo.Done
        };
    }

    public async Task<bool> Update(Todo todo)
    {
        await using var connection = _database.GetConnection();
        await connection.OpenAsync();
        var command = connection.CreateCommand();
        command.CommandText = $"UPDATE Items SET Title = @Title, Done = @Done WHERE Id = @Id";
        command.Parameters.Add("@Title", SqlDbType.VarChar, 200).Value = todo.Title;
        command.Parameters.Add("@Done", SqlDbType.Bit).Value = todo.Done;
        command.Parameters.Add("@Id", SqlDbType.Int).Value = todo.Id;

        return await command.ExecuteNonQueryAsync() > 0;
    }

    public async Task<bool> Delete(int id)
    {
        await using var connection = _database.GetConnection();
        await connection.OpenAsync();
        var command = connection.CreateCommand();
        command.CommandText = $"DELETE FROM Items WHERE Id = @Id";
        command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
        return await command.ExecuteNonQueryAsync() > 0;
    }

    private static Todo Map(SqlDataReader reader) => new()
    {
        Id = reader.GetInt32(reader.GetOrdinal("Id")),
        Title = reader.GetString(reader.GetOrdinal("Title")),
        Done = reader.GetBoolean(reader.GetOrdinal("Done"))
    };
}