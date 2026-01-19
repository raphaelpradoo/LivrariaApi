using LivrariaApi.Communication.Request;
using LivrariaApi.Communication.Response;
using LivrariaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace LivrariaApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Estou funcionando!");
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResponseCreateBookJson), StatusCodes.Status201Created)]
    public IActionResult Create([FromBody] RequestCreateBookJson request) 
    {
        var response = new ResponseCreateBookJson()
        {
            Title = request.Title,
            Author = request.Author,
            Price = request.Price,
            Stock = request.Stock,
            Genre = request.Genre
        };

        return Created(string.Empty, response);
    }

    [HttpPost("create-list")]
    [ProducesResponseType(typeof(IEnumerable<ResponseCreateBookJson>), StatusCodes.Status201Created)]
    public IActionResult Create([FromBody] List<RequestCreateBookJson> requests)
    {
        var responses = requests.Select(request => new ResponseCreateBookJson
        {
            Title = request.Title,
            Author = request.Author,
            Genre = request.Genre,
            Price = request.Price,
            Stock = request.Stock,
        }).ToList();

        return Created(string.Empty, responses);
    }
}
