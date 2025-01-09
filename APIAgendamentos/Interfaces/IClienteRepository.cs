using APIAgendamentos.Models;
namespace APIAgendamentos.Interfaces;

public interface IClienteRepository
{
    Task<IEnumerable<Cliente?>> GetAllAsync(int skip, int take);
    Task<Cliente?> GetByIdAsync(int id);
    Task<Cliente?> AddAsync(Cliente cliente);
    Task<Cliente?> UpdateAsync(Cliente cliente);
    Task<Cliente?> DeleteAsync(Cliente cliente);
}
