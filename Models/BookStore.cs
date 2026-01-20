using LivrariaApi.Communication.Response;

namespace LivrariaApi.Models;

public class BookStore
{
    public List<ResponseCreateBookJson> Livros { get; } = new();
}
