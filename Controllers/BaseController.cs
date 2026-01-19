using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LivrariaApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Estou funcionando!");
    }
}
