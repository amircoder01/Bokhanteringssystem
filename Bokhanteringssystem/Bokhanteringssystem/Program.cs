using System.ComponentModel.Design;
using System.Data.SQLite;
using System.Security.Cryptography.X509Certificates;

namespace Bokhanteringssystem
{
    internal class Program
    {
        static string _connectionString = "Data Source=books.db;Version=3;";
        static BookRepository bookRepository = new BookRepository(_connectionString);
        static BookService bookService = new BookService(bookRepository, _connectionString);

        static void Main(string[] args)
        {
            InitializeDatabase();
            while (true) // Loop för att hålla menyn igång
            {
                Menu();
            }
        }

        static void Menu()
        {
            Console.WriteLine("\n--- Bokhanteringssystem ---");
            Console.WriteLine("1. Lägg till bok");
            Console.WriteLine("2. Visa alla böcker");
            Console.WriteLine("3. Visa bok med ID");
            Console.WriteLine("4. Uppdatera bok");
            Console.WriteLine("5. Ta bort bok");
            Console.WriteLine("6. Avsluta");
            Console.Write("Välj ett alternativ: ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        AddBook();
                        break;
                    case 2:
                        bookService.GetAllBooks();
                        break;
                    case 3:
                        GetBookById();
                        break;
                    case 4:
                        UpdateBook();
                        break;
                    case 5:
                        DeleteBook();
                        break;
                    case 6:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Ogiltigt val. Försök igen.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Ange ett giltigt nummer.");
            }
        }

        static void AddBook()
        {
            Console.Write("Titel: ");
            string title = Console.ReadLine();
            Console.Write("Författare: ");
            string author = Console.ReadLine();

            // Validera att året är ett giltigt heltal
            Console.Write("Utgivningsår: ");
            if (int.TryParse(Console.ReadLine(), out int year))
            {
                bookService.AddBook(title, author, year);
                Console.WriteLine("Bok har lagts till!");
            }
            else
            {
                Console.WriteLine("❌ Felaktigt år. Ange ett giltigt heltal.");
            }
        }


        static void GetBookById()
        {
            Console.Write("Ange bok-ID: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                bookService.GetBookById(id);
            }
            else
            {
                Console.WriteLine("Ogiltigt ID.");
            }
        }

        static void UpdateBook()
        {
            Console.Write("Ange bok-ID att uppdatera: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Console.Write("Ny titel: ");
                string title = Console.ReadLine();
                Console.Write("Ny författare: ");
                string author = Console.ReadLine();

                // Validera att året är ett giltigt heltal
                Console.Write("Nytt utgivningsår: ");
                if (int.TryParse(Console.ReadLine(), out int year))
                {
                    bookService.UpdateBook(id, title, author, year);
                    Console.WriteLine("Bok har uppdaterats!");
                }
                else
                {
                    Console.WriteLine("❌ Felaktigt år. Ange ett giltigt heltal.");
                }
            }
            else
            {
                Console.WriteLine("❌ Ogiltigt ID.");
            }
        }


        static void DeleteBook()
        {
            Console.Write("Ange bok-ID att ta bort: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                bookService.DeleteBook(id);
                Console.WriteLine("Bok har tagits bort!");
            }
            else
            {
                Console.WriteLine("Ogiltigt ID.");
            }
        }
        static void InitializeDatabase()
        {
            Console.WriteLine("🔄 Initierar databasen...");
            bookService.TestConnection(); // Kollar att databasen finns
            bookService.SeedDatabase();   // Lägg till böcker om databasen är tom
        }
    }
}
