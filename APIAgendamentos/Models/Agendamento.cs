using APIAgendamentos.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APIAgendamentos.Models;

public class Agendamento
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo ClienteId é obrigatório.")]
    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; }

    [Required(ErrorMessage = "O campo ProfissionalId é obrigatório.")]
    public int ProfissionalId { get; set; }
    public Profissional? Profissional { get; set; }

    [Required(ErrorMessage = "O campo ServicoId é obrigatório.")]
    public int ServicoId { get; set; }
    public Servico? Servico { get; set; }

    [Required(ErrorMessage = "A data do agendamento é obrigatória.")]
    public DateTime DataAgendamento { get; set; }
    public StatusAgendamento? Status { get; set; } = StatusAgendamento.Pendente;
    public string? Notas { get; set; }
    public string Link { get; set; }
}
