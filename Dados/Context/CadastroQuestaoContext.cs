using Dados.Models;
using Microsoft.EntityFrameworkCore;


namespace Dados.Context
{
    public class CadastroQuestaoContext : DbContext
    {
        public CadastroQuestaoContext(DbContextOptions<CadastroQuestaoContext>
            options) : base(options)
        { }

        DbSet<Questao> Questoes { get; set; }
    }
}
