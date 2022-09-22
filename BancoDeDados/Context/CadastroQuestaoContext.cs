using BancoDeDados.Models;
using Microsoft.EntityFrameworkCore;


namespace BancoDeDados.Context
{
    public class CadastroQuestaoContext : DbContext
    {
        public CadastroQuestaoContext() :base()
        {}

        DbSet<Questao> Questoes { get; set; }    
    }
}
