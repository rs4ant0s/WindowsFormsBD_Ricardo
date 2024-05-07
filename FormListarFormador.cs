using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WindowsFormsBD.DBConnect;

namespace WindowsFormsBD
{
    public partial class FormListarFormador : Form
    {
        DBConnect ligacao = new DBConnect();
        public FormListarFormador()
        {
            InitializeComponent();
        }

        private void FormListarFormador_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            dataGridView1.Columns.Add("codID", "ID");
            dataGridView1.Columns.Add("Nome", "Nome");
            dataGridView1.Columns.Add("nif", "NIF");
            dataGridView1.Columns.Add("area", "Area");
            dataGridView1.Columns.Add("user", "User");
            ligacao.PreenchercomboUser(ref cmbUser);
            ligacao.PreenchercomboArea(ref cmbArea);
            ligacao.PreencherDataGridviewFormador(ref dataGridView1);
            lblRegistos.Text = "Nº Registos: " + dataGridView1.RowCount.ToString();

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
           
            dataGridView1.Rows.Clear();
            txtNome.Text = "";
            cmbArea.SelectedIndex = -1;
            cmbUser.SelectedIndex = -1;
            ligacao.PreenchercomboUser(ref cmbUser);
            ligacao.PreenchercomboArea(ref cmbArea);
            ligacao.PreencherDataGridviewFormador(ref dataGridView1);
            lblRegistos.Text = "Nº Registos: " + dataGridView1.RowCount.ToString();
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            string id_Area = "";
            string id_User = "";
            dataGridView1.Rows.Clear();

            if (cmbArea.SelectedIndex != -1)
            {

                id_Area = ItemCMBArea();
           
            }

            if (cmbUser.SelectedIndex != -1)
            {
                
                id_User = ItemCMBUser();
            
            }

            txtNome.Text = Geral.TirarEspacos(txtNome.Text);
            ligacao.PesquisarFormadorcmbAreacmbUser(ref dataGridView1, txtNome.Text, id_Area, id_User);
            lblRegistos.Text = "Nº Registos: " + dataGridView1.RowCount.ToString();
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

        private void btnImprime_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF (*.pdf)|*.pdf";
                sfd.FileName = "Formadores.pdf";
                bool fileError = false;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException)
                        {
                            fileError = true;
                            MessageBox.Show("Impossível de apagar o ficheiro!");
                        }
                    }
                    //if (!fileError == true)
                    //if (fileError == false)
                    if (!fileError)
                    {
                        try
                        {
                            PdfPTable pdfPTable = new PdfPTable(dataGridView1.Columns.Count);
                            pdfPTable.DefaultCell.Padding = 3;
                            pdfPTable.WidthPercentage = 100;
                            pdfPTable.HorizontalAlignment = Element.ALIGN_LEFT;

                            foreach (DataGridViewColumn column in dataGridView1.Columns)
                            {
                                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                                pdfPTable.AddCell(cell);
                            }

                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    pdfPTable.AddCell(cell.Value.ToString());
                                }
                            }

                            //using (FileStream stream = new FileStream(sfd.FileName, FileMode.Create))

                            FileStream stream = new FileStream(sfd.FileName, FileMode.Create);
                            //{
                            Document pdfDoc = new Document(PageSize.A4, 10f, 20f, 20f, 10f);
                            PdfWriter.GetInstance(pdfDoc, stream);
                            pdfDoc.Open();
                            pdfDoc.Add(pdfPTable);
                            pdfDoc.Close();
                            stream.Close();
                            //}

                            MessageBox.Show("Imprimiu com sucesso!");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("ERROR: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Não existe registos!");
            }
        }
    }
}