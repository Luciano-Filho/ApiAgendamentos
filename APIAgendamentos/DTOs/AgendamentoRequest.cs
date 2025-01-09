using APIAgendamentos.Models.Enums;

namespace APIAgendamentos.DTOs;

public class AgendamentoRequest
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public int ProfissionalId { get; set; }
    public int ServicoId { get; set; }
    public DateTime? DataAgendamento { get; set; }
    public StatusAgendamento? Status { get; set; } = StatusAgendamento.Pendente;
    public string? Notas { get; set; } 
}
