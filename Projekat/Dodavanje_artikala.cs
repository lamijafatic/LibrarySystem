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
    public partial class Dodavanje_artikala : Form
    {
        public Dodavanje_artikala()
        {
            InitializeComponent();
        }
        
       
        private void kupciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rad_sa_kupcima fr2 = new Rad_sa_kupcima();
            this.Hide();
            fr2.Show();
        }
      
      

        private void narudzbeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Brisanje_narudzbe fr4 = new Brisanje_narudzbe();
            this.Hide();
            fr4.Show();
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void buttonTrazi_Click(object sender, EventArgs e)
        {
            PrikaziPodatke();
        }
        private void PrikaziPodatke()
        {
            try {
                String upit = "SELECT artikal.artikal_id,naziv_artikla,vrsta_artikla,cijena FROM artikal";
                if ((textBoxIDA.Text != "")&&(textBoxNazivA.Text!=""))
                {
                    upit += " WHERE artikal.artikal_id LIKE '" + textBoxIDA.Text + "%' AND artikal.naziv_artikla LIKE '"+textBoxNazivA.Text+"%'";
                }
                if ((textBoxNazivA.Text != "")&&(textBoxIDA.Text==""))
                {
                    upit += " WHERE naziv_artikla LIKE '" + textBoxNazivA.Text + "%'";
                }
                if ((textBoxIDA.Text!="")&&(textBoxNazivA.Text=="")){
                    upit += " WHERE artikal_id LIKE '" + textBoxIDA.Text + "%'";
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
       /* private void Dodavanje_artikala_FormClosed(object sender, EventArgs e)
        {
            Form1 fr1 = new Form1();
            Brisanje_narudzbe fr2 = new Brisanje_narudzbe();
            Dodavanje_artikala fr3 = new Dodavanje_artikala();
            Form2 fr4 = new Form2();
            Kreiranje_narudzbe fr5 = new Kreiranje_narudzbe();
            Login_forma fr6 = new Login_forma();
            Rad_sa_kupcima fr7 = new Rad_sa_kupcima();
            /*fr1.Close();
            fr2.Close();
            fr3.Close();
            fr4.Close();
            fr5.Close();
            fr6.Close();
            fr7.Show();
            this.Close();
           
        }*/
        

        private void Dodavanje_artikala_Load(object sender, EventArgs e)
        {
            PrikaziPodatke();
        }
        

        private void buttonKreiranje_Click(object sender, EventArgs e)
        {
            String query1 = "INSERT INTO artikal(naziv_artikla, vrsta_artikla, cijena) VALUES " +
                   " ('" + textBoxNazivv.Text + "', '" + textBoxVrsta.Text + "', '" + textBoxCijena.Text + "')";
            String query2 = "SELECT artikal_id FROM artikal WHERE naziv_artikla='" + textBoxNazivv.Text + "' ";
            String query3 = "INSERT INTO skladiste(kolicina_stanje,artikal_id) VALUES " +
                   " ('" + textBoxKolicina.Text+ "', ";
            try
            {
                
                MySqlConnection konekcija = new MySqlConnection(Login_forma.konekcioniString);
                konekcija.Open();
                MySqlCommand cmd1 = new MySqlCommand(query1, konekcija);
                MySqlCommand cmd2 = new MySqlCommand(query2, konekcija);
                cmd1.ExecuteNonQuery();
                MySqlDataReader reader;
                reader = cmd2.ExecuteReader();
                reader.Read();
                string ID = reader[0].ToString();

                query3 += " '"+ID+"'); ";
                reader.Close();
                MySqlCommand cmd3 = new MySqlCommand(query3, konekcija);
                cmd3.ExecuteNonQuery();

                //reader.Close();
                MessageBox.Show("Novi artikal je dodan!");
                konekcija.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String query = "UPDATE skladiste SET kolicina_stanje = (kolicina_stanje + '"+numericUpDown1.Value+"') WHERE artikal_id="+textBoxIDAA.Text+";";
            try
            {

                MySqlConnection konekcija = new MySqlConnection(Login_forma.konekcioniString);
                konekcija.Open();
                MySqlCommand cmd = new MySqlCommand(query, konekcija);              
                cmd.ExecuteNonQuery();
                //MySqlDataReader reader;
                //reader = cmd.ExecuteReader();
                //reader.Read();
                              
                //reader.Close();
                numericUpDown1.Value = 0;
                MessageBox.Show("Količina je dodata!");
                konekcija.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonAzuriranje_Click(object sender, EventArgs e)
        {
            String query1 = "UPDATE artikal SET naziv_artikla = '"+textBoxNazivv.Text+"', vrsta_artikla='"+textBoxVrsta.Text+"', cijena='"+textBoxCijena.Text+"' WHERE artikal_id=" + textBoxIDAA.Text + ";";
            String query2 = "SELECT artikal_id FROM artikal WHERE naziv_artikla='" + textBoxNazivv.Text + "' ";
           String query3 = "UPDATE skladiste SET kolicina_stanje="+textBoxKolicina.Text+" " +
                   " WHERE artikal_id= ";
           
            try
            {

                MySqlConnection konekcija = new MySqlConnection(Login_forma.konekcioniString);
                konekcija.Open();
                MySqlCommand cmd1 = new MySqlCommand(query1, konekcija);
                MySqlCommand cmd2 = new MySqlCommand(query2, konekcija);
                cmd1.ExecuteNonQuery();
                MySqlDataReader reader;
                reader = cmd2.ExecuteReader();
                reader.Read();
                string ID = reader[0].ToString();

                query3 += " '" + ID + "'; ";
                reader.Close();
                MySqlCommand cmd3 = new MySqlCommand(query3, konekcija);
                cmd3.ExecuteNonQuery();

               

                //reader.Close();
                MessageBox.Show("Artikal je azuriran!");
                konekcija.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBoxVrsta_TextChanged(object sender, EventArgs e)
        {

        }

        private void Dodavanje_artikala_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        
    }
}
