using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics.Contracts;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using iTextSharp.text.pdf;

namespace WindowsFormsBD
{
    internal class DBConnect
    {
        private MySqlConnection connection;
        private string server;
        private string username;
        private string password;
        private string database;
        private string port;

        public DBConnect()
        {
            Initialize();
        }
        private void Initialize()
        {
            server = "192.168.1.151";
            database = "gestaoformandos";
            username = "ricardo";
            password = "********";
            port = "3306";

            string connectionString = "Server = " + server + ";Port = " + port + ";Database = " +
                database + "; Uid = " + username + "; Pwd = " + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public string StatusConnection()
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                else
                {
                    connection.Close();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            return connection.State.ToString();
        }

        public int Count()
        {
            int count = -1;
            string query = "select count(*) from formando";
            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    count = int.Parse(cmd.ExecuteScalar().ToString());
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
            return count;
        }

        public void Insert_old()
        {
            string query = "Insert into formando (nome, id_formando, morada, contacto, iban, sexo, data_nascimento) values " +
                " ('Pinto da Costa','10099', 'Rua do Porto', NULL, '0000000000000000000000000', 'M', '1950-06-22')";

            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();

                CloseConnection();

            }
        }

        public void Insert()
        {
            string query = "Insert into formando (nome, id_formando, morada, contacto, iban, sexo, data_nascimento) values " +
                " ('Pinto da Costa','10099', 'Rua do Porto', NULL, '0000000000000000000000000', 'M', '1950-06-22')";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                CloseConnection();
            }

        }

        public bool Insert(string ID, string nome, string morada, string contacto, string iban, char sexo, string data_nascimento, string IDnacionalidade)
        {
            string query = "insert into formando (id_formando, nome, morada, contacto, iban, sexo, data_nascimento, id_nacionalidade) values ('" +
               ID + "', '" + nome + "', '" + morada + "', '" + contacto + "', '" + iban + "', '" + sexo + "', '" + data_nascimento + "', '" + IDnacionalidade + "');";

            bool flag = true;

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                flag = false;
            }
            finally
            {
                CloseConnection();
            }

