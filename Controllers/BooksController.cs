using BookMvcApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookMvcApp.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookService _bookService;

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetBooksAsync();
            return View(books);
        }

        public async Task<IActionResult> Authors()
        {
            var authors = await _bookService.GetAuthorsAsync();
            return View(authors);
        }
    }
}