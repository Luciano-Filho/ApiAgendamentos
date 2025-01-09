using APIAgendamentos.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace APIAgendamentos.Data;

public class AgendamentoContext : IdentityDbContext<IdentityUser>
{
    public AgendamentoContext(DbContextOptions<AgendamentoContext> options)
        : base(options)
    {
    }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Profissional> Profissionais { get; set; }
    public DbSet<Agendamento> Agendamentos { get; set; }
    public DbSet<Servico> Servicos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); //Para usar o Identity
    }
}
