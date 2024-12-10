using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations;

public class BookService(ApplicationDbContext context) : IBookService
{
    public async Task<Book> GetBook()
    {
        return await context.Books
            .OrderByDescending(b => b.Price * b.QuantityPublished)
            .FirstOrDefaultAsync();
    }

    public async Task<List<Book>> GetBooks()
    {
        var albumSabatonPublishDate = new DateTime(2012, 5, 25); // в wiki две разные даты (25 мая и 22 мая)
        
        return await context.Books
            .Where(b => b.Title.Contains("Red") && b.PublishDate > albumSabatonPublishDate)
            .ToListAsync();
    }
}