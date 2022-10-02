namespace CadastroQuestoes
{
    public static class ConstantesSQL
    {
        public static string StringDeConexao = "Server=DESKTOPMATHEUS;Database=projetoIntegrador2a;Trusted_Connection=True;";
        public static string AdicionarConexao = "insert into Questoes values (@titulo, @corpo, @resposta);";
        public static string ObterTodasQuestoes = "select * from Questoes;";
    }
}