            return flag;

        }

        public bool InsertFormador(string Nome, string NIF,string data_nascimento, string IDArea, string IDUser)
        {
            string query = "insert into formador (id_formador, nome, nif, dataNascimento, id_area, id_utilizador) values (0, '" + 
                Nome + "', '" + NIF + "', '" + data_nascimento + "', '" + IDArea + "', '" + IDUser + "');";

            bool flag = true;

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                flag = false;
            }
            finally
            {
                CloseConnection();
            }

            return flag;

        }

        public bool InsertNacionalidade(string ALF2, string nacionalidade)
        {
            string query = "insert into Nacionalidade (id_nacionalidade, alf2, nacionalidade) values (0, '" + ALF2 + "', '" + nacionalidade + "');";

            bool flag = true;

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                flag = false;
            }
            finally
            {
                CloseConnection();
            }
            return flag;

        }

        public bool Delete()
        {
            string query = "delete from formando where id_formando = '34738'";

            bool flag = true;

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                flag = false;
            }
            finally
            {
                CloseConnection();
            }

            return flag;

        }


        public bool Update(string ID, string nome, string morada, string contacto, string iban, char sexo, string data_nascimento, string id_nacionalidade)
        {
            string query = "update formando set nome = '" + nome + "', morada = '" + morada + "', contacto = '" +
                contacto + "' , iban = '" + iban + "' , sexo = '" + sexo + "', data_nascimento = '" + data_nascimento + "', id_nacionalidade = '" + id_nacionalidade +
                "' where id_formando = " + ID;

            bool flag = true;

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                flag = false;
            }
            finally
            {
                CloseConnection();
            }

            return flag;

        }

        public bool UpdateFormador(string ID_formador, string Nome, string Nif, string data_nascimento, string id_area, string id_utilizador)
        {
            string query = "update formador set nome = '" + Nome + "', nif = '" + Nif + "', dataNascimento = '" + data_nascimento + "', id_area = '" + id_area + "', id_utilizador = '" + id_utilizador +
                "'  where id_formador = " + ID_formador;

            bool flag = true;

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                flag = false;
            }
            finally
            {
                CloseConnection();
            }

            return flag;

        }

        public bool UpdateNacionalidade(string ID_nacionalidade, string ALF2, string Nacionalidade)
        {
            string query = "update Nacionalidade set alf2 = '" + ALF2 + "', nacionalidade = '" + Nacionalidade +
                "' where id_nacionalidade = " + ID_nacionalidade;

            bool flag = true;

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                flag = false;
            }
            finally
            {
                CloseConnection();
            }

            return flag;

        }

        public bool Delete(string id)
        {
            string query = "delete from formando where id_formando = '" + id + "'";

            bool flag = true;

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                flag = false;
            }
            finally
            {
                CloseConnection();
            }

            return flag;

        }

        public bool DeleteNacionalidade(string id)
        {
            string query = "delete from Nacionalidade where id_nacionalidade = '" + id + "'";

            bool flag = true;

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                flag = false;
            }
            finally
            {
                CloseConnection();
            }

            return flag;

        }

        public bool DeleteFormador(string id)
        {
            string query = "delete from formador where id_formador = '" + id + "'";

            bool flag = true;

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                flag = false;
            }
            finally
            {
                CloseConnection();
            }

            return flag;

        }

        public bool Update(string id, string nome)
        {
            string query = "update formando set nome = '" + nome + "' where id_formando = '" + id + "'";

            bool flag = true;

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                flag = false;
            }
            finally
            {
                CloseConnection();
            }

            return flag;

        }

        public int DevolveUltimoID()
        {
            int ultimoID = 0;

            string query = "select max(id_formando) from formando";
            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    //ultimoID = int.Parse(cmd.ExecuteScalar().ToString());
                    int.TryParse(cmd.ExecuteScalar().ToString(), out ultimoID);
                    ultimoID++;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            //catch 
            //{
            //    //MessageBox.Show("ERRO!");
            //}
            finally
            {
                CloseConnection();
            }

            return ultimoID;
        }

        public int DevolveUltimoIDFormador()
        {
            int ultimoID = 0;

            string query = "select max(id_formador) from formador";
            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    //ultimoID = int.Parse(cmd.ExecuteScalar().ToString());
                    int.TryParse(cmd.ExecuteScalar().ToString(), out ultimoID);
                    ultimoID++;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            //catch 
            //{
            //    //MessageBox.Show("ERRO!");
            //}
            finally
            {
                CloseConnection();
            }

            return ultimoID;
        }

        public bool PesquisaFormando(string ID_pesquisa, ref string nome, ref string morada, ref string contacto,
            ref string iban, ref char sexo, ref string data_nascimento, ref string IDnacionalidade)
        {
            bool flag = false;

            string query = "select nome, morada, contacto, iban, sexo, data_nascimento, id_nacionalidade from formando " +
                 "where id_formando = '" + ID_pesquisa + "'";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        nome = dataReader[0].ToString();
                        morada = dataReader["morada"].ToString();
                        contacto = dataReader[2].ToString();
                        iban = dataReader[3].ToString();
                        sexo = dataReader["sexo"].ToString()[0];
                        data_nascimento = dataReader[5].ToString();
                        IDnacionalidade = dataReader[6].ToString();
                        flag = true;
                    }
                    dataReader.Close();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
            return flag;
        }

        public bool PesquisaNacionalidade(string ID_nacionalidade, ref string ALF2, ref string Nacionalidade)
        {
            bool flag = false;

            string query = "select alf2, nacionalidade from Nacionalidade " +
                 "where id_nacionalidade = '" + ID_nacionalidade + "'";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        ALF2 = dataReader["alf2"].ToString();
                        Nacionalidade = dataReader["nacionalidade"].ToString();
                        flag = true;
                    }
                    dataReader.Close();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
            return flag;
        }

        public bool PesquisaFormador(string ID_formador, ref string Nome, ref string Nif, ref string data_nascimento,
            ref string ID_area, ref string ID_utilizador)
        {
            bool flag = false;

            string query = "select nome, nif, dataNascimento, id_area, id_utilizador from formador where id_formador = '" + ID_formador + "'";
            //MessageBox.Show(query);
            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Nome = dataReader[0].ToString();
                        Nif = dataReader[1].ToString();
                        data_nascimento = dataReader[2].ToString();
                        ID_area = dataReader[3].ToString();
                        ID_utilizador = dataReader[4].ToString();
                        //MessageBox.Show(Nome + " " + Nif + " " + data_nascimento + " " + ID_area+" "+ID_utilizador);
                        flag = true;
                    }
                    dataReader.Close();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
            return flag;
        }

        public void PreencherDataGridviewFormandos(ref DataGridView dgv)
        {
            string query = "select id_formando, nome, iban, contacto, sexo from formando order by nome;";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dr = cmd.ExecuteReader();

                    int idxLinha = 0;
                    while (dr.Read())
                    {
                        dgv.Rows.Add();
                        dgv.Rows[idxLinha].Cells["codID"].Value = dr[0].ToString();
                        dgv.Rows[idxLinha].Cells["Nome"].Value = dr["nome"].ToString();
                        dgv.Rows[idxLinha].Cells[2].Value = dr[2].ToString();
                        dgv.Rows[idxLinha].Cells[3].Value = dr["contacto"].ToString();
                        dgv.Rows[idxLinha].Cells["Genero"].Value = dr["sexo"].ToString();
                        idxLinha++;
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                CloseConnection();
            }

        }

        public void PreencherDataGridviewNacionalidade(ref DataGridView dgv)
        {
            string query = "select id_nacionalidade, alf2, nacionalidade from nacionalidade order by nacionalidade;";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dr = cmd.ExecuteReader();

                    int idxLinha = 0;
                    while (dr.Read())
                    {
                        dgv.Rows.Add();
                        dgv.Rows[idxLinha].Cells[0].Value = dr[0].ToString();
                        dgv.Rows[idxLinha].Cells[1].Value = dr[1].ToString();
                        dgv.Rows[idxLinha].Cells[2].Value = dr[2].ToString();
                       
                        idxLinha++;
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                CloseConnection();
            }

        }


        public void PreencherDataGridviewFormador(ref DataGridView dgv)
        {
            string query = "select id_formador, nome, nif, id_area, id_utilizador from formador order by nome;";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dr = cmd.ExecuteReader();

                    int idxLinha = 0;
                    while (dr.Read())
                    {
                        dgv.Rows.Add();
                        dgv.Rows[idxLinha].Cells[0].Value = dr[0].ToString();
                        dgv.Rows[idxLinha].Cells[1].Value = dr[1].ToString();
                        dgv.Rows[idxLinha].Cells[2].Value = dr[2].ToString();
                        dgv.Rows[idxLinha].Cells[3].Value = dr[3].ToString();
                        dgv.Rows[idxLinha].Cells[4].Value = dr[4].ToString();

                        idxLinha++;
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                CloseConnection();
            }

        }

        public class NacionalidadeItem
        {
            public int IdNacionalidade { get; set; }
            public string Alf2 { get; set; }
            public string Nacionalidade { get; set; }

            public NacionalidadeItem(string nacionalidade, string alf2, int idNacionalidade)
            {
                IdNacionalidade = idNacionalidade;
                Alf2 = alf2;
                Nacionalidade = nacionalidade;
            }

            public override string ToString()
            {
                return $"{Nacionalidade}-{Alf2}-{IdNacionalidade} ";
            }
        }

        public void PreenchercomboNacionalidade(ref System.Windows.Forms.ComboBox cmb)
        {
            string query = "select nacionalidade,  alf2, id_nacionalidade  from Nacionalidade order by nacionalidade;";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dr = cmd.ExecuteReader();

                    cmb.Items.Clear();

                    while (dr.Read())
                    {
                        string nacionalidade = dr.GetString("nacionalidade");
                        string alf2 = dr.GetString("alf2");
                        int idNacionalidade = dr.GetInt32("id_nacionalidade");


                        NacionalidadeItem item = new NacionalidadeItem(nacionalidade, alf2, idNacionalidade);

                        cmb.Items.Add(item);
                    }

                    dr.Close();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public string DevolveromboNacionalidade(string id_nacionalidade)
        {
            string query = "select nacionalidade,  alf2, id_nacionalidade from Nacionalidade where id_nacionalidade = " + id_nacionalidade;

            string nacionalidade = "";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        nacionalidade = dr[2].ToString() + " - " +
                            dr[1].ToString() + " - " + dr[0].ToString();
                    }

                    dr.Close();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
            return nacionalidade;
        }

        public void PesquisarFormandosPorNomeGenero(ref DataGridView dgv, char genero, string nome)
        {

            string query = "SELECT id_formando, nome, iban, contacto, sexo FROM formando ";
            if (genero != 'T')
            {
                query = query + " where sexo = '" + genero + "'";
            }

            if (nome.Length > 0 && genero != 'T')
            {
                query = query + " and nome like '%" + nome + "%'";
            }

            else if (nome.Length > 0)
            {
                query = query + " where nome like '%" + nome + "%'";
            }

            query = query + " order by nome;";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dr = cmd.ExecuteReader();

                    int idxLinha = 0;
                    dgv.Rows.Clear(); // Limpa as linhas existentes antes de adicionar novas
                    while (dr.Read())
                    {
                        dgv.Rows.Add();
                        dgv.Rows[idxLinha].Cells["codID"].Value = dr["id_formando"].ToString();
                        dgv.Rows[idxLinha].Cells["Nome"].Value = dr["nome"].ToString();
                        dgv.Rows[idxLinha].Cells[2].Value = dr["iban"].ToString();
                        dgv.Rows[idxLinha].Cells[3].Value = dr["contacto"].ToString();
                        dgv.Rows[idxLinha].Cells["Genero"].Value = dr["sexo"].ToString();
                        idxLinha++;
                    }
                    dr.Close();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public void PesquisarFormandosPorNomeGeneroNacionalidade(ref DataGridView dgv, char genero, string nome, string id_nacionalidade)
        {

            string query = "select id_formando, nome, morada, contacto, iban, sexo, data_nascimento, nacionalidade, id_nacionalidade from vw_formando_nacionalidade";

            bool flag = false;

            if (genero != 'T')
            {
                query = query + " where sexo = '" + genero + "'";
                flag = true;
            }

            if (nome.Length > 0 && genero != 'T')
            {
                query = query + " and nome like '%" + nome + "%'";
            }
            else if (nome.Length > 0)
            {
                query = query + " where nome like '%" + nome + "%'";
                flag = true;
            }

            if (id_nacionalidade.Length > 0 && flag == true)
            {
                query = query + " and id_nacionalidade = '" + id_nacionalidade + "'";
            }
            else if (id_nacionalidade.Length > 0)
            {
                query = query + " where  id_nacionalidade = '" + id_nacionalidade + "'";
                //MessageBox.Show(query);
            }

            query = query + " order by nome;";
           

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dr = cmd.ExecuteReader();

                    int idxLinha = 0;
                    dgv.Rows.Clear(); // Limpa as linhas existentes antes de adicionar novas
                    while (dr.Read())
                    {
                        dgv.Rows.Add();
                        dgv.Rows[idxLinha].Cells[0].Value = dr[0].ToString();
                        dgv.Rows[idxLinha].Cells[1].Value = dr[1].ToString();
                        dgv.Rows[idxLinha].Cells[2].Value = dr[4].ToString();
                        dgv.Rows[idxLinha].Cells[3].Value = dr[5].ToString();
                        dgv.Rows[idxLinha].Cells[4].Value = dr[7].ToString();
                        idxLinha++;
                    }
                    dr.Close();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                CloseConnection();
            }


        }

        public void PesquisarFormadorcmbAreacmbUser(ref DataGridView dgv, string nome, string id_Area, string id_User)
        {

            string query = "select id_formador, nome, nif, area, nome_utilizador from vw_formador_area";

            bool flag = false;

            if (nome.Length > 0)
            {
                query = query + " where nome like '%" + nome + "%'";
                flag = true;
            }

            if (id_Area.Length > 0 && flag == true)
            {
                query = query + " and area = '" + id_Area + "'";
            }
            else if (id_Area.Length > 0)
            {
                query = query + " where  area = '" + id_Area + "'";
                
            }


            if (id_User.Length > 0 && flag == true)
            {
                query = query + " and nome_utilizador = '" + id_User + "'";
            }
            else if (id_User.Length > 0)
            {
                query = query + " where nome_utilizador = '" + id_User + "'";
            }

            query = query + " order by nome;";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dr = cmd.ExecuteReader();

                    int idxLinha = 0;
                    dgv.Rows.Clear(); // Limpa as linhas existentes antes de adicionar novas
                    while (dr.Read())
                    {
                        dgv.Rows.Add();
                        dgv.Rows[idxLinha].Cells[0].Value = dr[0].ToString();
                        dgv.Rows[idxLinha].Cells[1].Value = dr[1].ToString();
                        dgv.Rows[idxLinha].Cells[3].Value = dr[3].ToString();
                        dgv.Rows[idxLinha].Cells[4].Value = dr[4].ToString();
                        idxLinha++;
                    }
                    dr.Close();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                CloseConnection();
            }


        }


        public bool ValidateUser(string userName, string passWord, ref string id_user)
        {
            bool flag = false;

            string query = "select userRole from users" +
                " where binary uname = '" + userName + "' and pwd = sha2( '" + passWord + "',512);";
            //MessageBox.Show(userName);
            //MessageBox.Show(passWord);
            //MessageBox.Show(query);

            //try
            //{
            //    if (OpenConnection())
            //    {
            //        MySqlCommand cmd = new MySqlCommand(query, connection);
            //        MySqlDataReader dataReader = cmd.ExecuteReader();
            //        while (dataReader.Read())
            //        {
            //            id_user = dataReader["userRole"].ToString();
            //            flag = true;
            //        }
            //        dataReader.Close();
            //    }
            //}
            //catch (MySqlException ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //finally
            //{
            //    CloseConnection();
            //}
            //return flag;
            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    if (cmd.ExecuteScalar().ToString() != null)
                    {
                        id_user = cmd.ExecuteScalar().ToString();
                        flag = true;
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
            return flag;
        }

        //public string DevolverUserName(string UNAME, string PWD)
        //{
        //    string resultadoUser = "";
        //    string query = "select uname from users where binary uname = '" + UNAME  + "' AND pwd = SHA2('" + PWD + "', 512)";

        //    try
        //    {
        //        if (OpenConnection())
        //        {
        //            MySqlCommand cmd = new MySqlCommand(query, connection);
        //            MySqlDataReader dr = cmd.ExecuteReader();

        //            if (dr.Read()) 
        //            {
        //                resultadoUser = dr["uname"].ToString();
        //            }

        //            dr.Close();
        //        }
        //    }
        //    catch (MySqlException ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    finally
        //    {
        //        CloseConnection();
        //    }
        //    return resultadoUser;
        //}

        public bool ValidateUser2(string userName, string passWord, ref string id_user)
        {
            bool flag = false;

            try
            {
                string query = "select id_utilizador from utilizador where binary nome_utilizador = '" + userName + "' and palavra_passe = " +
                "sha2( '" + passWord + "',512) and estado = 'A';";

                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    if (cmd.ExecuteScalar() != null)
                    {
                        id_user = cmd.ExecuteScalar().ToString();
                        flag = true;
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                CloseConnection();
                if (flag)
                {
                    PUSuccessLogin(userName, "S");
                }
                else
                {
                    PUSuccessLogin(userName, "U");
                }

            }
            return flag;
        }

        private void PUSuccessLogin(string userName, string result)
        {
            try
            {
                string query = "call pUSuccessLogin('" + userName + "', '" + result + "');";

                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                CloseConnection();
            }

        }

        public bool ValidateUserStatus(string userName, ref int nfalhas)
        {
            bool flag = false;

            try
            {
                string query = "select falhas from utilizador where nome_utilizador = '" + userName + "' and estado = 'I';";

                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    if (cmd.ExecuteScalar() != null)
                    {
                        nfalhas = int.Parse(cmd.ExecuteScalar().ToString());
                        flag = true;
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
            return flag;
        }

        public bool InsertArea(string Area)
        {
            string query = "insert into area (id_area, area) values (0, '" + Area + "');";

            bool flag = true;

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                flag = false;
            }
            finally
            {
                CloseConnection();
            }
            return flag;

        }

        public string DevolveromboArea(string id_area)
        {
            string query = "select id_area, area  from area where id_area = " + id_area;

            string area = "";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        area = dr[1].ToString() + "-" + dr[0].ToString();
                    }

                    dr.Close();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
            return area;
        }
        public void PreenchercomboArea(ref System.Windows.Forms.ComboBox cmb)
        {
            string query = "select area, id_area  from area order by id_area;";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dr = cmd.ExecuteReader();

                    cmb.Items.Clear();

                    while (dr.Read())
                    {
                        string area = dr.GetString("area");
                        int idArea = dr.GetInt32("id_area");


                        AreaItem item = new AreaItem(area, idArea);

                        cmb.Items.Add(item);
                    }

                    dr.Close();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public class AreaItem
        {
            public int IdArea { get; set; }
            public string Area { get; set; }

            public AreaItem(string area, int idArea)
            {
                IdArea = idArea;
                Area = area;
            }
            public override string ToString()
            {
                return $"{Area}-{IdArea}";
            }
        }

        public bool PesquisaArea(string ID_area, ref string Area)
        {
            bool flag = false;

            string query = "select area from area " +
                 "where id_area = '" + ID_area + "'";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Area = dataReader["area"].ToString();
                        flag = true;
                    }
                    dataReader.Close();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
            return flag;
        }

        public bool UpdateArea(string ID_area, string Area)
        {
            string query = "update area set area = '" + Area +
                "' where id_area = " + ID_area;

            bool flag = true;

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                flag = false;
            }
            finally
            {
                CloseConnection();
            }

            return flag;

        }

        public void PreencherDataGridviewArea(ref DataGridView dgv)
        {
            string query = "select id_area, area from area order by id_area;";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dr = cmd.ExecuteReader();

                    int idxLinha = 0;
                    while (dr.Read())
                    {
                        dgv.Rows.Add();
                        dgv.Rows[idxLinha].Cells[0].Value = dr[0].ToString();
                        dgv.Rows[idxLinha].Cells[1].Value = dr[1].ToString();
                        
                        idxLinha++;
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                CloseConnection();
            }

        }

        public bool DeleteArea(string id)
        {
            string query = "delete from area where id_area = '" + id + "'";

            bool flag = true;

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                flag = false;
            }
            finally
            {
                CloseConnection();
            }

            return flag;

        }

        public void PreenchercomboUser(ref System.Windows.Forms.ComboBox cmb)
        {
            string query = "select id_utilizador, nome_utilizador from utilizador order by id_utilizador;";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dr = cmd.ExecuteReader();

                    cmb.Items.Clear();

                    while (dr.Read())
                    {
                        int Iduser = dr.GetInt32("id_utilizador");
                        string name = dr.GetString("nome_utilizador");
                       


                        UserItem item = new UserItem(name, Iduser);

                        cmb.Items.Add(item);
                    }

                    dr.Close();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public string DevolveromboUser(string id_utilizador)
        {
            string query = "select id_utilizador, nome_utilizador from utilizador where id_utilizador = " + id_utilizador;
            //MessageBox.Show(query);
            string utilizador = "";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        utilizador = dr[1].ToString() + "-" + dr[0].ToString();
                        //MessageBox.Show(utilizador);
                    }

                    dr.Close();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
            return utilizador;
        }



        public class UserItem
        {
           
            int IDUser { get; set; }
            public string Uname { get; set; }
         

            public UserItem(string uname, int iduser)
            {
                IDUser = iduser;
                Uname = uname;
            }
            public override string ToString()
            {
                return $"{Uname}-{IDUser}";
            }
        }

    }
}
