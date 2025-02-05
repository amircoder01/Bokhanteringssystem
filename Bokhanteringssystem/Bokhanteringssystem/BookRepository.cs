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
                    command.CommandText = "INSERT INTO Books (Title, Author, Year, Genre) VALUES (@Title, @Author, @Year, @Genre)";
                    command.Parameters.AddWithValue("@Title", book.Title);
                    command.Parameters.AddWithValue("@Author", book.Author);
                    command.Parameters.AddWithValue("@Year", book.Year);
                    command.Parameters.AddWithValue("@Genre", book.Genre);
                    command.ExecuteNonQuery();
                }
            }
        }
        public void GetAllBooks()
        {
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
                            Console.WriteLine($"{reader["Id"]} {reader["Title"]} {reader["Author"]} {reader["Year"]} {reader["Genre"]}");
                        }
                    }
                }
            }
        }
        public void GetBookById(int id)
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
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["Id"]} {reader["Title"]} {reader["Author"]} {reader["Year"]} {reader["Genre"]}");
                        }
                    }
                }
            }
        }
        public void UpdateBook(Book book)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE Books SET Title = @Title, Author = @Author, Year = @Year, Genre = @Genre WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", book.Id);
                    command.Parameters.AddWithValue("@Title", book.Title);
                    command.Parameters.AddWithValue("@Author", book.Author);
                    command.Parameters.AddWithValue("@Year", book.Year);
                    command.Parameters.AddWithValue("@Genre", book.Genre);
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
