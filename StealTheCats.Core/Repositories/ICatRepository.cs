using StealTheCats.Data.Models;

namespace StealTheCats.Core.Repositories;

public interface ICatRepository
{
    Task AddCatAsync(CatEntity cat);
    Task<CatEntity?> GetCatByIdAsync(string id);
    Task<List<CatEntity>> GetCatsAsync(int page, int pageSize);
    Task<List<CatEntity>> GetCatsByTagAsync(string tag, int page, int pageSize);
    Task<TagEntity?> GetTagByNameAsync(string name);
    Task AddTagAsync(TagEntity tag);
    Task SaveChangesAsync();
}