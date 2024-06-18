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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
          
        public string korisnickiID =Login_forma.korisnickiID;

        private void PrikazNarudzbi()
        {
            try
            {
                string upit = "SELECT narudzbenica_id as 'ID narudzbe', datum_narudzbe as 'Datum narudzbe' FROM narudzbenica WHERE kupac_id ='"+ korisnickiID+"' ORDER BY datum_narudzbe DESC" ;

                MySqlConnection konekcija = new MySqlConnection(Login_forma.konekcioniString);

                konekcija.Open();

                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(upit, konekcija);

                DataTable tabela = new DataTable();

                dataAdapter.Fill(tabela);
                dataGridView1.DataSource = tabela;
                dataAdapter.Dispose();
                konekcija.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void PrikazStavkeNarudzbe() {
            try
            {
                string kveri = "SELECT a.naziv_artikla AS 'Naziv artikla', sn.kolicina AS 'Kolicina', a.cijena * sn.kolicina as 'Ukupno' FROM stavka_narudzbenice sn, artikal a WHERE a.artikal_id = sn.artikal_id AND sn.narudzbenica_id = " + textBox1.Text ;

                MySqlConnection konekcija = new MySqlConnection(Login_forma.konekcioniString);

                konekcija.Open();

                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(kveri, konekcija);

                DataTable tabela = new DataTable();

                dataAdapter.Fill(tabela);
                dataGridView2.DataSource = tabela;
                dataAdapter.Dispose();
                konekcija.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Total()
        {
            String kveri = "SELECT a.cijena, sn.kolicina FROM stavka_narudzbenice sn, artikal a, narudzbenica n WHERE n.narudzbenica_id=sn.narudzbenica_id AND sn.artikal_id=a.artikal_id AND " +
                "sn.narudzbenica_id='" + textBox1.Text + "'";
            double total = 0;

            try
            {
                MySqlConnection konekcija = new MySqlConnection(Login_forma.konekcioniString);
                konekcija.Open();
                
                MySqlCommand cmd = new MySqlCommand(kveri, konekcija);
                MySqlDataReader reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    double cijena = Convert.ToDouble(reader[0]);
                    double kolicina = Convert.ToDouble(reader[1]);
                    
                    double racun = cijena * kolicina;
                    total += racun;
                }

                reader.Close();
                konekcija.Close();
                
                textBox2.Text = total.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

   

      

        private void Form6_Load(object sender, EventArgs e)
        {
            PrikazNarudzbi();
        }

        private void Form6_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            PrikazStavkeNarudzbe();
            Total();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PrikazNarudzbi();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void naručiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Kreiranje_narudzbe forma = new Kreiranje_narudzbe();
            forma.Show();
            this.Hide();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        
    }
}

 