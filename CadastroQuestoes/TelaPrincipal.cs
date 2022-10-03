using CadastroQuestoes.Modelo;
using System;
using System.Collections.Generic;
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
                Func<string, string, bool> ValidarVazioNull = (x, y) => { return !(string.IsNullOrEmpty(x) || string.IsNullOrEmpty(y)); };

                if (!ValidarVazioNull(txtTitulo.Text, txtCorpo.Text))
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
            return new Questao()
            {
                ID = leitor.GetInt32(0),
                Titulo = leitor.GetString(1),
                Corpo = leitor.GetString(2),
                Resposta = leitor.GetString(3)
            };
        }

        private void AoClicarExcluir(object sender, EventArgs e)
        {
            if (viewQuestoes.SelectedCells.Count > decimal.Zero)
            {
                var id = viewQuestoes.CurrentRow.Cells[0].Value.ToString();

                using (var con = new SqlConnection(ConstantesSQL.StringDeConexao))
                {
                    using (var cmd = new SqlCommand(ConstantesSQL.DeletarUsuarioPorId, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        this.PorDadosNaGrid();
                    }
                }
            }
        }

        private void AoClicarEmDesenvolvedores(object sender, EventArgs e)
        {
            new Desenvolvedores().ShowDialog();
        }

        private void AoClicarEmObjetivo(object sender, EventArgs e)
        {
            new Objetivo().ShowDialog();
        }
    }
}
