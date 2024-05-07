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
    public partial class FormInserirFormador : Form
    {
        DBConnect ligacao = new DBConnect();
        private string nifAux = "";
        public FormInserirFormador()
        {
            InitializeComponent();
        }

        private void FormInserirFormador_Load(object sender, EventArgs e)
        {
           
            nudID.Value = ligacao.DevolveUltimoIDFormador();
            ligacao.PreenchercomboArea(ref cmbArea);
            ligacao.PreenchercomboUser(ref cmbUser);
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            if (VerificarCampos())
            {
               
                if (ligacao.InsertFormador(txtNome.Text, mtxtNIF.Text, DateTime.Parse(mtxtDataNascimento.Text).ToString("yyyy-MM-dd"), ItemCMBArea(),ItemCMBUser()))
                {
                    MessageBox.Show("Gravado com sucesso!");
                    Limpar();
                    
                    nudID.Value = ligacao.DevolveUltimoIDFormador();
                    txtNome.Focus();
                }
                else
                {
                    MessageBox.Show("Erro na gravação do registo!");
                }
            }
        }

        private bool VerificarCampos()
        {
            if (nudID.Value ==  0)
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

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            mtxtDataNascimento.Text = dateTimePicker1.Value.ToShortDateString();
        }

        private void mtxtDataNascimento_TextChanged(object sender, EventArgs e)
        {
            int dia, mes, ano;
            string textoData;
            DateTime data;

            if (mtxtDataNascimento.MaskCompleted == true)
            {
                textoData = mtxtDataNascimento.Text;
                dia = int.Parse(textoData.Substring(0, 2));
                mes = int.Parse(textoData.Substring(3, 2));
                ano = int.Parse(textoData.Substring(6));

                try
                {
                    data = DateTime.Parse(dia + "-" + mes + "-" + ano);
                    dateTimePicker1.Value = data;
                }
                catch
                {
                    MessageBox.Show("Data incorreta!", "Erro!", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    mtxtDataNascimento.Focus();
                }
            }
        }

        private void Limpar()
        {
           
            txtNome.Text = string.Empty;
            mtxtNIF.Clear();
            dateTimePicker1.Value = DateTime.Now;
            mtxtDataNascimento.Clear();
            cmbArea.SelectedIndex = -1;
            cmbUser.SelectedIndex = -1;
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
            Close();
        }
    }

}
