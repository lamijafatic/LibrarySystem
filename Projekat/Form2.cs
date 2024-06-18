using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 4000; 
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }
       



private void timer_Tick(object sender, EventArgs e)
{
 
    ((System.Windows.Forms.Timer)sender).Stop();

    Login_forma forma = new Login_forma();
    forma.Show();
    this.Hide();
    
}

private void Form2_FormClosing(object sender, FormClosingEventArgs e)
{
    Application.Exit();
}



        
    


    }
}
