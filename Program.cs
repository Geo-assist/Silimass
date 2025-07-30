
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MySilmass
{
    class Book
    {
        public string Title { get; set; }
        public bool IsCheckedOut { get; set; }

        public Book(string title)
        {
            Title = title;
            IsCheckedOut = false;
        }
    }

    class SimpleLibrary
    {
        static List<Book> borrowedBooks = new List<Book>();

        static List<Book> library = new List<Book>()
        {
            new Book("The river and the source"),
            new Book("Blossoms of the savannah"),
            new Book("The Pearl"),
            new Book("The dolls house"),
            new Book("The river between")
        };
        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n The Library Manager");
                Console.WriteLine("1. Add a book");
                Console.WriteLine("2. Remove a book");
                Console.WriteLine("3. Display all available books");
                Console.WriteLine("4. Search for a book");
                Console.WriteLine("5. Borrow a book");
                Console.WriteLine("6. Clear book checkout status");
                Console.WriteLine("7. Quit");
                Console.Write("Choose an option (1-7): ");

                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.WriteLine("Enter book name to add:");
                        string? booktoAdd = Console.ReadLine();
                        AddBook(booktoAdd!);
                        break;

                    case "2":
                        Console.WriteLine("Enter book name to remove:");
                        string? booktoRemove = Console.ReadLine();
                        RemoveBook(booktoRemove!);
                        break;

                    case "3":
                        await FetchBooksAsync();
                        break;

                    case "4":
                        Console.WriteLine("Enter book title to search:");
                        string? bookToSearch = Console.ReadLine();
                        SearchBook(bookToSearch!);
                        break;

                    case "5":
                        Console.WriteLine("Enter book title to borrow:");
                        string? titleToBorrow = Console.ReadLine();
                        BorrowBook(titleToBorrow!);
                        break;

                    case "6":
                        Console.WriteLine("Enter book title to clear checkout flag");
                        string? titleToClear = Console.ReadLine();
                        ClearCheckoutFlag(titleToClear!);
                        break;



                    case "7":
                        Console.WriteLine("Exiting... GoodBye!");
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please choose 1,2,3,4,5,6 or 7");
                        break;
                }
            }

        }


        static void AddBook(string book)
        {
            if (library.Count >= 5)
            {
                Console.WriteLine("Library has reached its maximum capacity of 5 books");
            }
            else
            {
                library.Add(new Book(book));
                Console.WriteLine($"Book '{book}' added to the library.");
            }
        }
        static void RemoveBook(string book)
        {
            var bookToRemove = library.FirstOrDefault(b => b.Title.Equals(book, StringComparison.OrdinalIgnoreCase));
            if (bookToRemove != null)
            {
                library.Remove(bookToRemove);
                // If the book is borrowed, remove it from borrowedBooks as well
                Console.WriteLine($"Book '{book}' removed from the library.");
            }
            else
            {
                Console.WriteLine($"Book '{book}' not found in the library.");
            }

        }

        static async Task FetchBooksAsync()
        {
            Console.WriteLine("\n fetching books from the library...");
            await Task.Delay(2000);
            DisplayBooks();
        }

        static void DisplayBooks()
        {
            Console.WriteLine("\n Available books in the library");
            if (library.Count == 0)
            {
                Console.WriteLine($"There is no book available in the library.");
            }
            else
            {
                for (int i = 0; i < library.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {library[i].Title}");
                }

            }
        }

        static void BorrowBook(string title)
        {
            if (borrowedBooks.Count >= 3)
            {
                Console.WriteLine("You have reached the maximum limit of 3 borrowed books.");
                return;
            }
            var book = library.FirstOrDefault(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (book == null)
            {
                Console.WriteLine($"Book '{title}' not found in the library.");
                return;
            }
            if (book.IsCheckedOut)
            {
                Console.WriteLine($"Book '{title}' is already checked out.");
                return;
            }
            book.IsCheckedOut = true;
            borrowedBooks.Add(book);

            Console.WriteLine($" '{title}' has been checked out. You have borrowed {borrowedBooks.Count} book(s).");
        }

        static void ClearCheckoutFlag(string title)
        {
            var book = library.FirstOrDefault(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (book == null)
            {
                Console.WriteLine($"Book '{title}' not found in the library.");
                return;
            }
            else if (!book.IsCheckedOut)
            {
                Console.WriteLine($"Book '{title}' is not currently checked out.");
                return;
            }
            book.IsCheckedOut = false;
            borrowedBooks.Remove(book);
            Console.WriteLine($" '{title}' has been marked as returned.");
        }

        static void SearchBook(string book)
        {
            var bookFound = library.FirstOrDefault(b => b.Title.Equals(book, StringComparison.OrdinalIgnoreCase));
            if (bookFound != null)
            {
                string status = bookFound.IsCheckedOut ? "checked out" : "available";
                Console.WriteLine($"Book '{bookFound.Title}' is {status} in the library.");

            }
            else
            {
                Console.WriteLine($"Book '{book}' not in the library.");
            }
        }
    }
}

