using APIAgendamentos.Interfaces;
using APIAgendamentos.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIAgendamentos.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repo;

        public ClienteService(IClienteRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Obtém todos os clientes com suporte à paginação.
        /// </summary>
        /// <param name="skip">Número de registros a serem pulados.</param>
        /// <param name="take">Número máximo de registros a serem retornados.</param>
        /// <returns>Uma lista de clientes.</returns>
        public async Task<IEnumerable<Cliente>> GetAllAsync(int skip, int take)
        {
            const int maxPageSize = 100; // Tamanho máximo da página
            take = (take > maxPageSize) ? maxPageSize : take; // Limita o tamanho da página
            return await _repo.GetAllAsync(skip, take);
        }

        /// <summary>
        /// Obtém um cliente pelo ID.
        /// </summary>
        /// <param name="id">ID do cliente.</param>
        /// <returns>Um cliente se encontrado; caso contrário, null.</returns>
        public async Task<Cliente> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id); // Retorna diretamente o cliente
        }

        /// <summary>
        /// Adiciona um novo cliente.
        /// </summary>
        /// <param name="cliente">Cliente a ser adicionado.</param>
        /// <returns>O cliente adicionado.</returns>
        public async Task<Cliente> AddAsync(Cliente cliente)
        {
            if (cliente == null)
                return null; // Retorna null se o cliente for inválido
            return await _repo.AddAsync(cliente);
        }

        /// <summary>
        /// Remove um cliente pelo ID.
        /// </summary>
        /// <param name="id">ID do cliente a ser removido.</param>
        /// <returns>O cliente removido.</returns>
        public async Task<Cliente> DeleteAsync(int id)
        {
            var cliente = await _repo.GetByIdAsync(id);
            if (cliente == null)
                return null; // Retorna null se o cliente não for encontrado
            return await _repo.DeleteAsync(cliente);
        }

        /// <summary>
        /// Atualiza os dados de um cliente existente.
        /// </summary>
        /// <param name="cliente">Cliente com os dados atualizados.</param>
        /// <returns>O cliente atualizado.</returns>
        public async Task<Cliente> UpdateAsync(Cliente cliente)
        {
            if (cliente == null)
                return null; // Retorna null se o cliente for inválido
            return await _repo.UpdateAsync(cliente);
        }
    }
}
