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
    public partial class FormAutenticacao : Form
    {
        DBConnect ligacao = new DBConnect();
        //public string userName = "";
        public FormAutenticacao()
        {
            InitializeComponent();
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            int nfalhas = 0;
            if (ligacao.ValidateUserStatus(txtutilizador.Text, ref nfalhas)) 
            {
                MessageBox.Show("Utilizador Bloqueado! Nº de falhas de autenticação: " + nfalhas+ "\nContacte o Administrador do Sistema");
                return;
            }
            //string id_user = "";

            //if ( ligacao.ValidateUser( txtutilizador.Text, txtpassword.Text, ref id_user))
            //{
            //    DialogResult = DialogResult.OK;
            //}

            //if (ligacao.ValidateUser( txtutilizador.Text, txtpassword.Text, ref userName)) 
            
            if (ligacao.ValidateUser2(txtutilizador.Text, txtpassword.Text, ref Geral.id_user))
            {
                DialogResult = DialogResult.OK;
            }
            else 
            {
                MessageBox.Show("Erro na autenticação");
            }
        }

        private void FormAutenticacao_Load(object sender, EventArgs e)
        {
            txtpassword.Text = "";
            txtutilizador.Text = "";
            AcceptButton = btnlogin;
            ControlBox = false;

        }

        //public string UserName() 
        //{
        //    string UserName = ligacao.DevolverUserName(txtutilizador.Text,txtpassword.Text);

        //    return UserName;

        //}
    }
}
