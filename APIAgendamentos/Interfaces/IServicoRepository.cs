using APIAgendamentos.Models;

namespace APIAgendamentos.Interfaces;

public interface IServicoRepository
{
    Task<IEnumerable<Servico?>> GetAllAsync(int skip, int take);
    Task<Servico?> GetByIdAsync(int id);
    Task<Servico?> AddAsync(Servico servico);
    Task<Servico?> DeleteAsync(Servico servico);
    Task<Servico?> UpdateAsync(Servico servico);
}
