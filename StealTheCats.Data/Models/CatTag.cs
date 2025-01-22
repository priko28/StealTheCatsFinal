using System.Text.Json.Serialization;

namespace StealTheCats.Data.Models;

public class CatTag
{
    public int CatId { get; set; }
    public int TagId { get; set; }
    public CatEntity Cat { get; set; }
    public TagEntity Tag { get; set; }
}
