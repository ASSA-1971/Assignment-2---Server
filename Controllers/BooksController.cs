using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YourApp.Data;
using YourApp.Models;

namespace YourApp.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // list all books with their author
        public async Task<IActionResult> Index()
        {
            var books = await _context.Books
                .Include(b => b.Author)
                .ToListAsync();
            return View(books);
        }

        // show blank create form with author dropdown
        public IActionResult Create()
        {
            ViewBag.authorId = buildAuthorDropdown();
            return View();
        }

        // save new book
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("title,genre,price,publicationYear,authorId")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.authorId = buildAuthorDropdown(book.authorId);
            return View(book);
        }

        // show edit form with existing book and author dropdown
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound();

            ViewBag.authorId = buildAuthorDropdown(book.authorId);
            return View(book);
        }

        // save updated book
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("bookId,title,genre,price,publicationYear,authorId")] Book book)
        {
            if (id != book.bookId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Books.Any(b => b.bookId == book.bookId))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.authorId = buildAuthorDropdown(book.authorId);
            return View(book);
        }

        // confirm then delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var book = await _context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.bookId == id);
            if (book == null) return NotFound();

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // builds a dropdown list showing each author's full name
        private SelectList buildAuthorDropdown(int selectedId = 0)
        {
            var items = _context.Authors
                .Select(a => new SelectListItem
                {
                    Value = a.authorId.ToString(),
                    Text = a.firstName + " " + a.lastName
                })
                .ToList();

            return new SelectList(items, "Value", "Text", selectedId == 0 ? (object?)null : selectedId.ToString());
        }
    }
}
