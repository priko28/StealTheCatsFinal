namespace StealTheCats.Core.Dtos;

public class CatDto
{
    public int Id { get; set; }
    public string CatId { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public byte[] Image { get; set; }
    public DateTime Created { get; set; }
    public ICollection<string> Tags { get; set; }
}
