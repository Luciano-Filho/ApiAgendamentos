using APIAgendamentos.Interfaces;
using APIAgendamentos.Models;

namespace APIAgendamentos.Services;

public class ServicoService : IServicoService
{
    private readonly IServicoRepository _repo;
    public ServicoService(IServicoRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<Servico?>> GetAllAsync(int skip, int take)
    {
        const int MAXPAGESIZE = 10;
        take = (take >  MAXPAGESIZE) ? MAXPAGESIZE : take;
        return await _repo.GetAllAsync(skip, take);
    }
    public async Task<Servico?> GetByIdAsync(int id)
    {
        return await _repo.GetByIdAsync(id);
    }
    public async Task<Servico?> AddAsync(Servico servico)
    {
        if (servico == null)
            return null;
        return await _repo.AddAsync(servico);
    }

    public async Task<Servico?> DeleteAsync(int id)
    {
        var servico = await _repo.GetByIdAsync(id);
        if (servico == null)
            return null;
        return await _repo.DeleteAsync(servico);
    }
    public async Task<Servico?> UpdateAsync(Servico servico)
    {
        if (servico == null)
            return null; 
        return await _repo.UpdateAsync(servico);
    }
}
