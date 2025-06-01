using api_safezone_cs.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace api_safezone_cs.Data.AppData;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Alerta> Alertas { get; set; }
    public DbSet<Localizacao> Localizacaos { get; set; }
    public DbSet<Ocorrencia> Ocorrencias { get; set; }
    public DbSet<Vitima> Vitimas { get; set; }

}