using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using WMPLib;

namespace Lucrare_Atestat
{
    public partial class Form1 : Form
    {

        int? User_ID;
        string email;

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            axWindowsMediaPlayer2.Ctlcontrols.stop();
            axWindowsMediaPlayer2.Visible = false;
            timer1.Stop();
            pictureBox5.Hide();
            label3.Hide();            
            label2.Hide();
            label12.Hide();
            label13.Hide();
            pictureBox13.Hide();
            label14.Hide();
            label15.Hide();
            label16.Hide();
            label17.Hide();
            button8.Hide();
            button9.Hide();
            trackBar1.Value = 50;
        }
        Random random = new Random();
        public void functie(string mail)
        {
            email = mail;
        }

        private void InitializeMediaPlayer()
        {
            axWindowsMediaPlayer2.uiMode = "none";
            this.Padding = new Padding(0);
            axWindowsMediaPlayer2.Margin = new Padding(0);
            string VideoFileName = "Video.mp4";
            string VideoFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, VideoFileName);
            axWindowsMediaPlayer2.URL = VideoFilePath;
        }

        private WindowsMediaPlayer player;
        int idk;
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'logInDbDataSet.Users' table. You can move, or remove it, as needed.
            this.usersTableAdapter.Fill(this.logInDbDataSet.Users);
            // TODO: This line of code loads data into the 'logInDbDataSet.Muzica' table. You can move, or remove it, as needed.
            button7.Enabled = false;
            player = new WindowsMediaPlayer();
            label9.Text = "";
            button4_Click(sender, e);
            DataTable d = muzicaTableAdapter.Umplere(User_ID);
            for (int i = 0; i < d.Rows.Count; i++)
            {
                if (d.Rows[i]["Artist"].ToString() == "")
                {
                    listBox1.Items.Add(d.Rows[i]["Nume"] + ", de Necunoscut");
                }
                else
                {
                    listBox1.Items.Add(d.Rows[i]["Nume"] + ", de " + d.Rows[i]["Artist"]);
                }
            }
            if(listBox1.Items.Count>2)
                idk = random.Next(2, listBox1.Items.Count);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            User_ID = usersTableAdapter.Returnez_ID(email);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (axWindowsMediaPlayer2.Visible == true)
            {
                axWindowsMediaPlayer2.Ctlcontrols.stop();
                axWindowsMediaPlayer2.Visible = false;
                timer1.Stop();
                pictureBox5.Hide();
                label3.Hide();
                button8.Hide();
                button9.Hide();
                label14.Hide();
                label15.Hide();
                label16.Hide();
                label17.Hide();
            }
            else
            {
                InitializeMediaPlayer();
                axWindowsMediaPlayer2.Visible = true;
                InitializeTimer();
                //pictureBox4.Hide();
                pictureBox5.Show();
                label3.Show();
                button8.Show();
                button9.Show();
                label14.Hide();
                label15.Hide();
                label16.Hide();
                label17.Hide();
            }
        }

        private void InitializeTimer()
        {
            timer1.Interval = 90000; // in milisec
            timer1.Tick += Timer1_Tick;
            timer1.Start();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            axWindowsMediaPlayer2.Ctlcontrols.stop();
            axWindowsMediaPlayer2.Visible = false;
            timer1.Stop();
            pictureBox5.Hide();
            label3.Hide();
            button8.Hide();
            button9.Hide();
            label14.Hide();
            label15.Hide();
            label16.Hide();
            label17.Hide();
            //pictureBox4.Show();

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Selectare")
            {
                if (listBox1.SelectedIndex == -1)
                {
                    label9.Text = "Selecteaza o melodie pentru a face actiunea!";
                }
                else
                {
                    label9.Text = "";
                    

                        string h = (listBox1.SelectedItem).ToString();
                        string l = String.Empty;
                        for (int j = 0; j < h.Length; j++)
                        {
                            if (h[j] == ',')
                            {
                                l = h.Substring(0, j);
                            }
                        }
                        button7.Text = l;
                        string destinatie = muzicaTableAdapter.Dami_Dest(l);
                        player.URL = destinatie;
                        player.controls.play();
                }
            }
        }
        private void functie_skip()
        {
            string h = (listBox1.SelectedItem).ToString();
            string l = String.Empty;
            for (int j = 0; j < h.Length; j++)
            {
                if (h[j] == ',')
                {
                    l = h.Substring(0, j);
                }
            }
            button7.Text = l;
            string destinatie = muzicaTableAdapter.Dami_Dest(l);
            player.URL = destinatie;
            player.controls.play();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "Stergere")
            {
                if (listBox1.SelectedIndex == -1)
                {
                    label9.Text = "Selecteaza o melodie pentru a face actiunea!";
                }
                else
                {
                    string cop = "";
                    label9.Text = "";
                    string h = listBox1.SelectedItem.ToString();
                    listBox1.Items.Remove(h);
                    string l = String.Empty;
                    for (int i = 0; i < h.Length; i++)
                    {
                        if (h[i] == ',')
                        {
                            l = h.Substring(0, i);
                        }
                    }
                    cop=l;
                    string adresa = "";
                    DataTable d = muzicaTableAdapter.Adresa(l, User_ID);
                    adresa = d.Rows[0]["MP3"].ToString();
                    File.Delete(adresa);
                    muzicaTableAdapter.StergereQ(l);
                    muzicaTableAdapter.Update(logInDbDataSet.Muzica);
                    if (button7.Text == cop)
                    {
                        button7.Text = "Selectati Melodia";
                        StopMp3();
                    }
                    pictureBox13_Click(sender, e);
                    
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.Text != "Revino la lista de melodii")
            {
                listBox1.Hide();
                //button1.Hide();
                button1.Text = "Buton invalid";
                button2.Text = "Buton invalid";
                //button2.Hide();
                //button3.Hide();
                button3.Text = "Revino la lista de melodii";
                label8.Text = String.Empty;
            }
            else
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                listBox1.Show();
                button1.Text = "Selectare";
                button2.Text = "Stergere";
                button3.Text = "Adauga o noua melodie";
            }
        }
        string filePath = "\0";
        

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*";
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the path of specified file
                filePath = openFileDialog.FileName;
                textBox3.Text = filePath;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() != string.Empty)
            {
                if (textBox3.Text.Trim() != string.Empty)
                {
                    if (muzicaTableAdapter.Daca_Exista_Nume(textBox1.Text.Trim(), User_ID).ToString() == "0")
                    {
                        try
                        {
                            string sourceFilePath = filePath;
                            string fileName = Path.GetFileName(sourceFilePath);
                            string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
                            string destinationFolder = Path.Combine(projectDirectory, User_ID.ToString());
                            if (!Directory.Exists(destinationFolder))
                            {
                                Directory.CreateDirectory(destinationFolder);
                            }
                            string destinationFilePath = Path.Combine(destinationFolder, fileName);
                            File.Copy(sourceFilePath, destinationFilePath, true);
                            muzicaTableAdapter.Insert_Muzica(destinationFilePath, textBox1.Text.Trim(), textBox2.Text.Trim(), User_ID);
                            muzicaTableAdapter.Update(logInDbDataSet.Muzica);
                            label8.Text = "Melodie salvata!";
                            if (textBox2.Text.Trim() == "")
                            {
                                listBox1.Items.Add(textBox1.Text.Trim() + ", de Necunoscut");
                            }
                            else
                            {
                                listBox1.Items.Add(textBox1.Text.Trim() + ", de " + textBox2.Text.Trim());
                            }
                            if (listBox1.Items.Count > 2)
                                idk = random.Next(2, listBox1.Items.Count);
                        }
                        catch (Exception ex)
                        {
                            label8.Text = "Melodie deja adaugata?";
                        }
                    }
                    else
                    {
                        label8.Text = "Nume existent!";
                    }
                }
                else
                {
                    label8.Text = "Nu ati selectat melodie!";
                }
            }
            else
            {
                label8.Text = "Nu ati introdus nume!";
            }
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            PauseMp3();
        }

        private void PauseMp3()
        {
            if (player != null)
            {
                player.controls.pause();
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            ResumeMp3();
        }

        private void ResumeMp3()
        {
            if (player != null)
            {
                player.controls.play();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            StopMp3();
            if (player != null)
            {
                player.close();
            }
        }

        private void StopMp3()
        {
            if (player != null)
            {
                player.controls.stop();
            }
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            if (pictureBox13.Visible == true)
            {
                int cop = listBox1.SelectedIndex;
                while (listBox1.SelectedIndex == cop)
                {
                    listBox1.SelectedIndex = (listBox1.SelectedIndex + idk) % listBox1.Items.Count;
                }
                if (listBox1.Items.Count > 2)
                {
                    idk = random.Next(2, listBox1.Items.Count);
                }
                functie_skip();
            }
            else
            {
                if (listBox1.SelectedIndex == 0)
                {
                    listBox1.SelectedIndex = listBox1.Items.Count - 1;
                    functie_skip();
                }
                else
                {
                    listBox1.SelectedIndex--;
                    functie_skip();
                }
            }

        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            if (pictureBox13.Visible == true)
            {
                int cop = listBox1.SelectedIndex;
                while (listBox1.SelectedIndex == cop)
                {
                    listBox1.SelectedIndex = (listBox1.SelectedIndex + idk) % listBox1.Items.Count;
                }
                if (listBox1.Items.Count > 2)
                {
                    idk = random.Next(2, listBox1.Items.Count);
                }
                functie_skip();
            }
            else
            {
                if (listBox1.SelectedIndex == listBox1.Items.Count - 1)
                {
                    listBox1.SelectedIndex = 0;
                    functie_skip();
                }
                else
                {
                    listBox1.SelectedIndex++;
                    functie_skip();
                }
            }
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count <= 2)
            {
                label9.Text = "Nu exista shuffle pentru nr tau de melodii";
            }
            else
            {
                label11.Hide();
                label10.Hide();
                pictureBox12.Hide();
                pictureBox13.Show();
                label12.Show();
                label13.Show();
            }
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            label12.Hide();
            label13.Hide();
            pictureBox13.Hide();
            pictureBox12.Show();
            label10.Show();
            label11.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            InitializeMediaPlayer();
            axWindowsMediaPlayer2.Visible = true;
            InitializeTimer();
            if (label3.Visible == true)
            {
                pictureBox4_Click(sender, e);
            }
            else if (label14.Visible == true)
            {
                label3.Show();
                label14.Hide();
            }
            else if (label15.Visible == true)
            {
                label14.Show();
                label15.Hide();
            }
            else if (label16.Visible == true)
            {
                label15.Show();
                label16.Hide();
            }
            else if (label17.Visible == true)
            {
                label16.Show();
                label17.Hide();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            InitializeMediaPlayer();
            axWindowsMediaPlayer2.Visible = true;
            InitializeTimer();
            if (label3.Visible == true)
            {
                label14.Show();
                label3.Hide();
            }
            else if (label14.Visible == true)
            {
                label15.Show();
                label14.Hide();
            }
            else if (label15.Visible == true)
            {
                label16.Show();
                label15.Hide();
            }
            else if (label16.Visible == true)
            {
                label17.Show();
                label16.Hide();
            }
            if (label17.Visible == true)
            {
                pictureBox4_Click(sender, e);
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            player.settings.volume = trackBar1.Value;
        }

        
    }
}
