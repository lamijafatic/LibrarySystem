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
    public partial class Kreiranje_narudzbe : Form
    {
        public Kreiranje_narudzbe()
        {
            InitializeComponent();
        }

        public string korisnickiID = Login_forma.korisnickiID;
        public string idzadnjeg = "";
        public int maxArtikalID;
        public int kolicinaSkladiste;
        public int kolicinaKorpa;
      
        private void Form3_Load(object sender, EventArgs e)
        {
            PregledArtikala();
            try
            {
                string upit = "SELECT narudzbenica_id FROM narudzbenica WHERE narudzbenica_id = narudzbenica_id ORDER BY narudzbenica_id Desc ";
                MySqlConnection konekcija = new MySqlConnection(Login_forma.konekcioniString);

                konekcija.Open();
                MySqlCommand cmd = new MySqlCommand(upit, konekcija);

                MySqlDataReader reader = cmd.ExecuteReader();

                reader.Read();
                idzadnjeg = (Convert.ToInt32(reader[0]) + 1).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            PrikazKorpe();

        }
        private void PregledArtikala()
        {
            try
            {

                string kveri = "SELECT artikal.artikal_id AS 'ID artikla',artikal.naziv_artikla AS 'Naziv artila',skladiste.kolicina_stanje as 'Kolicina', artikal.cijena as 'Cijena' FROM skladiste, artikal WHERE artikal.artikal_id = skladiste.artikal_id";

                MySqlConnection konekcija = new MySqlConnection(Login_forma.konekcioniString);

                konekcija.Open();

                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(kveri, konekcija);

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
 

        private void PrikazKorpe()
        {

            try
            {

                string upit = "SELECT sn.artikal_id AS 'ID artikla', a.naziv_artikla as 'Naziv artikla', sn.kolicina as 'Kolicina', a.cijena * sn.kolicina as 'Ukupno' FROM stavka_narudzbenice sn, artikal a WHERE sn.narudzbenica_id = " +idzadnjeg + " AND sn.artikal_id = a.artikal_id ";

                MySqlConnection konekcija = new MySqlConnection(Login_forma.konekcioniString);

                konekcija.Open();

                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(upit, konekcija);

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

        private void KreirajNarudzbu()
        {
            try
            {
                
               try
                {
                    string upit = "SELECT narudzbenica_id FROM narudzbenica WHERE narudzbenica_id = narudzbenica_id ORDER BY narudzbenica_id Desc ";
                    MySqlConnection konekcija = new MySqlConnection(Login_forma.konekcioniString);

                    konekcija.Open();
                    MySqlCommand cmd = new MySqlCommand(upit, konekcija);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    reader.Read();
                    idzadnjeg = (Convert.ToInt32(reader[0]) + 1).ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
                

                string kveri = "INSERT INTO narudzbenica VALUES (" + idzadnjeg + ", " + korisnickiID + ", '" + DateTime.Now.ToString("yyyy-MM-dd") + "' ) ";

                MySqlConnection konekcija2 = new MySqlConnection(Login_forma.konekcioniString);
                konekcija2.Open();

                MySqlCommand cmdDva = new MySqlCommand(kveri, konekcija2);
                cmdDva.ExecuteNonQuery();
                textBox3.Text = "";
                textBox2.Text = "";
                textBox1.Text = "";
                MessageBox.Show("Narudžba je uspješno realizovana!");
                konekcija2.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void KorpaDodaj()
        {
            if (Convert.ToInt32(textBox1.Text) > NepostojeciArtikalID())
            {
                MessageBox.Show("Artikal sa unesenim ID-om ne postoji.");
            }
            else if (provjeriKolicinuSkladiste() < Convert.ToInt32(textBox2.Text))
            {
                MessageBox.Show("Unesena količina nije dostupna u skladištu.");
            }
            else
            {
                string kveri1 = "UPDATE skladiste SET kolicina_stanje = kolicina_stanje - " + textBox2.Text + " WHERE artikal_id = " + textBox1.Text + "";
                string kveri2 = "";

                if (ProvjeriArtikal() == 0)
                {
                    kveri2 = "INSERT INTO stavka_narudzbenice (narudzbenica_id, artikal_id, kolicina) VALUES (" +idzadnjeg+ ", " + textBox1.Text + ", " + textBox2.Text + ")";
                }
                else
                {
                    kveri2= "UPDATE stavka_narudzbenice SET kolicina = kolicina + " + textBox2.Text + " WHERE artikal_id = " + textBox1.Text + "";
                }

                MySqlConnection konekcija = new MySqlConnection(Login_forma.konekcioniString);
                konekcija.Open();

                MySqlCommand cmd = new MySqlCommand(kveri1, konekcija);
                MySqlCommand cmd2 = new MySqlCommand(kveri2, konekcija);

                cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();

                konekcija.Close();
            }
        }

        private void KorpaObrisi()
        {
            if (provjerKolicinuKorpa() < Convert.ToInt32(textBox2.Text))
            {
                MessageBox.Show("Broj artikala koji želite obrisati je veća nego količina koja se nalazi u korpi.");
            }

            else
            {
                string kveri1 = "UPDATE skladiste SET kolicina_stanje = kolicina_stanje + " + textBox2.Text + " WHERE artikal_id = " + textBox1.Text + "";
                string kveri2 = "UPDATE stavka_narudzbenice SET kolicina = kolicina - " + textBox2.Text + " WHERE artikal_id = " + textBox1.Text + "";

                MySqlConnection konekcija = new MySqlConnection(Login_forma.konekcioniString);
                konekcija.Open();

                MySqlCommand cmd = new MySqlCommand(kveri1, konekcija);
                MySqlCommand cmd2 = new MySqlCommand(kveri2, konekcija);

                cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();

                konekcija.Close();
            }
        }

        private int ProvjeriArtikal()
        {
            int broj = 0;
            try
            {
                string upit = "SELECT artikal_id FROM stavka_narudzbenice WHERE narudzbenica_id = " +idzadnjeg+ " AND artikal_id = " + textBox1.Text + "";
                MySqlConnection konekcija = new MySqlConnection(Login_forma.konekcioniString);
                konekcija.Open();
                MySqlCommand cmd = new MySqlCommand(upit, konekcija);
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows) broj = Convert.ToInt32(reader[0]);

                konekcija.Close();
                reader.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (broj == 0) return 0;
            else return broj;
        }

        private int NepostojeciArtikalID()
        {
            try
            {
                string upit = "SELECT artikal_id FROM artikal WHERE artikal_id = artikal_id ORDER BY artikal_id Desc ";
                MySqlConnection konekcija = new MySqlConnection(Login_forma.konekcioniString);

                konekcija.Open();
                MySqlCommand cmd = new MySqlCommand(upit, konekcija);

                MySqlDataReader reader = cmd.ExecuteReader();

                reader.Read();
                maxArtikalID = Convert.ToInt32(reader[0]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return maxArtikalID;
        }

        private int provjeriKolicinuSkladiste()
        {
            try
            {
                string upit= "SELECT kolicina_stanje FROM skladiste WHERE kolicina_stanje = kolicina_stanje";
                MySqlConnection konekcija = new MySqlConnection(Login_forma.konekcioniString);

                konekcija.Open();
                MySqlCommand cmd = new MySqlCommand(upit, konekcija);

                MySqlDataReader reader = cmd.ExecuteReader();

                reader.Read();
                kolicinaSkladiste = Convert.ToInt32(reader[0]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return kolicinaSkladiste;
        }

        private int provjerKolicinuKorpa()
        {
            try
            {
                string upit = "SELECT kolicina FROM stavka_narudzbenice WHERE narudzbenica_id = " +idzadnjeg+ " AND artikal_id = " + textBox1.Text + "";
                MySqlConnection konekcija = new MySqlConnection(Login_forma.konekcioniString);

                konekcija.Open();
                MySqlCommand cmd = new MySqlCommand(upit, konekcija);

                MySqlDataReader reader = cmd.ExecuteReader();

                reader.Read();
                kolicinaKorpa = Convert.ToInt32(reader[0]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return kolicinaKorpa;
        }

        private void Total()
        {
            String kveri = "SELECT a.cijena, sn.kolicina FROM stavka_narudzbenice sn, artikal a, narudzbenica n WHERE n.narudzbenica_id=sn.narudzbenica_id AND sn.artikal_id=a.artikal_id AND " +
                "sn.narudzbenica_id='" +idzadnjeg+ "'";
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

                textBox3.Text = total.ToString();
                reader.Close();
                konekcija.Close();
                textBox1.Text = "";
                textBox2.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void Form5_Load(object sender, EventArgs e)
        {
            PregledArtikala();
        }

        private void buttonDodaj_Click(object sender, EventArgs e)
        {
            Total();
            KorpaDodaj();
            PrikazKorpe();
            PregledArtikala();
            
        }

        private void buttonObrisi_Click(object sender, EventArgs e)
        {
            KorpaObrisi();
            PrikazKorpe();
            PregledArtikala();
            Total();
        }

      

       

        private void Form5_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void buttonKreiraj_Click(object sender, EventArgs e)
        {
            KreirajNarudzbu();
            PrikazKorpe();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            KorpaDodaj();
            PrikazKorpe();
            PregledArtikala();
            Total();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            KorpaObrisi();
            PrikazKorpe();
            PregledArtikala();
            Total();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            KreirajNarudzbu();
            PrikazKorpe();
        }

        private void naručiToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void prikazNarudžbiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 forma = new Form1();
            forma.Show();
            this.Hide();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Kreiranje_narudzbe_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }


  
        
    }
}
