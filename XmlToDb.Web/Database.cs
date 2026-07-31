// Хранит загруженные xml-файлы в базе SQLite: сам файл кладётся в поле типа BLOB.

using Microsoft.Data.Sqlite;

public class DocumentInfo
{
    public long Id;
    public string FileName;
    public string LoadDate;
}

public static class Database
{
    private static string _connectionString;

    public static void Init(string folder)
    {
        Directory.CreateDirectory(folder);
        _connectionString = "Data Source=" + Path.Combine(folder, "XmlToDb.db");

        using var connection = Open();
        var command = connection.CreateCommand();
        command.CommandText =
            @"CREATE TABLE IF NOT EXISTS Documents (
                  Id       INTEGER PRIMARY KEY AUTOINCREMENT,
                  FileName TEXT NOT NULL,
                  LoadDate TEXT NOT NULL,
                  Content  BLOB NOT NULL);";
        command.ExecuteNonQuery();
    }

    public static void AddDocument(string fileName, byte[] content)
    {
        using var connection = Open();
        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Documents (FileName, LoadDate, Content) VALUES ($name, $date, $content);";
        command.Parameters.AddWithValue("$name", fileName);
        command.Parameters.AddWithValue("$date", DateTime.Now.ToString("dd.MM.yyyy HH:mm"));
        command.Parameters.AddWithValue("$content", content);
        command.ExecuteNonQuery();
    }

    public static List<DocumentInfo> GetDocuments()
    {
        var documents = new List<DocumentInfo>();

        using var connection = Open();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT Id, FileName, LoadDate FROM Documents ORDER BY Id DESC;";

        using var reader = command.ExecuteReader();
        while (reader.Read())
            documents.Add(new DocumentInfo
            {
                Id = reader.GetInt64(0),
                FileName = reader.GetString(1),
                LoadDate = reader.GetString(2)
            });

        return documents;
    }

    public static (string FileName, byte[] Content) GetDocument(long id)
    {
        using var connection = Open();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT FileName, Content FROM Documents WHERE Id = $id;";
        command.Parameters.AddWithValue("$id", id);

        using var reader = command.ExecuteReader();
        return reader.Read() ? (reader.GetString(0), (byte[])reader["Content"]) : (null, null);
    }

    private static SqliteConnection Open()
    {
        var connection = new SqliteConnection(_connectionString);
        connection.Open();
        return connection;
    }
}
