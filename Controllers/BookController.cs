using LivrariaApi.Communication.Request;
using LivrariaApi.Communication.Response;
using LivrariaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace LivrariaApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly BookStore Livraria;
    public BookController(BookStore livraria)
    {
        Livraria = livraria;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Get()
    {
        if (Livraria.Livros.Any() == false)
            return NoContent();
        return Ok(Livraria.Livros);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Get(Guid id)
    {
        var livro = Livraria.Livros.FirstOrDefault(l => l.Id == id);

        if (livro is null)
            return NotFound();

        return Ok(livro);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResponseCreateBookJson), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public IActionResult Create([FromBody] RequestCreateBookJson request) 
    {
        // Verifica se já existe livro com o mesmo título
        bool tituloJaExiste = Livraria.Livros
            .Any(l => l.Title.Equals(request.Title, StringComparison.OrdinalIgnoreCase));

        if (tituloJaExiste)
            return Conflict("Já existe um livro cadastrado com este título.");

        var response = new ResponseCreateBookJson()
        {
            Title = request.Title,
            Author = request.Author,
            Price = request.Price,
            Stock = request.Stock,
            Genre = request.Genre
        };

        Livraria.Livros.Add(response);

        return Created(string.Empty, response);
    }

    [HttpPost("create-list")]
    [ProducesResponseType(typeof(IEnumerable<ResponseCreateBookJson>), StatusCodes.Status201Created)]
    public IActionResult Create([FromBody] List<RequestCreateBookJson> requests)
    {
        var titlesExistentes = Livraria.Livros
            .Select(l => l.Title)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        var novosLivros = requests
            .Where(r => titlesExistentes.Add(r.Title)) // Add retorna false se já existir
            .Select(r => new ResponseCreateBookJson
            {
                Title = r.Title,
                Author = r.Author,
                Genre = r.Genre,
                Price = r.Price,
                Stock = r.Stock
            }).ToList();

        foreach (var livro in novosLivros)
            Livraria.Livros.Add(livro);

        return Created(string.Empty, novosLivros);
    }
}
