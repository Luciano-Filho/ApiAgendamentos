using APIAgendamentos.Models;
namespace APIAgendamentos.Interfaces;

public interface IAgendamentoService
{
    Task<IEnumerable<Agendamento?>> GetAllAsync(int skip, int take);
    Task<Agendamento?> GetByIdAsync(int id);
    Task<(bool sucesso, string mensagem, Agendamento? dados)> AddAsync(Agendamento agendamento);
    Task<(bool sucesso, string mensagem, Agendamento? dados)> UpdateAsync(Agendamento agendamento);
    Task<IEnumerable<Agendamento?>> GetAgendamentosPorProfissional(int idProfissional);
}
