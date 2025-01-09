using System.ComponentModel.DataAnnotations;

namespace APIAgendamentos.Models;

public class Servico
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo nome é obrigatório.")]
    public string? Nome { get; set; }                

    [StringLength(300, ErrorMessage = "O Endereço não pode ter mais de 300 caracteres.")]
    public string? Descricao { get; set; }

    public decimal Preco { get; set; }              
    public TimeSpan Duracao { get; set; }           
    public bool Ativo { get; set; }      
}
