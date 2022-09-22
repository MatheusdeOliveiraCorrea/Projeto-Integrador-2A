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

        private void AoClicarSair(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void Ao(object sender, EventArgs e)
        {
            txtTitulo.Text = Regex.Replace(txtTitulo.Text, @"(^\w)|(\s\w)", m => m.Value.ToUpper());

        }
    }
}
