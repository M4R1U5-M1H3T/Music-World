using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Lucrare_Atestat
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            textBox3.PasswordChar = '*';
            DoubleBuffered = true;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'logInDbDataSet.Users' table. You can move, or remove it, as needed.
            this.usersTableAdapter.Fill(this.logInDbDataSet.Users);

        }

        void f_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (textBox3.PasswordChar == '*')
            {
                textBox3.PasswordChar = '\0';
            }
            else
            {
                textBox3.PasswordChar = '*';
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.ToString().Trim() != "")
            {

                if (textBox3.Text.ToString().Trim() != "")
                {
                    int? o;
                    o = usersTableAdapter.Daca_Exista(textBox1.Text.ToString().Trim(), textBox3.Text.ToString().Trim());
                    if (o == 0)
                    {
                        label1.Text = "Date invalide!";
                    }
                    else
                    {
                        //Se face trecere la aplicatie !!!!
                        string mail = textBox1.Text.ToString().Trim();
                        ///trebuie sa transfer mail la form3
                        this.Hide();
                        Form1 f = new Form1();
                        f.label1.Text = mail; // aici transmit email-u
                        f.functie(mail);
                        f.FormClosed += new FormClosedEventHandler(f_FormClosed);
                        f.ShowDialog();
                    }
                }
                else
                {
                    label1.Text = "Date Invalide!";
                }
            }
            else
            {
                label1.Text = "Date Invalide!";
            }
        }
    }
}
