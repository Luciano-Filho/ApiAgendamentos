using APIAgendamentos.Interfaces;
using APIAgendamentos.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIAgendamentos.Services
{
    public class ProfissionalService : IProfissionalService
    {
        private readonly IProfissionalRepository _repo;

        public ProfissionalService(IProfissionalRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Obtém todos os profissionais com suporte à paginação.
        /// </summary>
        /// <param name="skip">Número de registros a serem pulados.</param>
        /// <param name="take">Número máximo de registros a serem retornados.</param>
        /// <returns>Uma lista de profissionais.</returns>
        public async Task<IEnumerable<Profissional>> GetAllAsync(int skip, int take)
        {
            const int maxPageSize = 100; // Tamanho máximo da página
            take = (take > maxPageSize) ? maxPageSize : take; // Limita o tamanho da página
            return await _repo.GetAllAsync(skip, take);
        }

        /// <summary>
        /// Obtém um profissional pelo ID.
        /// </summary>
        /// <param name="id">ID do profissional.</param>
        /// <returns>Um profissional se encontrado; caso contrário, null.</returns>
        public async Task<Profissional> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id); // Retorna diretamente o profissional
        }

        /// <summary>
        /// Adiciona um novo profissional.
        /// </summary>
        /// <param name="profissional">Profissional a ser adicionado.</param>
        /// <returns>O profissional adicionado.</returns>
        public async Task<Profissional> AddAsync(Profissional profissional)
        {
            if (profissional == null)
                return null; // Retorna null se o profissional for inválido
            return await _repo.AddAsync(profissional);
        }

        /// <summary>
        /// Remove um profissional.
        /// </summary>
        /// <param name="profissional">Profissional a ser removido.</param>
        /// <returns>O profissional removido.</returns>
        public async Task<Profissional> DeleteAsync(Profissional profissional)
        {
            if (profissional == null)
                return null; // Retorna null se o profissional for inválido
            await _repo.DeleteAsync(profissional);
            return profissional;
        }

        /// <summary>
        /// Atualiza os dados de um profissional existente.
        /// </summary>
        /// <param name="profissional">Profissional com os dados atualizados.</param>
        /// <returns>O profissional atualizado.</returns>
        public async Task<Profissional> UpdateAsync(Profissional profissional)
        {
            if (profissional == null)
                return null; // Retorna null se o profissional for inválido
            return await _repo.UpdateAsync(profissional);
        }
    }
}
