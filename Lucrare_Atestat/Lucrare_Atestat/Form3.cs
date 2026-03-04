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
    public partial class Form3 : Form
    {
        public Form3()
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
            string l = textBox2.Text.ToString().Trim();
            int? x;
            x = usersTableAdapter.Exista_Sau_Nu(l);
            if (x == 1)
            {
                label1.Text = "Cont existent!";
            }
            else
            {
                if (textBox1.Text.ToString().Trim() != "")
                {
                    if (textBox2.Text.ToString().Trim() != "")
                    {
                        if (textBox3.Text.ToString().Trim() != "")
                        {
                            Random random = new Random();
                            int newID = random.Next(0, 10000);
                            while (usersTableAdapter.Verif_ID(newID) != 0)
                            {
                                newID = random.Next(0, 10000);
                            }
                            usersTableAdapter.InsertQuery(textBox1.Text.ToString().Trim(), textBox2.Text.ToString().Trim(), textBox3.Text.ToString().Trim(), newID);
                            usersTableAdapter.Update(logInDbDataSet.Users); /// trebuie sa dau update la database
                            //Se face trecere la aplicatie !!!!
                            string mail = textBox2.Text.ToString().Trim(); // aici e email-ul
                            //trebuie sa transmit email de la asta la form3
                            this.Hide();
                            Form1 f = new Form1();
                            f.label1.Text = mail; // aici transmit la label
                            f.functie(mail);
                            f.FormClosed += new FormClosedEventHandler(f_FormClosed);
                            f.ShowDialog();
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
                else
                {
                    label1.Text = "Date Invalide!";
                }
            }
        }

        void f_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'logInDbDataSet.Users' table. You can move, or remove it, as needed.
            this.usersTableAdapter.Fill(this.logInDbDataSet.Users);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f = new Form2();
            f.FormClosed += new FormClosedEventHandler(f_FormClosed);
            f.ShowDialog(); 
        }
    }
}
