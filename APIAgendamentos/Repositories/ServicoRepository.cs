using APIAgendamentos.Data;
using APIAgendamentos.Interfaces;
using APIAgendamentos.Models;
using Microsoft.EntityFrameworkCore;

namespace APIAgendamentos.Repositories
{
    public class ServicoRepository : IServicoRepository
    {
        private readonly AgendamentoContext _context;
        public ServicoRepository(AgendamentoContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Servico?>> GetAllAsync(int skip, int take)
        {
            return await _context.Servicos.AsNoTracking().Skip(skip).Take(take).ToListAsync();
        }
        public async Task<Servico?> GetByIdAsync(int id)
        {
            return await _context.Servicos.FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<Servico?> AddAsync(Servico servico)
        {
            await _context.Servicos.AddAsync(servico);
            await _context.SaveChangesAsync();
            return servico;
        }
        public async Task<Servico?> DeleteAsync(Servico servico)
        {
            _context.Remove(servico);
            await _context.SaveChangesAsync();
            return servico;
        }
        public async Task<Servico?> UpdateAsync(Servico servico)
        {
            var entidadeExistente = await _context.Servicos.FirstOrDefaultAsync(p => p.Id == servico.Id);
            if (entidadeExistente == null)
                return null; 

            _context.Entry(entidadeExistente).CurrentValues.SetValues(servico);
            await _context.SaveChangesAsync();
            return servico;
        }
    }
}
