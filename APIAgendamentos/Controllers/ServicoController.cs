using APIAgendamentos.Interfaces;
using APIAgendamentos.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIAgendamentos.Controllers;
[ApiController]
[Route("[Controller]")]

public class ServicoController : ControllerBase
{
    private readonly IServicoService _service;
    public ServicoController(IServicoService servico)
    {
        _service = servico;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Servico>>> RetornaTodosOsServicos(int skip = 0, int take = 50)
    {
        var servicos = await _service.GetAllAsync(skip, take);
        if (!servicos.Any())
            return NoContent();
        return Ok(servicos);
    }
    [HttpGet("{id:int}", Name ="GetServicoById")]
    public async Task<ActionResult<Servico>> RetornaServicoPorId(int id)
    {
        var servico = await _service.GetByIdAsync(id);
        if (servico == null)
            return NotFound($"Não foi encontrado Servico para o id {id}");
        return Ok(servico);
    }
    [HttpPost]
    public async Task<ActionResult<Servico?>> CadastraServico(Servico servico)
    {
        if(servico == null)
            return BadRequest("Dados inválidos. Verifique os dados informados");
        var servicoCadastrado = await _service.AddAsync(servico);
        return CreatedAtRoute("GetServicoById", new { id = servicoCadastrado.Id }, servicoCadastrado);
    }
    [HttpPut]
    public async Task<ActionResult<Servico>> AtualizaServico(Servico servico)
    {
        var servicoExistente = await _service.GetByIdAsync(servico.Id);
        if (servicoExistente == null)
            return NotFound($"Não há Servico com o id {servico.Id}");
        var servicoAtualizado = await _service.UpdateAsync(servico);
        return Ok(servicoAtualizado);
    }
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Servico?>> DeletaServico(int id)
    {
        var servico = await _service.GetByIdAsync(id);
        if (servico == null)
            return NotFound($"Não há Servico com o id {id}");
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
