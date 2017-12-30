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
    public partial class Form2 : Form
    {
        public Form2(string keyshow)
        {
            InitializeComponent();
            this.keyshow = keyshow;
            textBox1.Text = keyshow.ToString();

        }
        string keyshow;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 s = new Form3(keyshow);
            s.Show();
            this.Hide();
        }
    }
}
