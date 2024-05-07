using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsBD
{
    public partial class FormInserirNacionalidade : Form
    {
        DBConnect ligacao = new DBConnect();
        

        public FormInserirNacionalidade()
        {
            InitializeComponent();
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            if (VerificarCampos())
            {
               
                if (ligacao.InsertNacionalidade(txtALF2.Text, txtNacionalidade.Text))
                {
                    MessageBox.Show("Nacionalidade gravada com sucesso!");
                    Limpar();
                    txtALF2.Focus();
                }
                else
                {
                    MessageBox.Show("Erro na gravação da nacionalidade!");
                }
            }
        }

        private bool VerificarCampos()
        {
            txtNacionalidade.Text = Geral.TirarEspacos(txtNacionalidade.Text); // colocado apos correção
            if (txtNacionalidade.Text.Length > 100)
            {
                MessageBox.Show("Erro: A nacionalidade deve conter até 100 carateres!");
                txtNacionalidade.Focus();
                return false;
            }
            txtALF2.Text = Geral.TirarEspacos(txtALF2.Text);  // colocado apos correção em aula
            if (txtALF2.Text.Length != 2)
            {
                MessageBox.Show("Erro no campo ALF2 tem de colcar 2 caracteres pelo menos!");
                txtALF2.Focus();
                return false;
            }
            return true;
        }

        private void Limpar()
        { 
            txtNacionalidade.Text = string.Empty;
            txtALF2.Text = string.Empty;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnGravar_MouseUp(object sender, MouseEventArgs e)
        /*exmplo de como utilizar o click drt ou esq neste caso como temos a função click ele vai
         * dar sempre prioridade mas se apagar a função em cima e deixar esta ele funciona */
        {
            //if (e.Button == MouseButtons.Right)
            //{
            //    Close() ;
            //}
            //if (e.Button == MouseButtons.Left)
            //{
            //    Limpar();
            //}
            switch (e.Button) 
            {
                case MouseButtons.Left:
                    Limpar();
                    break;
                case MouseButtons.Right:
                    Close();
                    break;
            }
        }

        private void FormInserirNacionalidade_Load(object sender, EventArgs e)
        {

        }
    }
}
