using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CadastroQuestoes
{
    public partial class TelaPrincipal : Form
    {
        public TelaPrincipal()
        {
            InitializeComponent();
        }

        private void AoClicarSair(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AoClicarEmSalvar(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarTituloCorpo())
                    throw new Exception("POR FAVOR, COLOQUE UM TITULO E UM CORPO");

                using (SqlConnection conexaoSql = new SqlConnection("Server=DESKTOPMATHEUS\\FACULDADE;Database=bancoquestoes;Trusted_Connection=True;"))
                {
                    using (var cmd = new SqlCommand("insert into Questoes values (@titulo, @corpo, @resposta);", conexaoSql))
                    {
                        cmd.Parameters.AddWithValue("@titulo", txtTitulo.Text);
                        cmd.Parameters.AddWithValue("@corpo", txtCorpo.Text);
                        if (string.IsNullOrEmpty(txtResposta.Text)) cmd.Parameters.AddWithValue("@resposta", null);
                        cmd.Parameters.AddWithValue("@resposta", txtResposta.Text);

                        conexaoSql.Open();
                        cmd.ExecuteNonQuery();
                        conexaoSql.Close(); 
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERRO");
            }
        }

        private void ValidarTituloSaida(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCorpo.Text))
            {
                errorProvider1.SetError(txtTitulo, "NÃO PODE SER VAZIO");
            }
            else
            {
                errorProvider1.Clear();
                txtTitulo.Text = Regex.Replace(txtTitulo.Text, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
            }
        }

        private void AoValidarCorpoEntrada(object sender, EventArgs e)
        {
            var somenteLeitura = string.IsNullOrEmpty(txtTitulo.Text)? txtCorpo.ReadOnly = true : txtCorpo.ReadOnly = false;
        }

        public bool ValidarTituloCorpo()
        {
            return !(string.IsNullOrEmpty(txtTitulo.Text) || string.IsNullOrEmpty(txtCorpo.Text));
        }
    }
}
