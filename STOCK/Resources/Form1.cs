using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using WMPLib;

namespace STOCK
{
    public partial class Form1 : Form
    {
        WindowsMediaPlayer player = new WindowsMediaPlayer();
        private OleDbConnection conexao;
        private OleDbCommand comando;
        private string strcon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=dados.mdb";

        public Form1()
        {
            InitializeComponent();
            //txtprocurar.Visible = false;
            Carrega();
        }
        public void Limpar() 
        {
            txtdescricao.Clear();
            txtcategoria.Clear();
            txtpreco.Clear();

            txtdescricao.Focus();
        }

        public void Carrega()
        {
            try
            {
                conexao = new OleDbConnection(strcon);
                conexao.Open();
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter("SELECT * FROM produtos ", conexao);
                DataTable Formatabela = new DataTable();
                dataAdapter.Fill(Formatabela);

                tabela.Rows.Clear();
                for (int i = 0; i < Formatabela.Rows.Count; i++)
                {
                    tabela.Rows.Add(
                        Formatabela.Rows[i][0],
                        Formatabela.Rows[i][1],
                        Formatabela.Rows[i][2],
                        Formatabela.Rows[i][3]);
                }
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            player.URL = "CLICK12A.wav";
            player.controls.play();
            string descricao, categoria;
            int preco;




            descricao = txtdescricao.Text;

            categoria = txtcategoria.Text;

            preco = Convert.ToInt32(txtpreco.Text);

            try
            {


                conexao = new OleDbConnection(strcon);
                conexao.Open();

                comando = new OleDbCommand("INSERT INTO produtos(descricao, preco, categoria)VALUES('" + descricao + "','" +
                    preco + "','" + categoria + "')", conexao);
                comando.ExecuteNonQuery();

               
                MessageBox.Show("Dados registados com sucesso!", "Registado!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Carrega();
                conexao.Close();
            }





        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            player.URL = "CLICK12A.wav";
            player.controls.play();
            try
            {

                conexao = new OleDbConnection(strcon);
                conexao.Open();
                OleDbCommand actualizar = new OleDbCommand("DELETE FROM produtos WHERE id=" + txtid.Text + "", conexao);

                actualizar.ExecuteNonQuery();

                MessageBox.Show("Dados eliminados com sucesso!", "Eliminado!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("Ocorreu um erro ao tentar eliminar o registo\n" + ex.Message);
            }
            finally
            {
                Carrega();
                conexao.Close();
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            player.URL = "CLICK12A.wav";
            player.controls.play();
            Limpar();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            player.URL = "CLICK12A.wav";
            player.controls.play();

            Application.Exit();
        }

        private void tabela_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ////int id;
            //string id;
            ////id = Convert.ToInt32(txtid.Text);

            //id = txtid.Text; 

            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            txtid.Text = tabela.Rows[e.RowIndex].Cells[0].Value.ToString() + "";
            txtdescricao.Text = tabela.Rows[e.RowIndex].Cells[1].Value.ToString() + "";
            txtpreco.Text = tabela.Rows[e.RowIndex].Cells[2].Value.ToString() + "";
            txtcategoria.Text = tabela.Rows[e.RowIndex].Cells[3].Value.ToString() + "";
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

            player.URL = "CLICK12A.wav";
            player.controls.play();
            try
            {

                conexao = new OleDbConnection(strcon);
                conexao.Open();
                OleDbCommand actualizar = new OleDbCommand("UPDATE produtos SET descricao='" + txtdescricao.Text + "', preco='" + txtpreco.Text + "', categoria='" + txtcategoria.Text
                    + "' WHERE ID=" + txtid.Text + "", conexao);

                actualizar.ExecuteNonQuery();

                MessageBox.Show("Dados alterados com sucesso!", "Registado!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("Ocorreu um erro ao tentar actualizar o registo\n" + ex.Message);
            }
            finally
            {
                Carrega();
                conexao.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //txtprocurar.Visible = true;
        }

        private void button1_DoubleClick(object sender, EventArgs e) 
        {
            txtprocurar.Visible = false;
        }

        private void txtprocurar_KeyUp(object sender, KeyEventArgs e)
        {
                try
                {
                    conexao = new OleDbConnection(strcon);
                    conexao.Open();
                    OleDbDataAdapter dataAdapter = new OleDbDataAdapter("SELECT * FROM produtos Where ID like '" + txtprocurar.Text+ "%'", conexao);
                    DataTable Formatabela = new DataTable();
                    dataAdapter.Fill(Formatabela);

                    tabela.Rows.Clear();
                    for (int i = 0; i < Formatabela.Rows.Count; i++)
                    {
                        tabela.Rows.Add(
                            Formatabela.Rows[i][0],
                            Formatabela.Rows[i][1],
                            Formatabela.Rows[i][2],
                            Formatabela.Rows[i][3]);
                    }
                }
                catch (OleDbException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conexao.Close();
                    Carrega();
                }
            
            
        }

        //private void Carrega(string p)
        //{
        //    throw new NotImplementedException();
        //}

        //private void Carrega(string p)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Carrega(string r)
        //{
        //    try

        //    {
        //        conexao = new OleDbConnection(strcon);
        //        conexao.Open();
        //        OleDbDataAdapter dataAdapter = new OleDbDataAdapter("SELECT * FROM produtos Where ID like '"+r+ "%'", conexao);
        //        DataTable Formatabela = new DataTable();
        //        dataAdapter.Fill(Formatabela);

        //        tabela.Rows.Clear();
        //        for (int i = 0; i < Formatabela.Rows.Count; i++)
        //        {
        //            tabela.Rows.Add(
        //                Formatabela.Rows[i][0],
        //                Formatabela.Rows[i][1],
        //                Formatabela.Rows[i][2],
        //                Formatabela.Rows[i][3]);
        //        }
        //    }
        //    catch (OleDbException ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    finally
        //    {
        //        conexao.Close();
        //        Carrega();
        //    }
        //}

        private void txtprocurar_KeyPress(object sender, KeyPressEventArgs e)
        {
            


        }

        private void txtprocurar_Click(object sender, EventArgs e)
        {
            txtprocurar.Clear();
        }


    }
}

