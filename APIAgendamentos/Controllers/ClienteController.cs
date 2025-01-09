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
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _service;

        public ClienteController(IClienteService service)
        {
            _service = service;
        }

        /// <summary>
        /// Obtém todos os clientes com suporte à paginação.
        /// </summary>
        /// <param name="skip">Número de registros a serem pulados.</param>
        /// <param name="take">Número máximo de registros a serem retornados.</param>
        /// <returns>Uma lista de clientes.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> RetornaTodosOsClientes(int skip = 0, int take = 50)
        {
            var clientes = await _service.GetAllAsync(skip, take);
            if (!clientes.Any())
                return NoContent(); // Retorna 204 No Content se não houver clientes

            return Ok(clientes); // Retorna 200 OK com a lista de clientes
        }

        /// <summary>
        /// Obtém um cliente pelo ID.
        /// </summary>
        /// <param name="id">ID do cliente.</param>
        /// <returns>Um cliente se encontrado; caso contrário, uma mensagem de erro.</returns>
        [HttpGet("{id:int}", Name = "GetClienteById")]
        public async Task<ActionResult<Cliente>> RetornaClientePorID(int id)
        {
            var cliente = await _service.GetByIdAsync(id);
            if (cliente == null)
                return NotFound($"Não há cliente para o id {id}"); // Retorna 404 Not Found se não encontrar

            return Ok(cliente); // Retorna 200 OK com o cliente encontrado
        }

        /// <summary>
        /// Cadastra um novo cliente.
        /// </summary>
        /// <param name="cliente">Dados do cliente a ser cadastrado.</param>
        /// <returns>O cliente cadastrado ou uma mensagem de erro.</returns>
        [HttpPost]
        public async Task<ActionResult<Cliente>> CadastraCliente([FromBody] Cliente cliente)
        {
            if (cliente == null)
                return BadRequest("Dados inválidos."); // Retorna 400 Bad Request se os dados forem inválidos

            var clienteCadastrado = await _service.AddAsync(cliente);
            return CreatedAtRoute("GetClienteById", new { id = clienteCadastrado.Id }, clienteCadastrado); // Retorna 201 Created com a localização do novo recurso
        }

        /// <summary>
        /// Atualiza os dados de um cliente existente.
        /// </summary>
        /// <param name="cliente">Dados atualizados do cliente.</param>
        /// <returns>O cliente atualizado ou uma mensagem de erro.</returns>
        [HttpPut]
        public async Task<ActionResult<Cliente>> AtualizaCliente([FromBody] Cliente cliente)
        {
            var clientePorId = await _service.GetByIdAsync(cliente.Id);
            if (clientePorId == null)
                return NotFound($"Não há cliente para o id {cliente.Id}"); // Retorna 404 Not Found se o cliente não existir

            var clienteAtualizado = await _service.UpdateAsync(cliente); // Aguarda a atualização do cliente
            return Ok(clienteAtualizado); // Retorna 200 OK com o cliente atualizado
        }

        /// <summary>
        /// Remove um cliente pelo ID.
        /// </summary>
        /// <param name="id">ID do cliente a ser removido.</param>
        /// <returns>Uma mensagem de confirmação ou uma mensagem de erro.</returns>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeletaCliente(int id)
        {
            var clientePorId = await _service.GetByIdAsync(id);
            if (clientePorId == null)
                return NotFound($"Não há cliente para o id {id}");

            await _service.DeleteAsync(id); // Passa o ID para o DeleteAsync
            return NoContent(); // Retorna 204 No Content após a exclusão
        }
    }
}
