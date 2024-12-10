using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations;

public class AuthorService(ApplicationDbContext context) : IAuthorService
{
    public async Task<Author> GetAuthor()
    {
        return await context.Books
            .OrderByDescending(b => b.Title.Length)
            .ThenBy(b => b.AuthorId)
            .Select(b => b.Author)
            .FirstOrDefaultAsync();
    }

    public async Task<List<Author>> GetAuthors()
    {
        return await context.Authors
            .Where(a => a.Books.Count(b => b.PublishDate.Year > 2015) % 2 == 0)
            .ToListAsync();
    }
}