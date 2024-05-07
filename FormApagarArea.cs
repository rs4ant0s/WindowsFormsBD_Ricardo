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
    public partial class FormApagarArea : Form
    {
        DBConnect ligacao = new DBConnect();
        public FormApagarArea()
        {
            InitializeComponent();
        }

        private void FormApagarArea_Load(object sender, EventArgs e)
        {
            Carrega_combobox();
        }

        private void Carrega_combobox()
        {
            ligacao.PreenchercomboArea(ref cmbArea);
            txtArea.ReadOnly = true;
            btnApagar.Enabled = false;
        }

        private void cmbArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Area = "", ID_area = ItemCMB();
            ligacao.PesquisaArea(ID_area, ref Area);
            txtArea.Text = Area;

            btnApagar.Enabled = true;
        }

        private string ItemCMB()
        {
            // Recebe o item selecionado no ComboBox
            AreaItem itemSelecionado = (AreaItem)cmbArea.SelectedItem;
            // Dividindo a representação do item em uma matriz de palavras
            string[] palavras = itemSelecionado.ToString().Split('-');
            // Selecionando a última palavra
            string ultimaPalavra = palavras[palavras.Length - 1].Trim();
            return ultimaPalavra;
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja eliminar a Area com o nrº :  " + ItemCMB(), "Eliminar",
           MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                if (ligacao.DeleteArea(ItemCMB()))
                {
                    MessageBox.Show("Registo eliminado!");
                    txtArea.Text = "";
                    ligacao.PreenchercomboArea(ref cmbArea);
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
    }
    
}
