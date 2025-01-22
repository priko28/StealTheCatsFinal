using Newtonsoft.Json;

namespace StealTheCats.Core.Dtos;

public class Breed
{
    [JsonProperty("temperament")]
    public string Temperament { get; set; }
}
