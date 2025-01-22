using StealTheCats.Core.Dtos;
using StealTheCats.Core.Repositories;
using StealTheCats.Data.Models;
using System.Text.Json;

namespace StealTheCats.Core.Services
{
    public class CatService(
        ICatRepository catRepository) : ICatService
    {
        private readonly ICatRepository _catRepository = catRepository;
        private readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri("https://api.thecatapi.com/v1/") };
        private readonly string _apiKey = Environment.GetEnvironmentVariable("CatApiKey");

        // Fetch cat images
        public async Task FetchAndSaveCatsAsync()
        {
            var catsFromApi = await FetchCatsFromApiAsync();
            foreach (var cat in catsFromApi)
            {
                if (await _catRepository.GetCatByIdAsync(cat.Id) != null)
                    continue;

                // Create CatEntity
                var newCat = new CatEntity
                {
                    CatId = cat.Id,
                    Width = cat.Width,
                    Height = cat.Height,
                    Image = await _httpClient.GetByteArrayAsync(cat.Url),
                    Created = DateTime.UtcNow
                };

                // Parse and add tags
                var tagNames = cat.Breeds
                    .SelectMany(b => b.Temperament.Split(','))
                    .Select(t => t.Trim())
                    .Distinct();

                foreach (var tagName in tagNames)
                {
                    var tag = await GetOrCreateTagAsync(tagName);
                    newCat.CatTags.Add(new CatTag { Cat = newCat, Tag = tag });
                }

                await _catRepository.AddCatAsync(newCat);
            }

            await _catRepository.SaveChangesAsync();
        }

        // Retrieve by ID
        public async Task<CatEntity?> GetCatByIdAsync(string id)
        {
            return await _catRepository.GetCatByIdAsync(id);
        }

        // Retrieve list of cats.
        public async Task<List<CatEntity>> GetCatsAsync(int page, int pageSize)
        {
            return await _catRepository.GetCatsAsync(page, pageSize);
        }

        // Retrieve list of cats by tag.
        public async Task<List<CatEntity>> GetCatsByTagAsync(string tag, int page, int pageSize)
        {
            return await _catRepository.GetCatsByTagAsync(tag, page, pageSize);
        }

        // Fetch 25 cats.
        private async Task<IEnumerable<CatApiResponse>> FetchCatsFromApiAsync()
        {
            var response = await _httpClient.GetAsync($"images/search?limit=25&has_breeds=1&api_key={_apiKey}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var cats = JsonSerializer.Deserialize<List<CatApiResponse>>(content,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            return cats;
        }

        // Retrieve existing tag or create a new.
        private async Task<TagEntity> GetOrCreateTagAsync(string tagName)
        {
            var tag = await _catRepository.GetTagByNameAsync(tagName);

            if (tag == null)
            {
                tag = new TagEntity
                {
                    Name = tagName,
                    Created = DateTime.UtcNow
                };
                await _catRepository.AddTagAsync(tag);
            }

            return tag;
        }
    }
}
