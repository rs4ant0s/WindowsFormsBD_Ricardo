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
using static WindowsFormsBD.DBConnect;

namespace WindowsFormsBD
{
    public partial class FormAtualizarFormador : Form
    {
        DBConnect ligacao = new DBConnect();
        private string nifAux = "";
        public FormAtualizarFormador()
        {
            InitializeComponent();
        }

        private void FormAtualizarFormador_Load(object sender, EventArgs e)
        {
            DesativarControlos();
            
            this.AcceptButton = this.btnPesquisa;
            ligacao.PreenchercomboArea(ref cmbArea);
            ligacao.PreenchercomboUser(ref cmbUser);
        }
        private void DesativarControlos()
        {
            txtNome.ReadOnly = true;
            mtxtNIF.ReadOnly = true;
            mtxtDataNascimento.ReadOnly = true;
            dateTimePicker1.Visible = false;
            cmbArea.Enabled = false;
            cmbUser.Enabled = false;
            btnAtualizar.Enabled = false;
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            string nome = "", Nif = "", data_nascimento = "", ID_area = "", ID_utilizador = "";


            if (ligacao.PesquisaFormador(nudID.Value.ToString(), ref nome, ref Nif, ref data_nascimento, ref ID_area, ref ID_utilizador))
            {

                txtNome.Text = nome;
                mtxtNIF.Text = Nif;
                mtxtDataNascimento.Text = data_nascimento;
                cmbArea.Text = ligacao.DevolveromboArea(ID_area);
             
                cmbUser.Text = ligacao.DevolveromboUser(ID_utilizador);
        

                txtNome.ReadOnly = false;
                mtxtNIF.ReadOnly = false;
                mtxtDataNascimento.ReadOnly = false;
                cmbArea.Enabled = true;
                cmbUser.Enabled = true;
                btnAtualizar.Enabled = true;
               

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
            mtxtDataNascimento.Clear();
            cmbUser.SelectedIndex = -1;
            cmbArea.SelectedIndex = -1;

        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            if (VerificarCampos())
            {
                
                if (ligacao.UpdateFormador(nudID.Value.ToString(), txtNome.Text, nifAux, DateTime.Parse(mtxtDataNascimento.Text).ToString("yyyy-MM-dd"), ItemCMBArea(), ItemCMBUser()))
                {
                    MessageBox.Show("Atualizado com sucesso!");
                    Limpar();
                }
                else
                {
                    MessageBox.Show("Erro na actualização do registo!");
                }

            }
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

            nifAux = mtxtNIF.Text.Replace(" ", "");
            if (nifAux.Length != 0 && nifAux.Length != 9)
            {
                MessageBox.Show("Erro no campo NIF!");
                mtxtNIF.Focus();
                return false;
            }

            if (mtxtDataNascimento.Text.Length != 10 || Geral.CheckDate(mtxtDataNascimento.Text) == false)
            {
                MessageBox.Show("Erro no campo Data Nascimento!");
                mtxtDataNascimento.Focus();
                return false;
            }
            if (cmbArea.SelectedIndex == -1)
            {

                MessageBox.Show("Erro no campo Area!");
                cmbArea.Focus();
                return false;
        }

            if (cmbUser.SelectedIndex == -1)
            {
                MessageBox.Show("Erro no campo User!");
                cmbUser.Focus();
                return false;
            }



            return true;
        }

        private string ItemCMBArea()
        {
            // Recebe o item selecionado no ComboBox
            AreaItem itemSelecionado = (AreaItem)cmbArea.SelectedItem;
            // Dividindo a representação do item em uma matriz de palavras
            string[] palavras = itemSelecionado.ToString().Split('-');
            // Selecionando a última palavra
            string ultimaPalavra = palavras[palavras.Length - 1].Trim();
           
            
            return ultimaPalavra;
        }

        private string ItemCMBUser()
        {
            // Recebe o item selecionado no ComboBox
            UserItem itemSelecionado = (UserItem)cmbUser.SelectedItem;
            // Dividindo a representação do item em uma matriz de palavras
            string[] palavras = itemSelecionado.ToString().Split('-');
            // Selecionando a última palavra
            string ultimaPalavra = palavras[palavras.Length - 1].Trim();
            return ultimaPalavra;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
 
            btnAtualizar.Enabled = false;
            nudID.Focus();
            Limpar();
            DesativarControlos();
        }
    }
}
