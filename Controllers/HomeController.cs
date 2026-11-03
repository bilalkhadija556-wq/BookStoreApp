using Microsoft.AspNetCore.Mvc;
using BookStoreApp.Models;
using BookStoreApp.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace BookStoreApp.Controllers
{
    public class HomeController : Controller
    {
        // In-memory list of authors
        public static List<Author> Authors = new List<Author>
        {
            new Author
            {
                Id = 1,
                Name = "J.K. Rowling",
                Country = "United Kingdom"
            },

            new Author
            {
                Id = 2,
                Name = "Robert Kiyosaki",
                Country = "United States"
            }
        };

        // In-memory list of books
        public static List<Book> Books = new List<Book>
        {
            new Book
            {
                Id = 1,
                Title = "Harry Potter",
                Price = 1500,
                AuthorId = 1
            },

            new Book
            {
                Id = 2,
                Title = "Rich Dad Poor Dad",
                Price = 1200,
                AuthorId = 2
            },

            new Book
            {
                Id = 3,
                Title = "The Chamber of Secrets",
                Price = 1800,
                AuthorId = 1
            }
        };

        // Home page
        public IActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();

            model.Authors = Authors;

            foreach (Book book in Books)
            {
                Author author = Authors.FirstOrDefault(
                    a => a.Id == book.AuthorId
                );

                BookCardViewModel card = new BookCardViewModel();

                card.Id = book.Id;
                card.Title = book.Title;
                card.Price = book.Price;

                if (author != null)
                {
                    card.AuthorName = author.Name;
                }

                model.Books.Add(card);
            }

            return View(model);
        }

        // Add Book
        [HttpPost]
        public IActionResult AddBook(string Title, decimal Price, int AuthorId)
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                return RedirectToAction("Index");
            }

            int newId = 1;

            if (Books.Count > 0)
            {
                newId = Books.Max(b => b.Id) + 1;
            }

            Book newBook = new Book();

            newBook.Id = newId;
            newBook.Title = Title;
            newBook.Price = Price;
            newBook.AuthorId = AuthorId;

            Books.Add(newBook);

            return RedirectToAction("Index");
        }

        // Add Author
        [HttpPost]
        public IActionResult AddAuthor(string Name, string Country)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                return RedirectToAction("Index");
            }

            int newId = 1;

            if (Authors.Count > 0)
            {
                newId = Authors.Max(a => a.Id) + 1;
            }

            Author newAuthor = new Author();

            newAuthor.Id = newId;
            newAuthor.Name = Name;
            newAuthor.Country = Country;

            Authors.Add(newAuthor);

            return RedirectToAction("Index");
        }

        // Remove Book
        [HttpPost]
        public IActionResult RemoveBook(int id)
        {
            Book book = Books.FirstOrDefault(b => b.Id == id);

            if (book != null)
            {
                Books.Remove(book);
            }

            return RedirectToAction("Index");
        }

        // About page
        public IActionResult About()
        {
            return View();
        }

        // Contact page
        public IActionResult Contact()
        {
            return View();
        }
    }
}
