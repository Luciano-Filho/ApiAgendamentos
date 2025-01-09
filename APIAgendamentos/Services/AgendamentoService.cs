using APIAgendamentos.DTOs;
using APIAgendamentos.Interfaces;
using APIAgendamentos.Models;

namespace APIAgendamentos.Services;

public class AgendamentoService : IAgendamentoService
{
    private readonly IAgendamentoRepository _repo;
    public AgendamentoService(IAgendamentoRepository repo)
    {
        _repo = repo;
    }
    public async Task<IEnumerable<Agendamento?>> GetAllAsync(int skip, int take)
    {
        int maxPageSize = 100;
        take = (take > maxPageSize) ? maxPageSize : take;
        return await _repo.GetAllAsync(skip, take);
    }
    public async Task<Agendamento?> GetByIdAsync(int id)
    {
        var agendamento = await _repo.GetByIdAsync(id);
        if (agendamento == null)
            return null;
        return agendamento;
    }
    public async Task<IEnumerable<Agendamento?>> GetAgendamentosPorProfissional(int id)
    {
        return await _repo.GetAgendamentoProfissiolnalAsync(id);
    }
    public async Task<(bool sucesso, string mensagem, Agendamento? dados)> AddAsync(Agendamento agendamento)
    {
        var validacao = await ValidarAgendamento(agendamento);
        if (!validacao.sucesso)
            return validacao;

        var agendamentoCriado = await _repo.AddAsync(agendamento);
        return (true, "Agendamento criado com sucesso", agendamentoCriado);
    }
    public async Task<(bool sucesso, string mensagem, Agendamento? dados)> UpdateAsync(Agendamento agendamento)
    {
        var validacao = await ValidarAgendamento(agendamento);
        if (!validacao.sucesso)
            return validacao;
        var agendamentoAtualizado = await _repo.UpdateAsync(agendamento);
        return (true, "Agendamento atualizado com sucesso", agendamentoAtualizado);
    }
    private async Task<(bool sucesso, string mensagem, Agendamento? dados)> ValidarAgendamento(Agendamento agendamento)
    {
        bool existeObjetos = await _repo.VerificaExistencia(agendamento.ClienteId, agendamento.ProfissionalId, agendamento.ServicoId);
        if (!existeObjetos)
            return (false, "Cliente, Profissional ou Serviço inválido(s)", null);
        var profissionalOcupado = await _repo.ProfissionalIndisponivelAsync(agendamento.ProfissionalId, agendamento.DataAgendamento);
        if (profissionalOcupado)
            return (false, $"Profissional não está disponível no horário escolhido", null);

        return (true, "Validação bem-sucedida", null);
    }
}

