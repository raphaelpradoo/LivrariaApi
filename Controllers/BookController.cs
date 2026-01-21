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
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Get()
    {
        if (Livraria.Livros.Any() == false)
            return NoContent();
        return Ok(Livraria.Livros);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Get(Guid id)
    {
        var livro = Livraria.Livros.FirstOrDefault(l => l.Id == id);

        if (livro is null)
            return NotFound();

        return Ok(livro);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResponseBookJson), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Create([FromBody] RequestBookJson request) 
    {
        // Verifica se já existe livro com o mesmo título
        bool tituloJaExiste = Livraria.Livros
            .Any(l => l.Title.Equals(request.Title, StringComparison.OrdinalIgnoreCase));

        if (tituloJaExiste)
            return Conflict("Já existe um livro cadastrado com este título.");

        var response = new ResponseBookJson()
        {
            Title = request.Title,
            Author = request.Author,
            Price = request.Price,
            Stock = request.Stock,
            Genre = request.Genre,
            Created_At = DateTime.UtcNow,
        };

        Livraria.Livros.Add(response);

        return Created(string.Empty, response);
    }

    [HttpPost("create-list")]
    [ProducesResponseType(typeof(IEnumerable<ResponseBookJson>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Create([FromBody] List<RequestBookJson> requests)
    {
        var titlesExistentes = Livraria.Livros
            .Select(l => l.Title)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        var novosLivros = requests
            .Where(r => titlesExistentes.Add(r.Title)) // Add retorna false se já existir
            .Select(r => new ResponseBookJson
            {
                Title = r.Title,
                Author = r.Author,
                Genre = r.Genre,
                Price = r.Price,
                Stock = r.Stock,
                Created_At = DateTime.UtcNow
            }).ToList();

        foreach (var livro in novosLivros)
            Livraria.Livros.Add(livro);

        return Created(string.Empty, novosLivros);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Update(Guid id, [FromBody] RequestBookJson request) 
    {
        // Verifica se o livro a ser editado existe
        var livro = Livraria.Livros.FirstOrDefault(l => l.Id == id);

        if (livro is null)
            return NotFound();

        // Verifica se já existe livro com o mesmo título
        bool tituloJaExiste = Livraria.Livros
            .Any(l => l.Title.Equals(request.Title, StringComparison.OrdinalIgnoreCase));

        if (tituloJaExiste)
            return Conflict("Já existe um livro cadastrado com este título.");

        livro.Title = request.Title;
        livro.Author = request.Author;
        livro.Price = request.Price;
        livro.Stock = request.Stock;
        livro.Genre = request.Genre;
        livro.Update_At = DateTime.UtcNow;

        return Ok(livro);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Delete(Guid id) 
    {
        // Verifica se o livro a ser excluido existe
        var livro = Livraria.Livros.FirstOrDefault(l => l.Id == id);

        if (livro is null)
            return NotFound();

        Livraria.Livros.RemoveAt(Livraria.Livros.IndexOf(livro));

        return Ok("Livro " + livro.Title + " removido com sucesso.");
    }
}
