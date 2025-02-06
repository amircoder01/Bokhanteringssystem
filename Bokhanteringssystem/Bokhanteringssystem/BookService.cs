using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokhanteringssystem
{
    public class BookService
    {
        private readonly BookRepository _bookRepository;
        private string _connectionString;

        public BookService(BookRepository bookRepository, string connectionString)
        {
            _bookRepository = bookRepository;
            _connectionString = connectionString; // Nu sätts connection-strängen korrekt
        }

        public void AddBook(string title, string author, int year)
        {
            var book = new Book(title, author, year);
            book.Validering();
            _bookRepository.AddBook(book);
        }
        public void GetAllBooks()
        {
            var books = _bookRepository.GetAllBooks(); // Hämtar listan med böcker

            foreach (var book in books) // Skriver ut böckerna här istället
            {
                Console.WriteLine($"{book.Id} {book.Title} {book.Author} {book.Year} ");
            }
        }
        public void TestConnection()
        {
            _bookRepository.TestConnection();
        }
        public void GetBookById(int id)
        {
            var book = _bookRepository.GetBookById(id);

            if (book != null)
            {
                Console.WriteLine($"{book.Id} {book.Title} {book.Author} {book.Year} ");
            }
            else
            {
                Console.WriteLine("❌ Boken hittades inte.");
            }
        }

        public void UpdateBook(int id, string title, string author, int year)
        {
            var book = new Book(id, title, author, year);
            book.Validering();
            _bookRepository.UpdateBook(book);
        }
        public void DeleteBook(int id)
        {
            _bookRepository.DeleteBook(id);
        }
        public void SeedDatabase()
        {
            using (var connection = new SQLiteConnection(_connectionString)) // 🟢 Nu används rätt connectionString
            {
                connection.Open();
                var checkCommand = new SQLiteCommand("SELECT COUNT(*) FROM Books", connection);
                long count = (long)checkCommand.ExecuteScalar();

                if (count == 0)
                {
                    var insertCommand = new SQLiteCommand(connection);
                    insertCommand.CommandText = @"
                    INSERT INTO Books (Title, Author, Year) VALUES 
                    ('1984', 'George Orwell', 1949),
                    ('Brave New World', 'Aldous Huxley', 1932),
                    ('To Kill a Mockingbird', 'Harper Lee', 1960),
                    ('The Great Gatsby', 'F. Scott Fitzgerald', 1925),
                    ('Lord of the rings', 'J.R.R. Tolkien', 1954),
                    ('The Catcher in the Rye', 'J.D. Salinger', 1951);
                    ";
                    insertCommand.ExecuteNonQuery();
                    Console.WriteLine("📚 Standardböcker har lagts till i databasen.");
                }
            }
        }
    }
}
