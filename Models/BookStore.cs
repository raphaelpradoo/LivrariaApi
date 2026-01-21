using LivrariaApi.Communication.Response;

namespace LivrariaApi.Models;

public class BookStore
{
    public List<ResponseBookJson> Livros { get; } = new();
}
