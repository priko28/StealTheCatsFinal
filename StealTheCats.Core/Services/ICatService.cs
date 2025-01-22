using StealTheCats.Data.Models;

namespace StealTheCats.Core.Services
{
    public interface ICatService
    {
        Task FetchAndSaveCatsAsync();
        Task<CatEntity?> GetCatByIdAsync(string id);
        Task<List<CatEntity>> GetCatsAsync(int page, int pageSize);
        Task<List<CatEntity>> GetCatsByTagAsync(string tag, int page, int pageSize);
    }
}
