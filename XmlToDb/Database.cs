// Хранит загруженные xml-файлы во встроенной базе SQLite: сам файл кладётся в поле типа BLOB.

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace XmlToDb
{
    internal class DocumentInfo
    {
        public long Id;
        public string FileName;
        public string LoadDate;

        public override string ToString()
        {
            return FileName + "   (" + LoadDate + ")";
        }
    }

    internal static class Database
    {
        private static readonly string DbPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "XmlToDb", "XmlToDb.db");

        public static void Init()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(DbPath));

            using (var connection = Open())
            using (var command = connection.CreateCommand())
            {
                command.CommandText =
                    @"CREATE TABLE IF NOT EXISTS Documents (
                          Id       INTEGER PRIMARY KEY AUTOINCREMENT,
                          FileName TEXT NOT NULL,
                          LoadDate TEXT NOT NULL,
                          Content  BLOB NOT NULL);";
                command.ExecuteNonQuery();
            }
        }

        public static void AddDocument(string fileName, byte[] content)
        {
            using (var connection = Open())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO Documents (FileName, LoadDate, Content) VALUES (@name, @date, @content);";
                command.Parameters.AddWithValue("@name", fileName);
                command.Parameters.AddWithValue("@date", DateTime.Now.ToString("dd.MM.yyyy HH:mm"));
                command.Parameters.Add("@content", DbType.Binary).Value = content;
                command.ExecuteNonQuery();
            }
        }

        public static List<DocumentInfo> GetDocuments()
        {
            var documents = new List<DocumentInfo>();

            using (var connection = Open())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT Id, FileName, LoadDate FROM Documents ORDER BY Id DESC;";

                using (var reader = command.ExecuteReader())
                    while (reader.Read())
                        documents.Add(new DocumentInfo
                        {
                            Id = reader.GetInt64(0),
                            FileName = reader.GetString(1),
                            LoadDate = reader.GetString(2)
                        });
            }

            return documents;
        }

        public static byte[] GetContent(long id)
        {
            using (var connection = Open())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT Content FROM Documents WHERE Id = @id;";
                command.Parameters.AddWithValue("@id", id);
                return (byte[])command.ExecuteScalar();
            }
        }

        private static SQLiteConnection Open()
        {
            var connection = new SQLiteConnection("Data Source=" + DbPath + ";Version=3;");
            connection.Open();
            return connection;
        }
    }
}
