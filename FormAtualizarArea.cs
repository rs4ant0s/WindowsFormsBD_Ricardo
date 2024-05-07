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
    public partial class FormAtualizarArea : Form
    {
        DBConnect ligacao = new DBConnect();
        public FormAtualizarArea()
        {
            InitializeComponent();
        }

        private void FormAtualizarArea_Load(object sender, EventArgs e)
        {
            carrega_combobox();
        }

        private void carrega_combobox()
        {
            ligacao.PreenchercomboArea(ref cmbArea);
            groupBox3.Enabled = false;
            btnAtualizar.Enabled = false;
        }

        private void cmbArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Area = "", ID_Area = Ultimapalavra();

            ligacao.PesquisaArea(ID_Area, ref Area);

            txtArea.Text = Area;
            btnAtualizar.Enabled = true;
            groupBox3.Enabled = true;

        }

        private string Ultimapalavra()
        {
            // Recebe o item selecionado no ComboBox
            AreaItem itemSelecionado = (AreaItem)cmbArea.SelectedItem;
            // Dividindo a representação do item em uma matriz de palavras
            string[] palavras = itemSelecionado.ToString().Split('-');
            // Selecionando a última palavra
            string ultimaPalavra = palavras[palavras.Length - 1].Trim();
            return ultimaPalavra;
        }

        private bool VerificarCampos()
        {

            if (txtArea.Text.Length > 100)
            {
                MessageBox.Show("Erro: A Area deve conter até 100 carateres!");
                txtArea.Focus();
                return false;
            }

            return true;
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            if (VerificarCampos())
            {
                if (ligacao.UpdateArea(Ultimapalavra(), txtArea.Text))
                {
                    MessageBox.Show("Atualizado com sucesso!");

                    cmbArea.Items.Clear();
                    carrega_combobox();
                    txtArea.Text = "";
                }
                else
                {
                    MessageBox.Show("Erro na actualização da Area!");
                }


            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

}
