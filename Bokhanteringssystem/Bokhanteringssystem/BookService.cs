using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokhanteringssystem
{
    public class BookService
    {
        private readonly BookRepository _bookRepository;

        public BookService(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public void AddBook(string title, string author, int year, string genre)
        {
            var book = new Book(0, title, author, year, genre);
            book.Validering();
            _bookRepository.AddBook(book);
        }
        public void GetAllBooks()
        {
            _bookRepository.GetAllBooks();
        }
        public void TestConnection()
        {
            _bookRepository.TestConnection();
        }
        public void GetBookById(int id)
        {
            _bookRepository.GetBookById(id);
        }
        public void UpdateBook(int id, string title, string author, int year, string genre)
        {
            var book = new Book(id, title, author, year, genre);
            book.Validering();
            _bookRepository.UpdateBook(book);
        }
        public void DeleteBook(int id)
        {
            _bookRepository.DeleteBook(id);
        }
    }
}
