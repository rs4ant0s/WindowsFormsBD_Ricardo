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
    public partial class FormApagarNacionalidade : Form
    {
        DBConnect ligacao = new DBConnect();
        public FormApagarNacionalidade()
        {
            InitializeComponent();
        }

   
        private void FormApagarNacionalidade_Load(object sender, EventArgs e)
        {
            Carrega_combobox();
        }

        private void cmbNacionalidade_SelectedIndexChanged(object sender, EventArgs e)
        {

            string ALF2 = "", Nacionalidade = "", ID_nacionalidade = ItemCMB();
            ligacao.PesquisaNacionalidade(ID_nacionalidade, ref ALF2, ref Nacionalidade);
            txtALF2.Text = ALF2;
            txtNacionalidade.Text = Nacionalidade;
           
            btnApagar.Enabled = true;
           
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

        private void btnApagar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja eliminar a nacionalidade com o nrº :  " + ItemCMB(), "Eliminar",
             MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                if (ligacao.DeleteNacionalidade(ItemCMB()))
                {
                    MessageBox.Show("Registo eliminado!");
                    txtALF2.Text = "";
                    txtNacionalidade.Text = "";
                    ligacao.PreenchercomboNacionalidade(ref cmbNacionalidade);
                    Carrega_combobox();

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
        private void Carrega_combobox() 
        {
            ligacao.PreenchercomboNacionalidade(ref cmbNacionalidade);
            txtALF2.ReadOnly = true;
            txtNacionalidade.ReadOnly = true;
            btnApagar.Enabled = false;
        }
    }
}
