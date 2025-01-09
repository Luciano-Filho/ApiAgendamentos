using System.ComponentModel.DataAnnotations;

namespace APIAgendamentos.Models;

public class Profissional
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo nome é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome não pode ter mais de 100 caracteres.")]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "O campo Especialidade é obrigatório.")]
    public string? Especialidade { get; set; }

    [EmailAddress(ErrorMessage = "O formato do email é inválido.")]
    public string? Email { get; set; }

    [Phone(ErrorMessage = "O formato do telefone é inválido.")]
    public string? Telefone { get; set; }
    //public decimal ValorHora { get; set; }
}
