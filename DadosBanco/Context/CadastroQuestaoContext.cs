using DadosBanco.Models;
using Microsoft.EntityFrameworkCore;


namespace DadosBanco.Context
{
    public class CadastroQuestaoContext : DbContext
    {
        public CadastroQuestaoContext(DbContextOptions<CadastroQuestaoContext>
            options) : base(options)
        { }

        DbSet<Questao> Questoes { get; set; }
    }
}
