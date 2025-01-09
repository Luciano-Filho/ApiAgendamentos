using APIAgendamentos.Data;
using APIAgendamentos.Interfaces;
using APIAgendamentos.Models;
using Microsoft.EntityFrameworkCore;

namespace APIAgendamentos.Repositories;

public class AgendamentoRepository : IAgendamentoRepository
{
    private readonly AgendamentoContext _context;
    public AgendamentoRepository(AgendamentoContext context)
    {
        _context = context;   
    }
    public async Task<IEnumerable<Agendamento?>> GetAllAsync(int skip, int take)
    {
        return await _context.Agendamentos.Include(a => a.Cliente).
            Include(a => a.Profissional).Include(a => a.Servico).
            Skip(skip).Take(take).ToListAsync();
    }
    public async Task<Agendamento?> GetByIdAsync(int id)
    {
        return await _context.Agendamentos.Include(a => a.Cliente)
            .Include(a => a.Profissional).Include(a => a.Servico)
            .FirstOrDefaultAsync(a => a.Id == id);
    }
    public async Task<IEnumerable<Agendamento?>> GetAgendamentoProfissiolnalAsync(int id)
    {
        var agendamentos = await _context.Agendamentos.Where(p => p.ProfissionalId == id).
            AsNoTracking().Include(p=>p.Profissional).Include(p=>p.Cliente).Include(p => p.Servico).ToListAsync();
        return agendamentos;
    }

    public async Task<bool> VerificaExistencia(int idCliente, int idProfissional, int idServico)
    {
        bool existeCliente = await _context.Clientes.AnyAsync(c => c.Id == idCliente); //Usar any porque o firstOrDefault pode retornar null
        bool existeProfissional = await _context.Profissionais.AnyAsync(p => p.Id == idProfissional);
        bool existeServico = await _context.Servicos.AnyAsync(s => s.Id == idServico);
        return existeCliente && existeProfissional && existeServico;
    }
    public async Task<Agendamento?> AddAsync(Agendamento agendamento)
    {
        await _context.Agendamentos.AddAsync(agendamento);
        await _context.SaveChangesAsync();
        return agendamento;
    }
    public async Task<Agendamento?> UpdateAsync(Agendamento agendamento)
    {
        _context.Agendamentos.Update(agendamento);
        await _context.SaveChangesAsync();
        return agendamento;
    }
    public async Task<bool> ProfissionalIndisponivelAsync(int id, DateTime horario)
    {
        var profissional = await _context.Profissionais.FirstOrDefaultAsync(p => p.Id == id);
        return profissional == null
            ? throw new Exception($"Não há profissional para o id {id}")
            : await _context.Agendamentos
       .AnyAsync(a => a.ProfissionalId == profissional.Id && a.DataAgendamento == horario);
    }
}
