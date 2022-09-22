using System;
using System.Globalization;
using System.Linq;
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

        private void aoClicarSair(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void aoDigitarAlgoTitulo(object sender, KeyPressEventArgs e)
        {
            txtCorpo.Text = $"{txtTitulo.Text.Length + 1} Letra Digitada";

        }

        private void ao(object sender, EventArgs e)
        {
            txtTitulo.Text = Regex.Replace(txtTitulo.Text, @"(^\w)|(\s\w)", m => m.Value.ToUpper());

        }
    }
}
