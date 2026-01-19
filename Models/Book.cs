namespace LivrariaApi.Models;

public class Book
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public Genre Genre { get; set; }    
    public decimal Price { get; set; } = decimal.Zero;
    public int Stock { get; set; } = 0;
    public DateTime Created_At { get; set; } = DateTime.Now;
    public DateTime? Update_At { get; set; } = null;
}
public enum Genre
{
    Ação = 1,
    Romance = 2,
    Fição = 3,
    Guerra = 4,
    Manga = 5,
    HQ = 6
};
