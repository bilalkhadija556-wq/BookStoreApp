
using Microsoft.AspNetCore.Mvc;
using BookStoreApp.Models;
using BookStoreApp.ViewModels;

namespace BookStoreApp.Controllers
{
    public class HomeController : Controller
    {
        // List of authors
        static List<Author> authors = new List<Author>()
        {
            new Author { Id = 1, Name = "Ali", Country = "Pakistan" },
            new Author { Id = 2, Name = "John", Country = "USA" }
        };
        // List of books
        static List<Book> books = new List<Book>()
        {
            new Book { Id = 1, Title = "C# Basics", Price = 500, AuthorId = 1 },
            new Book { Id = 2, Title = "ASP.NET Core", Price = 800, AuthorId = 1 },
            new Book { Id = 3, Title = "Clean Code", Price = 1000, AuthorId = 2 }
        };
        // Home Page
        public IActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();

            model.Authors = authors;

            foreach (Book book in books)
            {
                string authorName = "Unknown";

                foreach (Author author in authors)
                {
                    if (author.Id == book.AuthorId)
                    {
                        authorName = author.Name;
                    }
                }

                BookCardViewModel bookCard = new BookCardViewModel();

                bookCard.Id = book.Id;
                bookCard.Title = book.Title;
                bookCard.Price = book.Price;
                bookCard.AuthorName = authorName;

                model.Books.Add(bookCard);
            }
            return View(model);
        }
        // About Page
        public IActionResult About()
        {
            return View();
        }

        // Contact Page
        public IActionResult Contact()
        {
            return View();
        }
        // Add Book
        [HttpPost]
        public IActionResult AddBook(string title, decimal price, int authorId)
        {
            int newId = 1;

            foreach (Book book in books)
            {
                if (book.Id >= newId)
                {
                    newId = book.Id + 1;
                }
            }
            Book newBook = new Book();
            newBook.Id = newId;
            newBook.Title = title;
            newBook.Price = price;
            newBook.AuthorId = authorId;
            books.Add(newBook);

            return RedirectToAction("Index");
        }
        // Add Author
        [HttpPost]
        public IActionResult AddAuthor(string name, string country)
        {
            int newId = 1;

            foreach (Author author in authors)
            {
                if (author.Id >= newId)
                {
                    newId = author.Id + 1;
                }
            }

            Author newAuthor = new Author();

            newAuthor.Id = newId;
            newAuthor.Name = name;
            newAuthor.Country = country;

            authors.Add(newAuthor);

            return RedirectToAction("Index");
        }
        // Remove Book
        [HttpPost]
        public IActionResult RemoveBook(int id)
        {
            Book bookToRemove = null;
            foreach (Book book in books)
            {
                if (book.Id == id)
                {
                    bookToRemove = book;
                }
            }
            if (bookToRemove != null)
            {
                books.Remove(bookToRemove);
            }
            return RedirectToAction("Index");
        }
    }
}
