using LivrariaApi.Models;

namespace LivrariaApi.Communication.Request;

public class RequestCreateBookJson
{
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public Genre Genre { get; set; }
    public decimal Price { get; set; } = decimal.Zero;
    public int Stock { get; set; } = 0;
}
