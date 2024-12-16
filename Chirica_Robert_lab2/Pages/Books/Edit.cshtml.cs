﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Chirica_Robert_lab2.Models;

namespace Chirica_Robert_lab2.Pages.Books
{
    public class EditModel : BookCategoriesPageModel
    {
        private readonly Chirica_Robert_lab2.Data.Chirica_Robert_lab2Context _context;

        public EditModel(Chirica_Robert_lab2.Data.Chirica_Robert_lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Book =  await _context.Book
                .Include(b => b.Publisher)
                .Include(b => b.BookCategories).ThenInclude(b => b.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Book == null)
            {
                return NotFound();
            }

            PopulateAssignedCategoryData(_context, Book);

            ViewData["PublisherID"] = new SelectList(_context.Publisher, "ID", "PublisherName");
            ViewData["AuthorID"] = new SelectList(_context.Set<Author>(), "ID", "FullName");

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCategories)
        {
            if (id == null) { 
                return NotFound(); 
            }
            
            var bookToUpdate = await _context.Book                
                .Include(i => i.Publisher)                 
                .Include(i => i.BookCategories).ThenInclude(i => i.Category)                 
                .FirstOrDefaultAsync(s => s.ID == id);             
            
            if (bookToUpdate == null) {                 
                return NotFound();             
            }

            //se va modifica AuthorID  conform cu sarcina de la lab 2
            if (await TryUpdateModelAsync<Book>(
                bookToUpdate, 
                "Book", 
                i => i.Title, 
                i => i.Author,                 
                i => i.Price, 
                i => i.PublishingDate, 
                i => i.PublisherID)) {                 
                UpdateBookCategories(_context, selectedCategories, bookToUpdate);                 
                await _context.SaveChangesAsync();                 
                return RedirectToPage("./Index");             
            }

            UpdateBookCategories(_context, selectedCategories, bookToUpdate); 
            PopulateAssignedCategoryData(_context, bookToUpdate); 
            return Page();
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.ID == id);
        }
    }
}
