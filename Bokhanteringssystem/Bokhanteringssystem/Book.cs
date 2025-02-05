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
        public string Genre { get; set; }
        public Book(int id, string title, string author, int year, string genre)
        {
            Id = id;
            Title = title;
            Author = author;
            Year = year;
            Genre = genre;
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
            if (Year > 2025)
            {
                throw new ArgumentException("Year must be a positive number");
            }
        }
        public string ToStrig()
        {
            return $"{Id} {Title} {Author} {Year} {Genre}";
        }
    }

}
