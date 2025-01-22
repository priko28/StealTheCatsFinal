using Microsoft.EntityFrameworkCore;
using StealTheCats.Core.Repositories;
using StealTheCats.Data.Context;
using StealTheCats.Data.Models;

public class CatRepository(CatDbContext context) : ICatRepository
{
    private readonly CatDbContext _context = context;

    // Add a new cat to the database if it doesnt exist.
    public async Task AddCatAsync(CatEntity cat)
    {
        if (!await _context.Cats.AnyAsync(c => c.CatId == cat.CatId))
        {
            _context.Cats.Add(cat);
        }
    }

    // Get a specific cat by ID including tags.
    public async Task<CatEntity?> GetCatByIdAsync(string id)
    {
        return await _context.Cats
            .Include(c => c.CatTags)
            .ThenInclude(ct => ct.Tag)
            .FirstOrDefaultAsync(c => c.Id.Equals(id));
    }

    // Get a list of all cats.
    public async Task<List<CatEntity>> GetCatsAsync(int page, int pageSize)
    {
        return await _context.Cats
            .Include(c => c.CatTags)
            .ThenInclude(ct => ct.Tag)
            .OrderBy(c => c.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    // Get a list of cats filtered by a specific tag.
    public async Task<List<CatEntity>> GetCatsByTagAsync(string tag, int page, int pageSize)
    {
        return await _context.Cats
            .Include(c => c.CatTags)
            .ThenInclude(ct => ct.Tag)
            .Where(c => c.CatTags.Any(ct => ct.Tag.Name == tag))
            .OrderBy(c => c.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    // Get a tag by its name.
    public async Task<TagEntity?> GetTagByNameAsync(string name)
    {
        return await _context.Tags.FirstOrDefaultAsync(t => t.Name == name);
    }

    // Add a new tag to the database.
    public async Task AddTagAsync(TagEntity tag)
    {
        if (!await _context.Tags.AnyAsync(t => t.Name == tag.Name))
        {
            _context.Tags.Add(tag);
        }
    }

    // Save changes to the database.
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
