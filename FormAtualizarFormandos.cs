using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WindowsFormsBD.DBConnect;

namespace WindowsFormsBD
{
    public partial class FormAtualizarFormandos : Form
    {
        DBConnect ligacao = new DBConnect();
        string contactoAux = "";
        public FormAtualizarFormandos()
        {
            InitializeComponent();
        }

        private void FormAtualizarFormandos_Load(object sender, EventArgs e)
        {
            //txtNome.ReadOnly = true;
            //txtMorada.ReadOnly = true;
            //mtxtContacto.ReadOnly = true;
            //mtxtIBAN.ReadOnly = true;
            //rbFeminino.Enabled = false;
            //rbMasculino.Enabled = false;
            //rbOutro.Enabled = false;
            //mtxtDataNascimento.ReadOnly = true;
            //dateTimePicker1.Visible = false;

            DesativarControlos();

            btnAtualizar.Enabled = false;

            this.AcceptButton = this.btnPesquisa;

            ligacao.PreenchercomboNacionalidade(ref cmbNacionalidade);
        }

        private void DesativarControlos()
        {
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
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            string nome = "", morada = "", contacto = "", iban = "", data_nascimento = "", IDnacionalidade ="";
            char genero = ' ';
            

            if (ligacao.PesquisaFormando(nudID.Value.ToString(), ref nome, ref morada, ref contacto,
                ref iban, ref genero, ref data_nascimento, ref IDnacionalidade))
            {
                txtNome.Text = nome;
                txtMorada.Text = morada;
                mtxtContacto.Text = contacto;
                mtxtIBAN.Text = iban;
                cmbNacionalidade.Text =ligacao.DevolveromboNacionalidade(IDnacionalidade);
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

                groupBox3.Enabled = false;
                btnAtualizar.Enabled = true;

                txtNome.ReadOnly = false;
                txtMorada.ReadOnly = false;
                mtxtContacto.ReadOnly = false;
                mtxtIBAN.ReadOnly = false;
                rbFeminino.Enabled = true;
                rbMasculino.Enabled = true;
                rbOutro.Enabled = true;
                mtxtDataNascimento.ReadOnly = false;
                cmbNacionalidade.Enabled= true;

            }
            else
            {
                MessageBox.Show("Formando não encontrado!");
                Limpar();
            }
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

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            if (VerificarCampos())
            {
               
                if (ligacao.Update(nudID.Value.ToString(), txtNome.Text, txtMorada.Text, contactoAux, mtxtIBAN.Text, Genero(),
                   DateTime.Parse(mtxtDataNascimento.Text).ToString("yyyy-MM-dd"), ItemCMB()))
                {
                    MessageBox.Show("Atualizado com sucesso!");
                    btnCancelar_Click(sender, e);
                    
                }
                else
                {
                    MessageBox.Show("Erro na actualização do registo!");
                }

            }
        }

        private char Genero()
        {
            char genero = 'T';

            if (rbFeminino.Checked)
            {
                genero = 'F';
            }
            else if (rbMasculino.Checked)
            {
                genero = 'M';
            }
            else if (rbOutro.Checked)
            {
                genero = 'O';
            }
            return genero;
        }

        private bool VerificarCampos()
        {
            if (nudID.Value == 0)
            {
                MessageBox.Show("Erro no campo ID!");
                nudID.Focus();
                return false;
            }

            txtNome.Text = Geral.TirarEspacos(txtNome.Text);
            if (txtNome.Text.Length < 3)
            {
                MessageBox.Show("Erro no campo Nome!");
                txtNome.Focus();
                return false;
            }

            txtMorada.Text = Geral.TirarEspacos(txtMorada.Text);
            if (txtMorada.Text.Length < 3)
            {
                MessageBox.Show("Erro no campo Morada!");
                txtMorada.Focus();
                return false;
            }

            
            contactoAux = mtxtContacto.Text.Replace(" ", "");
            if (contactoAux.Length != 0 && contactoAux.Length != 9)
            {
                MessageBox.Show("Erro no campo Contacto!");
                mtxtContacto.Focus();
                return false;
            }

            if (mtxtIBAN.Text.Length < 25)
            {
                MessageBox.Show("Erro no campo IBAN!");
                mtxtIBAN.Focus();
                return false;
            }

            if (Genero() == 'T')
            {
                MessageBox.Show("Erro no campo Gémero!");
                rbFeminino.Focus();
                return false;
            }

            if (mtxtDataNascimento.Text.Length != 10 || Geral.CheckDate(mtxtDataNascimento.Text) == false)
            {
                MessageBox.Show("Erro no campo Data Nascimento!");
                mtxtDataNascimento.Focus();
                return false;
            }

            if (cmbNacionalidade.SelectedIndex == -1)
            {

                MessageBox.Show("Erro no campo Nacionaliadde!");
                cmbNacionalidade.Focus();
                return false;
            }


            return true;
        }

        private string ItemCMB()
        {
            // Recebe o item selecionado no ComboBox
            NacionalidadeItem itemSelecionado = (NacionalidadeItem)cmbNacionalidade.SelectedItem;
            // Dividindo a representação do item em uma matriz de palavras
            string[] palavras = itemSelecionado.ToString().Split('-');
            // Selecionando a última palavra
            string ultimaPalavra = palavras[palavras.Length - 1].Trim();
            return ultimaPalavra;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            groupBox3.Enabled = true;
            btnAtualizar.Enabled = false;
            nudID.Focus();

            Limpar();
            DesativarControlos();

        }

        private void cmbNacionalidade_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
