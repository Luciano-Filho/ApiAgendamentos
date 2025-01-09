using System.ComponentModel.DataAnnotations;

namespace APIAgendamentos.Models;

public class Cliente
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo nome é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome não pode ter mais de 100 caracteres.")]
    public string Nome { get; set; } = String.Empty;

    [Phone(ErrorMessage = "O formato do telefone é inválido.")]
    public string? Telefone { get; set; }
    /*[Required(ErrorMessage = "O campo email é obrigatório.")]
    [EmailAddress(ErrorMessage = "O formato do email é inválido.")]
    public string? Email { get; set; }*/

    /*[Required(ErrorMessage = "A Data de Nascimento é obrigatória.")]
    public DateTime DataNascimento { get; set; }

    [StringLength(200, ErrorMessage = "O Endereço não pode ter mais de 200 caracteres.")]
    public string? Endereco { get; set; }*/
}
