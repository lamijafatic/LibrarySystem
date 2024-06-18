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
    public partial class Brisanje_narudzbe : Form
    {
        public Brisanje_narudzbe()
        {
            InitializeComponent();
        }
      /*  public static String konekcioniString = "Server=localhost; Port=3306; Database=projekat; Uid=root; Pwd=";*/

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Brisanje_narudzbe_Load(object sender, EventArgs e)
        {
            try
            {

                String query = "SELECT k.ime, k.prezime ,n.narudzbenica_id AS 'ID narudzbenice', n.kupac_id AS 'ID kupca',n.datum_narudzbe AS 'Datum narudzbe' FROM kupac k, narudzbenica n" +
                    " WHERE k.kupac_id=n.kupac_id";
                MySqlConnection konekcija = new MySqlConnection(Login_forma.konekcioniString);
                konekcija.Open();
               
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(query, konekcija);
                
                DataTable tabela = new DataTable();
                
                dataAdapter.Fill(tabela);
                
                dataGridView1.DataSource = tabela;
                dataAdapter.Dispose();
             }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                String query = "DELETE FROM narudzbenica WHERE narudzbenica_id= '" + textBoxIDN.Text + "';";
                MySqlConnection konekcija = new MySqlConnection(Login_forma.konekcioniString);
                konekcija.Open();
                MySqlCommand cmd = new MySqlCommand(query, konekcija);

                /*MySqlDataReader reader;
                reader = cmd.ExecuteReader();
                reader.Read();*/

                cmd.ExecuteNonQuery();
                //reader.Close();
                MessageBox.Show("Narudzbenica ID=" + textBoxIDN.Text + " je izbrisana!");
                konekcija.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Brisanje_narudzbeFormClosed(object sender, FormClosedEventArgs e)
        {
            //Login_forma fr7 = new Login_forma();
           // fr7.Close();
            //Application.Exit(); // Exit the application
        }

        private void artikliToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dodavanje_artikala fr11 = new Dodavanje_artikala();
            this.Hide();
            fr11.Show();
        }

        private void kupciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rad_sa_kupcima fr13 = new Rad_sa_kupcima();
            this.Hide();
            fr13.Show();
        }

        private void Brisanje_narudzbe_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
