using APIAgendamentos.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIAgendamentos.Interfaces
{
    public interface IServicoService
    {
        Task<IEnumerable<Servico?>> GetAllAsync(int skip, int take);
        Task<Servico?> GetByIdAsync(int id);
        Task<Servico?> AddAsync(Servico servico);

        Task<Servico?> DeleteAsync(int id);
        Task<Servico?> UpdateAsync(Servico serviço);
    }
}
