namespace APIAgendamentos.Models;
public class FormularioAgendamento
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public List<Servico> Servicos { get; set; } = new List<Servico>();
    public List<Profissional> Profissionais { get; set; } = new List<Profissional>();
    public List<DateTime> HorariosDisponiveis { get; set; } = new List<DateTime>();
    public string Link { get; private set; }
    public DateTime DataCriacao { get; private set; }

    public FormularioAgendamento()
    {
        DataCriacao = DateTime.Now;
        GerarLink();
    }

    public void GerarLink()
    {
        // Cria um link único usando um GUID
        Link = $"https://api.meuhorario.com/agendamento/{Guid.NewGuid()}";
    }

    public void AdicionarServico(Servico servico)
    {
        Servicos.Add(servico);
    }

    public void AdicionarProfissional(Profissional profissional)
    {
        Profissionais.Add(profissional);
    }

    public void DefinirHorariosDisponiveis(List<DateTime> horarios)
    {
        HorariosDisponiveis = horarios;
    }
}
