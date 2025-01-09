using APIAgendamentos.Models;

namespace APIAgendamentos.Interfaces;

public interface IAgendamentoRepository
{
    Task<IEnumerable<Agendamento?>> GetAllAsync(int skip, int take);
    Task<Agendamento?> GetByIdAsync(int id);
    Task<Agendamento?> AddAsync(Agendamento agendamento);
    Task<Agendamento?> UpdateAsync(Agendamento agendamento);
    Task<bool> VerificaExistencia(int idCliente, int idProfissional, int idServico);
    Task<bool> ProfissionalIndisponivelAsync(int id, DateTime horario);
    Task<IEnumerable<Agendamento?>> GetAgendamentoProfissiolnalAsync(int id);
}
