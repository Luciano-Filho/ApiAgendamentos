using APIAgendamentos.Data;
using APIAgendamentos.Interfaces;
using APIAgendamentos.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIAgendamentos.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AgendamentoContext _context;

        public ClienteRepository(AgendamentoContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Cliente?>> GetAllAsync(int skip, int take)
        {
            return await _context.Clientes
                                 .AsNoTracking() // Não rastreia as entidades
                                 .Skip(skip)
                                 .Take(take)
                                 .ToListAsync();
        }
        public async Task<Cliente?> GetByIdAsync(int id)
        {
            return await _context.Clientes.FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<Cliente?> AddAsync(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }
        public async Task<Cliente?> DeleteAsync(Cliente cliente)
        {
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }
        public async Task<Cliente?> UpdateAsync(Cliente cliente)
        {
            var entidadeExistente = await _context.Clientes.FirstOrDefaultAsync(p => p.Id == cliente.Id);

            if (entidadeExistente == null)
                return null;

            _context.Entry(entidadeExistente).CurrentValues.SetValues(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }
    }
}
