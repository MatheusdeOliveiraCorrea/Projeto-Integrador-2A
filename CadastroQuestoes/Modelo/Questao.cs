using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroQuestoes.Modelo
{
    public class Questao
    {
        public Questao(){}

        public Questao(string titulo, string corpo, string resposta)
        {
            Titulo = titulo; 
            Corpo = corpo;
            Resposta = resposta;
        }

        public int ID { get; set; }

        public string Titulo { get; set; }

        public string Corpo { get; set; }

        public string Resposta { get; set; }
    }
}
