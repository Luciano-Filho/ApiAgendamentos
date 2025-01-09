using APIAgendamentos.Models;

namespace APIAgendamentos.Interfaces;

public interface IClienteService
{
    Task<IEnumerable<Cliente>> GetAllAsync(int skip, int take);
    Task<Cliente> GetByIdAsync(int id);
    Task<Cliente> AddAsync(Cliente cliente);
    Task<Cliente> DeleteAsync(int id);
    Task<Cliente> UpdateAsync(Cliente cliente);
}
