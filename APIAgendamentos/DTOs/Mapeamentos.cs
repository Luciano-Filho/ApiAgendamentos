using APIAgendamentos.Models;
using AutoMapper;

namespace APIAgendamentos.DTOs;

public class Mapeamentos : Profile
{
    public Mapeamentos()
    {
        CreateMap<AgendamentoRequest, Agendamento>().ReverseMap();

    }
}
