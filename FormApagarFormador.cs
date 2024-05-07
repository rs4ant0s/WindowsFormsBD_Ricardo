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
    public partial class FormApagarFormador : Form
    {
        DBConnect ligacao = new DBConnect();
        public FormApagarFormador()
        {
            InitializeComponent();
        }

        private void FormApagarFormador_Load(object sender, EventArgs e)
        {

            txtNome.ReadOnly = true;
            mtxtNIF.ReadOnly = true;
            mtxtDataNascimento.ReadOnly = true;
            dateTimePicker1.Visible = false;
            cmbArea.Enabled = false;
            cmbUser.Enabled = false;
            btnEliminar.Enabled = false;
            this.AcceptButton = this.btnPesquisa;
           
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {

            string nome = "", Nif = "", data_nascimento = "", ID_area = "", ID_utilizador = "";


            if (ligacao.PesquisaFormador(nudID.Value.ToString(), ref nome, ref Nif, ref data_nascimento, ref ID_area, ref ID_utilizador))
            {

                cmbArea.Text = ligacao.DevolveromboArea(ID_area);
                cmbUser.Text = ligacao.DevolveromboUser(ID_utilizador);
                txtNome.Text = nome;
                mtxtNIF.Text = Nif;
                mtxtDataNascimento.Text = data_nascimento;
                btnEliminar.Enabled = true;
                txtNome.ReadOnly = true;
                mtxtNIF.ReadOnly = true;
                mtxtDataNascimento.ReadOnly = true;

            }
            else
            {
                MessageBox.Show("Formador não encontrado!");
                Limpar();
            }

        }
        private void Limpar()
        {
            nudID.Value = 0;
            txtNome.Text = string.Empty;
            mtxtNIF.Clear();
            //dateTimePicker1.Value = DateTime.Now;
            mtxtDataNascimento.Clear();
            cmbUser.SelectedIndex = -1;
            cmbArea.SelectedIndex = -1;
         
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja eliminar o Formador com ID : " + nudID.Value.ToString(), "Eliminar",
              MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                if (ligacao.DeleteFormador(nudID.Value.ToString()))
                {
                    MessageBox.Show("Registo eliminado!");
                    Limpar();
                    nudID.Focus();
                }
                else
                {
                    MessageBox.Show("Não foi possível eliminar o registo!");
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
