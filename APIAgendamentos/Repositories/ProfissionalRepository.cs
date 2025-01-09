using APIAgendamentos.Data;
using APIAgendamentos.Interfaces;
using APIAgendamentos.Models;
using Microsoft.EntityFrameworkCore;

namespace APIAgendamentos.Repositories
{
    public class ProfissionalRepository : IProfissionalRepository
    {
        private readonly AgendamentoContext _context;

        public ProfissionalRepository(AgendamentoContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Profissional?>> GetAllAsync(int skip, int take)
        {
            return await _context.Profissionais
                                 .AsNoTracking()
                                 .Skip(skip)
                                 .Take(take)
                                 .ToListAsync();
        }
        public async Task<Profissional?> GetByIdAsync(int id)
        {
            return await _context.Profissionais.FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<Profissional> AddAsync(Profissional profissional)
        {
            await _context.Profissionais.AddAsync(profissional);
            await _context.SaveChangesAsync();
            return profissional;
        }
        public async Task<Profissional?> UpdateAsync(Profissional profissional)
        {
            var entidadeExistente = await _context.Profissionais.FirstOrDefaultAsync(p => p.Id == profissional.Id);

            if (entidadeExistente == null)
                return null;

            _context.Entry(entidadeExistente).CurrentValues.SetValues(profissional);
            await _context.SaveChangesAsync();
            return profissional;
        }
        public async Task<Profissional> DeleteAsync(Profissional profissional)
        {
            _context.Profissionais.Remove(profissional);
            await _context.SaveChangesAsync();
            return profissional;
        }
    }
}
