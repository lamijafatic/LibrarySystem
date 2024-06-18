using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace WindowsFormsApplication1
{
    public partial class Login_forma : Form
    {
        public Login_forma()
        {
            InitializeComponent();
        }

        public static String konekcioniString = "Server=localhost; Port=3306; Database=ado; Uid=root; Pwd=";
        public static string korisnickiID;

        private void Prijava()
        {
            errorProvider1.Clear();
            String korisnickoIme = textBoxKorisnickoIme.Text;
            String sifra = textBoxSifra.Text;
            String kveri = "SELECT ime, kupac_id, pass FROM kupac WHERE user = '" + korisnickoIme + "'";


            try
            {
                MySqlConnection konekcija = new MySqlConnection(konekcioniString);
                konekcija.Open();
                MySqlCommand cmd = new MySqlCommand(kveri, konekcija);
                MySqlDataReader reader;
                reader = cmd.ExecuteReader();
                reader.Read();

                if (!reader.HasRows)
                {
                    errorProvider1.SetError(textBoxKorisnickoIme, "Pogrešno korisničko ime.");
                }
                else
                {
                    string ime = reader[0].ToString();
                    korisnickiID = reader[1].ToString();
                    string pass = reader[2].ToString();


                    if (sifra == pass)
                    {
                        if (korisnickiID == "1")
                        {
                            MessageBox.Show("Dobrodošli administratore! ");
                            Rad_sa_kupcima fr2 = new Rad_sa_kupcima();
                            this.Hide();
                            fr2.Show();

                       }
                        else
                        {
                            MessageBox.Show("Uspješno ste se logovali korisniče!" );
                            Form fr3 = new Kreiranje_narudzbe();
                            this.Hide();
                            fr3.Show();
                        }
                    }
                    else
                    {
                        errorProvider1.SetError(textBoxSifra, "Pogrešan password.");
                    }


                }
                reader.Close();
                konekcija.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        

        private void Login_forma_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void buttonPrijava_Click(object sender, EventArgs e)
        {
            Prijava();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Login_forma_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
