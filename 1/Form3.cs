using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form3 : Form
    {
        Class1 path1;
        Class1 Encrypt;
        Class1 Decod;
        public Form3(string keyshow)
        {
            InitializeComponent();
            this.keyshow = keyshow;
            path1 = new Class1();
            Encrypt = new Class1();
            Decod = new Class1();

        }
        string keyshow;

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            string str1 = textBox1.Text;
            string pass1 = Decod.Decod(keyshow);
            if (str1 == pass1)
            {
                string pass = textBox2.Text;
                string b64key1 = Encrypt.Encrypt(pass);
                MessageBox.Show("Пароль успешно изменён");
                Form2 s = new Form2(b64key1);
                s.ShowDialog();
                this.Hide();

            }
            else
            {

                textBox1.Text = "";
                MessageBox.Show("Неверный ввод!");
            }
        }

       
    }
}
