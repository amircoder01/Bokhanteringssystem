using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokhanteringssystem
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public Book(int id, string title, string author, int year)
        {
            Id = id;
            Title = title;
            Author = author;
            Year = year;
        }
        public Book(string title, string author, int year)
        {
            Title = title;
            Author = author;
            Year = year;
        }
        public void Validering()
        {
            if (string.IsNullOrEmpty(Title))
            {
                throw new ArgumentException("Title is required");
            }
            if (string.IsNullOrEmpty(Author))
            {
                throw new ArgumentException("Author is required");
            }
            if (Year < 0 || Year > 2025)
            {
                throw new ArgumentException("Year must be between 0 and 2025");
            }

        }
        public override string ToString()
        {
            return $"{Id} {Title} {Author} {Year}";
        }

    }

}
