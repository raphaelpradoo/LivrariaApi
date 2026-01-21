using LivrariaApi.Models;
using System.ComponentModel.DataAnnotations;

namespace LivrariaApi.Communication.Request;

public class RequestBookJson
{
    [Required(ErrorMessage = "O título é obrigatório.")]
    [StringLength(120, MinimumLength = 2, ErrorMessage = "O título deve ter entre 2 e 120 caracteres.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "O autor é obrigatório.")]
    [StringLength(120, MinimumLength = 2, ErrorMessage = "O autor deve ter entre 2 e 120 caracteres.")]
    public string Author { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "O gênero é obrigatório.")]
    [EnumDataType(typeof(Genre), ErrorMessage = "Gênero inválido.")]
    public Genre Genre { get; set; }

    [Required(ErrorMessage = "O preço é obrigatório.")]
    [Range(0, double.MaxValue, ErrorMessage = "O preço deve ser maior ou igual a zero.")]
    public decimal Price { get; set; } = decimal.Zero;

    [Required(ErrorMessage = "O estoque é obrigatório.")]
    [Range(0, int.MaxValue, ErrorMessage = "O estoque deve ser maior ou igual a zero.")]
    public int Stock { get; set; } = 0;
}
