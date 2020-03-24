using System;
using System.Linq;
using System.Threading.Tasks;
using BlogBlog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BlogBlog.Controllers
{
    public class BlogTablesController : Controller
    {
        private readonly BlogContext _context;

        public BlogTablesController(BlogContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            ViewData["CurrentFilter"] = searchString;
            ViewData["DateSort"] = sortOrder == "Date" ? "date_desc" : "Date";

            var entries = from h in _context.BlogTable
                select h;

            switch (sortOrder)
            {
                case "date_desc":
                    entries = entries.OrderByDescending(s => s.BlogDateTime);
                    break;
                case "Date":
                    entries = entries.OrderBy(s => s.BlogDateTime);
                    break;
            }

            if (!string.IsNullOrEmpty(searchString))
                entries = entries.Where(s =>
                    s.BlogHeadline.Contains(searchString) || s.Cat.CatName.Contains(searchString));

            //var blogContext = _context.BlogTable.Include(b => b.Cat);
            return View(await entries.AsNoTracking().ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var blogTable = await _context.BlogTable
                .Include(b => b.Cat)
                .FirstOrDefaultAsync(m => m.BlogId == id);
            if (blogTable == null) return NotFound();

            return View(blogTable);
        }

        // GET: BlogTables/Create
        public IActionResult Create()
        {
            ViewData["CatId"] = new SelectList(_context.Categories, "CatId", "CatName");
            return View();
        }

        // POST: BlogTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BlogId,BlogHeadline,BlogEntry,BlogDateTime,CatId")]
            BlogTable blogTable)
        {
            if (ModelState.IsValid)
            {
                blogTable.BlogDateTime = DateTime.Now;
                _context.Add(blogTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CatId"] = new SelectList(_context.Categories, "CatId", "CatName", blogTable.CatId);
            return View(blogTable);
        }
    }
}