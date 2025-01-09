using APIAgendamentos.Models;

namespace APIAgendamentos.Interfaces;

public interface IProfissionalRepository
{
    Task<IEnumerable<Profissional?>> GetAllAsync(int skip, int take);
    Task<Profissional?> GetByIdAsync(int id);
    Task<Profissional> AddAsync(Profissional profissional);
    Task<Profissional?> UpdateAsync(Profissional profissional);
    Task<Profissional> DeleteAsync(Profissional profissional);
}
