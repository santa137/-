using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Class1 path1;
        Class1 Encrypt;
        Class1 Decod;
        public Form1()
        {
            InitializeComponent();
            path1 = new Class1();
            Encrypt = new Class1();
            Decod = new Class1();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            string pass = textBox1.Text;
            string key = textBox2.Text;
            if (!(File.Exists(path1.Path1)))
            {

                string b64key1 = Encrypt.Encrypt(pass);
                Form2 s = new Form2(b64key1);
                s.ShowDialog();
                this.Hide();
            }
            else
            {
                string pass1 = Decod.Decod(key);
                if (pass1 == pass)
                {
                    string b64key1 = Encrypt.Encrypt(pass);
                    Form2 s = new Form2(b64key1);
                    s.ShowDialog();
                    this.Hide();
                }
                else
                {
                    textBox2.Text = "";
                    MessageBox.Show("Неверный ввод!");
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.PasswordChar = '*';
        }
    }
}
