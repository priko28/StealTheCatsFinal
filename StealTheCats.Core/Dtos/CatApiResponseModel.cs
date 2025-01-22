using Newtonsoft.Json;

namespace StealTheCats.Core.Dtos;

public class CatApiResponse
{
    [JsonProperty("id")]
    public string Id { get; set; }
    [JsonProperty("width")]
    public int Width { get; set; }
    [JsonProperty("height")]
    public int Height { get; set; }
    [JsonProperty("url")]
    public string Url { get; set; }
    [JsonProperty("breeds")]
    public List<Breed> Breeds { get; set; }
}
