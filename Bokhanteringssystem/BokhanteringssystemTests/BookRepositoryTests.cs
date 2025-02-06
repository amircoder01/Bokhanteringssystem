using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bokhanteringssystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokhanteringssystem.Tests
{
    [TestClass()]
    public class BookRepositoryTests
    {

        [TestMethod()]
        public void AddBookTest()
        {
            // Arrange
            string testdb = "Data Source=test.db;Version=3;";
            BookRepository bookRepository = new BookRepository(testdb);
            Book book = new Book("Test", "Test", 2021);
            // Act
            bookRepository.AddBook(book);
            var books = bookRepository.GetAllBooks();
            // Assert
            Assert.IsTrue(books.Any(b => b.Title == "Test"));

            var testBook = books.First(b => b.Title == "Test");
            if(testBook != null)
            {
                bookRepository.DeleteBook(testBook.Id);
            }
        }

        [TestMethod()]
        public void GetAllBooksTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetBookByIdTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void UpdateBookTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteBookTest()
        {
            Assert.Fail();
        }
    }
}