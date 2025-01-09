using APIAgendamentos.DTOs;
using APIAgendamentos.Interfaces;
using APIAgendamentos.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIAgendamentos.Controllers;
[ApiController]
[Route("[Controller]")]

public class AgendamentoController : ControllerBase
{
    private readonly IAgendamentoService _service;
    private readonly IMapper _mapper;
    public AgendamentoController(IAgendamentoService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Agendamento>>> GetAll(int skip = 0, int take = 50)
    {
        var agendamentos = await _service.GetAllAsync(skip, take);
        if (!agendamentos.Any())
            return NoContent();
        return Ok(agendamentos);
    }
    [HttpGet("{id:int}",Name ="GetPorID")]
    public async Task<ActionResult<Agendamento>> GetPorId(int id)
    {
        var agendamento = await _service.GetByIdAsync(id);
        if (agendamento == null)
            return NotFound($"Não há agendamento para o id {id}");
        return Ok(agendamento);
    }

    [HttpGet("profissional/{profissionalId:int}")]
    public async Task<IEnumerable<Agendamento?>> RetornaAgendamentosPorProfissional(int profissionalId)
    {
        return await _service.GetAgendamentosPorProfissional(profissionalId);
    }

    [HttpPost]
    public async Task<ActionResult<Agendamento?>> CadastraAgendamento(AgendamentoRequest agendamentoRequest)
    {
        try
        {
            var agendamentoModel = _mapper.Map<Agendamento>(agendamentoRequest);
            var (sucesso, mensagem, agendamentoCriado) = await _service.AddAsync(agendamentoModel);
            if (!sucesso)
                return BadRequest(new { Mensagem = mensagem });

            return Ok(agendamentoCriado);
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocorreu um erro ao cadastrar um agendamento: {ex.Message}");
        }
    }
    [HttpPut]

    public async Task<ActionResult<Agendamento?>> AtualizaAgendamento(AgendamentoRequest agendamentoRequest)
    {
        try
        {
            //Verifica se existe o agendamento
            var agendamentoPorId = await _service.GetByIdAsync(agendamentoRequest.Id);
            if (agendamentoPorId == null)
                return NotFound($"Não existe agendamento para o id {agendamentoRequest.Id}");


            var agendamentoModel = _mapper.Map<Agendamento>(agendamentoRequest);
            var (sucesso, mensagem, agendamentoAtualizado) = await _service.UpdateAsync(agendamentoModel);
            if (!sucesso)
                return BadRequest(new { Mensagem = mensagem });

            return Ok(agendamentoAtualizado);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
