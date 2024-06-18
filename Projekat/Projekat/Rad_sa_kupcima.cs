using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApplication1
{
    public partial class Rad_sa_kupcima : Form
    {
        public Rad_sa_kupcima()
        {
            InitializeComponent();
        }
      /*  public static String konekcioniStrin = "Server=localhost; Port=3306; Database=projekat; Uid=root; Pwd=";*/
        


        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            //treba uraditi izlazak     
            Application.Exit();
            
        }
       
        private void buttonTrazi_Click_1(object sender, EventArgs e)
        {
            Prikaz_kupaca();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Rad_sa_kupcima_Load(object sender, EventArgs e)
        {
            Prikaz_kupaca();
        }
        private void buttonAzuriranje_Click_1(object sender, EventArgs e)
        {
            String kveri = "UPDATE kupac SET " +
                      " ime='" + textBoximee.Text + "', " +
                    " prezime='" + textBoxprezimee.Text + "', " +
                    " grad='" + textBoxgradd.Text + "', " +
                    " adresa='" + textBoxadresaa.Text + "', " +
                    " telefon='" + textBoxtelefonn.Text + "', " +
                    " user='" + textBoxkorimee.Text + "', " +
                    " pass='" + textBoxsifraa.Text + "' " +
                    " WHERE kupac_id='" + textBoxID.Text + "' ";

            try
            {

                MySqlConnection konekcija = new MySqlConnection(Login_forma.konekcioniString);
                konekcija.Open();
                MySqlCommand cmd = new MySqlCommand(kveri, konekcija);

                /*MySqlDataReader reader;
                reader = cmd.ExecuteReader();
                reader.Read();*/

                cmd.ExecuteNonQuery();
                //reader.Close();
                MessageBox.Show("Podaci za kupca ID=" + textBoxID.Text + " su ažurirani!");
                konekcija.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonKreiranje_Click_1(object sender, EventArgs e)
        {
            String kveri = "INSERT INTO kupac(ime, prezime, grad, adresa, telefon, user, pass) VALUES " +
                   " ('" + textBoximee.Text + "', '" + textBoxprezimee.Text + "', '" + textBoxgradd.Text + "', " +
                    " '" + textBoxadresaa.Text + "', '" + textBoxtelefonn.Text + "', '" + textBoxkorimee.Text + "', '" + textBoxsifraa.Text + "') ";

            try
            {

                MySqlConnection konekcija = new MySqlConnection(Login_forma.konekcioniString);
                konekcija.Open();
                MySqlCommand cmd = new MySqlCommand(kveri, konekcija);
                //MySqlDataReader reader;
                //reader = cmd.ExecuteReader();
                //reader.Read();
                cmd.ExecuteNonQuery();
                //reader.Close();
                MessageBox.Show("Novi korisnik je dodan!");
                konekcija.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void narudzbeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Brisanje_narudzbe fr5 = new Brisanje_narudzbe();
            fr5.Show();
        }

        private void artikliToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dodavanje_artikala fr9 = new Dodavanje_artikala();
            this.Hide();
            fr9.Show();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private void Prikaz_kupaca()
        {

            try {
                String upit = "SELECT kupac.kupac_id,ime,prezime,grad,adresa,telefon,user,pass FROM kupac";
                /*if (textBoxIME.Text != "")
                {
                    upit += " WHERE ime LIKE '" + textBoxIME.Text + "%'";
                }
                if (textBoxPREZIME.Text != "")
                {
                    upit += " WHERE prezime LIKE '" + textBoxPREZIME.Text + "%'";
                }*/
                if ((textBoxIME.Text != "") && (textBoxPREZIME.Text != ""))
                {
                    upit += " WHERE ime LIKE '" + textBoxIME.Text + "%' AND prezime LIKE '" + textBoxPREZIME.Text + "%'";
                }
                if ((textBoxIME.Text != "") && (textBoxPREZIME.Text == ""))
                {
                    upit += " WHERE ime LIKE '" + textBoxIME.Text + "%'";
                }
                if ((textBoxPREZIME.Text != "") && (textBoxIME.Text == ""))
                {
                    upit += " WHERE prezime LIKE '" + textBoxPREZIME.Text + "%'";
                }
             
                
                MySqlConnection konekcija = new MySqlConnection(Login_forma.konekcioniString);
                konekcija.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(upit, konekcija);
                DataTable tabela = new DataTable();
                adapter.Fill(tabela);
                dataGridView1.DataSource = tabela;
                adapter.Dispose();
            
            
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
        
        
        
        }

        private void menuStrip1_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Rad_sa_kupcima_Load_1(object sender, EventArgs e)
        {
            Prikaz_kupaca();
        }

        private void Rad_sa_kupcima_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

       /* private void textBoxIME_TextChanged(object sender, EventArgs e)
        {
            Prikaz_kupaca();
        }
        private void textBoxPREZIME_TextChanged(object sender, EventArgs e)
        {
            Prikaz_kupaca();
        }*/
    }
}
