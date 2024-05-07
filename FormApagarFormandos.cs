using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace WindowsFormsBD
{
    public partial class FormApagarFormandos : Form
    {
        DBConnect ligacao = new DBConnect();
        public FormApagarFormandos()
        {
            InitializeComponent();
        }

        private void FormApagarFormandos_Load(object sender, EventArgs e)
        {
            //groupBox1.Enabled = false;
            txtNome.ReadOnly = true;
            txtMorada.ReadOnly = true;
            mtxtContacto.ReadOnly = true;
            mtxtIBAN.ReadOnly = true;
            rbFeminino.Enabled = false;
            rbMasculino.Enabled = false;
            rbOutro.Enabled = false;
            mtxtDataNascimento.ReadOnly = true;
            dateTimePicker1.Visible = false;
            cmbNacionalidade.Enabled = false;

            btnEliminar.Enabled = false;

            this.AcceptButton = this.btnPesquisa;

            ligacao.PreenchercomboNacionalidade(ref cmbNacionalidade);
        }


        private void Limpar()
        {
            nudID.Value = 0;
            txtNome.Text = string.Empty;
            txtMorada.Text = "";
            mtxtContacto.Clear();
            mtxtIBAN.Text = string.Empty;
            rbFeminino.Checked = false;
            rbMasculino.Checked = false;
            rbOutro.Checked = false;
            //dateTimePicker1.Value = DateTime.Now;
            mtxtDataNascimento.Clear();
            cmbNacionalidade.SelectedIndex = -1;
        }


        private void btnPesquisa_Click_1(object sender, EventArgs e)
        {
            string nome = "", morada = "", contacto = "", iban = "", data_nascimento = "", IDnacionalidade = "";
            char genero = ' ';

            if (ligacao.PesquisaFormando(nudID.Value.ToString(), ref nome, ref morada, ref contacto,
                ref iban, ref genero, ref data_nascimento, ref IDnacionalidade ))
            {
                txtNome.Text = nome;
                txtMorada.Text = morada;
                mtxtContacto.Text = contacto;
                mtxtIBAN.Text = iban;
                cmbNacionalidade.Text = ligacao.DevolveromboNacionalidade(IDnacionalidade); ;
                
                if (genero == 'F')
                {
                    rbFeminino.Checked = true;
                }
                else if (genero == 'M')
                {
                    rbMasculino.Checked = true;
                }
                else if (genero == 'O')
                {
                    rbOutro.Checked = true;
                }
                mtxtDataNascimento.Text = data_nascimento;

                
                btnEliminar.Enabled = true;

                txtNome.ReadOnly = true;
                txtMorada.ReadOnly = true;
                mtxtContacto.ReadOnly = true;
                mtxtIBAN.ReadOnly = true;
                rbFeminino.Enabled = false;
                rbMasculino.Enabled = false;
                rbOutro.Enabled = false;
                mtxtDataNascimento.ReadOnly = true;

            }
            else
            {
                MessageBox.Show("Formando não encontrado!");
                Limpar();
            }
        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja eliminar o registo ID " + nudID.Value.ToString(), "Eliminar",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                if (ligacao.Delete(nudID.Value.ToString()))
                {
                    MessageBox.Show("Registo eliminado!");
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
