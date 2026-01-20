using LivrariaApi.Models;

namespace LivrariaApi.Communication.Response;

public class ResponseCreateBookJson
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public Genre Genre { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public DateTime Created_At { get; set; } = DateTime.Now;
    public DateTime Update_At { get; set; }
}
