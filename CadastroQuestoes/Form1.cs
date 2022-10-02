using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CadastroQuestoes.Modelo;

namespace CadastroQuestoes
{
    public partial class TelaPrincipal : Form
    {
        public TelaPrincipal()
        {
            InitializeComponent();
            this.PorDadosNaGrid();
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

                using (SqlConnection conexaoSql = new SqlConnection(ConstantesSQL.StringDeConexao))
                {
                    using (var cmd = new SqlCommand(ConstantesSQL.AdicionarConexao, conexaoSql))
                    {
                        var questao = new Questao(txtTitulo.Text, txtCorpo.Text, txtResposta.Text); 

                        cmd.Parameters.AddWithValue("@titulo", questao.Titulo);
                        cmd.Parameters.AddWithValue("@corpo", questao.Corpo);
                        if (string.IsNullOrEmpty(txtResposta.Text)) cmd.Parameters.AddWithValue("@resposta", "'campo vazio....'");
                        if (!string.IsNullOrEmpty(txtResposta.Text)) cmd.Parameters.AddWithValue("@resposta", questao.Resposta);

                        conexaoSql.Open();
                        cmd.ExecuteNonQuery();
                        conexaoSql.Close();
                    }

                    this.PorDadosNaGrid();
                    this.LimparCampos();
                    MessageBox.Show($"A questão {txtTitulo.Text} foi adicionada com sucesso"); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERRO");
            }
        }

        private void ValidarTituloSaida(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitulo.Text))
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
            var somenteLeitura = string.IsNullOrEmpty(txtTitulo.Text) ? txtCorpo.ReadOnly = true : txtCorpo.ReadOnly = false;
        }

        public bool ValidarTituloCorpo()
        {
            return !(string.IsNullOrEmpty(txtTitulo.Text));
        }

        private void AoValidarEntradaResposta(object sender, EventArgs e)
        {
            var validacao = !string.IsNullOrEmpty(txtCorpo.Text);

            if (validacao)
            {
                txtResposta.ReadOnly = false;
                errorProvider1.Clear();
            }

            if (!validacao)
            {
                txtResposta.ReadOnly = true;
                errorProvider1.SetError(txtCorpo, "NÃO PODE SER VAZIO!");
            }
        }

        private void LimparCampos()
        {
            txtTitulo.Text = String.Empty;
            txtCorpo.Text = String.Empty;
            txtResposta.Text = String.Empty; 
        }

        private void PorDadosNaGrid()
        {
            viewQuestoes.DataSource = null;
            viewQuestoes.DataSource = ObterTodos();

            viewQuestoes.Columns[0].Visible = false;
        }

        private List<Questao> ObterTodos()
        {
            using (var con = new SqlConnection(ConstantesSQL.StringDeConexao))
            {
                using (var cmd = new SqlCommand(ConstantesSQL.ObterTodasQuestoes, con))
                {
                    con.Open();
                    SqlDataReader lerDoBancoDeDados = cmd.ExecuteReader();

                    var lista = new List<Questao>();

                    while (lerDoBancoDeDados.Read())
                    {
                        lista.Add(CriarQuestaoDoBancoDeDados(lerDoBancoDeDados));
                    }

                    return lista;
                }
            }
        }

        private Questao CriarQuestaoDoBancoDeDados(SqlDataReader leitor)
        {
            var questao = new Questao();
            questao.ID = leitor.GetInt32(0);
            questao.Titulo = leitor.GetString(1);
            questao.Corpo = leitor.GetString(2);
            questao.Resposta = leitor.GetString(3); 

            return questao;
        }
    }
}
