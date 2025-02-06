using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokhanteringssystem
{
    public class BookRepository
    {
        private readonly string _connectionString;

        public BookRepository(string connectionString)
        {
            _connectionString = connectionString;
            InitializeDatabase();
        }
        private void InitializeDatabase()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                // Skapa en ny tabell utan Genre
                var createTableCommand = new SQLiteCommand(@"
        CREATE TABLE IF NOT EXISTS Books (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Title TEXT NOT NULL,
            Author TEXT NOT NULL,
            Year INTEGER NOT NULL
        );", connection);
                createTableCommand.ExecuteNonQuery();
            }
        }


        public void TestConnection()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                try
                {
                    connection.Open(); // Öppna anslutningen
                    Console.WriteLine("✅ Anslutning till databasen lyckades!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Fel vid anslutning: {ex.Message}");
                }
                finally
                {
                    connection.Close(); // Stäng anslutningen
                }
            }
        }

        public void AddBook(Book book)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO Books (Title, Author, Year) VALUES (@Title, @Author, @Year)";
                    command.Parameters.AddWithValue("@Title", book.Title);
                    command.Parameters.AddWithValue("@Author", book.Author);
                    command.Parameters.AddWithValue("@Year", book.Year);
                    command.ExecuteNonQuery();
                }
            }
        }
        public List<Book> GetAllBooks()
        {
            var books = new List<Book>();

            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Books";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            books.Add(new Book(
                                Convert.ToInt32(reader["Id"]),
                                reader["Title"].ToString(),
                                reader["Author"].ToString(),
                                Convert.ToInt32(reader["Year"])
                            ));
                        }
                    }
                }
            }
            return books; // Returnerar listan istället för att skriva ut direkt
        }

        public Book GetBookById(int id)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Books WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read()) // Om vi hittar en bok, returnera den
                        {
                            return new Book(
                                Convert.ToInt32(reader["Id"]),
                                reader["Title"].ToString(),
                                reader["Author"].ToString(),
                                Convert.ToInt32(reader["Year"])
                            );
                        }
                    }
                }
            }
            return null; // Om ingen bok hittas, returnera null
        }

        public void UpdateBook(Book book)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE Books SET Title = @Title, Author = @Author, Year = @Year WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", book.Id);
                    command.Parameters.AddWithValue("@Title", book.Title);
                    command.Parameters.AddWithValue("@Author", book.Author);
                    command.Parameters.AddWithValue("@Year", book.Year);
                    command.ExecuteNonQuery();
                }
            }
        }
        public void DeleteBook(int id)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM Books WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
