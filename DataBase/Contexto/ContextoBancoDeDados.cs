using DataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Contexto
{
    public class ContextoBancoDeDados : DbContext
    {
        public ContextoBancoDeDados(DbContextOptions<ContextoBancoDeDados> options) : base(options) { }

        public DbSet<Questao> Questoes { get; set; }
    }
}
