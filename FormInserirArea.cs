using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsBD
{
    public partial class FormInserirArea : Form
    {
        DBConnect ligacao = new DBConnect();
        public FormInserirArea()
        {
            InitializeComponent();
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            if (VerificarCampos())
            {

                if (ligacao.InsertArea( txtArea.Text))
                {
                    MessageBox.Show("Área gravada com sucesso!");
                    txtArea.Text = string.Empty;
                    txtArea.Focus();
                }
                else
                {
                    MessageBox.Show("Erro na gravação da Área!");
                }
            }

        }

        private bool VerificarCampos()
        {
            txtArea.Text = Geral.TirarEspacos(txtArea.Text); // colocado apos correção
            if (txtArea.Text.Length > 100)
            {
                MessageBox.Show("Erro: A área deve conter até 100 carateres!");
                txtArea.Focus();
                return false;
            }
            
            return true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
