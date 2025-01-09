using APIAgendamentos.Interfaces;
using APIAgendamentos.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIAgendamentos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfissionalController : ControllerBase
    {
        private readonly IProfissionalService _service;

        public ProfissionalController(IProfissionalService service)
        {
            _service = service;
        }

        /// <summary>
        /// Obtém todos os profissionais com suporte à paginação.
        /// </summary>
        /// <param name="skip">Número de registros a serem pulados.</param>
        /// <param name="take">Número máximo de registros a serem retornados.</param>
        /// <returns>Uma lista de profissionais.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profissional>>> RetornaTodosProfissionais(int skip = 0, int take = 50)
        {
            var profissionais = await _service.GetAllAsync(skip, take);
            if (!profissionais.Any())
                return NoContent(); // Retorna 204 No Content se não houver profissionais

            return Ok(profissionais); // Retorna 200 OK com a lista de profissionais
        }

        /// <summary>
        /// Obtém um profissional pelo ID.
        /// </summary>
        /// <param name="id">ID do profissional.</param>
        /// <returns>Um profissional se encontrado; caso contrário, uma mensagem de erro.</returns>
        [HttpGet("{id:int}", Name = "GetById")]
        public async Task<ActionResult<Profissional>> RetornaProfissionalPorId(int id)
        {
            var profissional = await _service.GetByIdAsync(id);
            if (profissional == null)
                return NotFound($"Não foi encontrado um profissional para o id {id}"); // Retorna 404 Not Found se não encontrar

            return Ok(profissional); // Retorna 200 OK com o profissional encontrado
        }

        /// <summary>
        /// Cadastra um novo profissional.
        /// </summary>
        /// <param name="profissional">Dados do profissional a ser cadastrado.</param>
        /// <returns>O profissional cadastrado ou uma mensagem de erro.</returns>
        [HttpPost]
        public async Task<ActionResult<Profissional>> CadastraProfissional([FromBody] Profissional profissional)
        {
            if (profissional == null)
                return BadRequest("Dados inválidos. Verifique os dados informados"); // Retorna 400 Bad Request se os dados forem inválidos

            var profissionalCadastrado = await _service.AddAsync(profissional);
            return CreatedAtRoute("GetById", new { id = profissionalCadastrado.Id }, profissionalCadastrado); // Retorna 201 Created com a localização do novo recurso
        }

        /// <summary>
        /// Atualiza os dados de um profissional existente.
        /// </summary>
        /// <param name="profissional">Dados atualizados do profissional.</param>
        /// <returns>O profissional atualizado ou uma mensagem de erro.</returns>
        [HttpPut]
        public async Task<ActionResult<Profissional>> AtualizaProfissional([FromBody] Profissional profissional)
        {
            var profissionalExistente = await _service.GetByIdAsync(profissional.Id);
            if (profissionalExistente == null)
                return NotFound($"Não há profissional com o id {profissional.Id}"); // Retorna 404 Not Found se o profissional não existir

            var profissionalAtualizado = await _service.UpdateAsync(profissional);
            return Ok(profissionalAtualizado); // Retorna 200 OK com o profissional atualizado
        }

        /// <summary>
        /// Remove um profissional pelo ID.
        /// </summary>
        /// <param name="id">ID do profissional a ser removido.</param>
        /// <returns>Uma mensagem de confirmação ou uma mensagem de erro.</returns>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeletaProfissional(int id)
        {
            var profissionalPorId = await _service.GetByIdAsync(id);
            if (profissionalPorId == null)
                return NotFound($"Não há profissional para o id {id}"); // Retorna 404 Not Found se o profissional não existir

            await _service.DeleteAsync(profissionalPorId); // Remove o profissional
            return NoContent(); // Retorna 204 No Content após a exclusão
        }
    }
}
