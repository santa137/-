using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Security.Cryptography;




namespace WindowsFormsApp1
{
    public partial class ZeroSector : Form
    {

        byte[] mbrData;

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern SafeFileHandle CreateFile(
            string lpFileName,
            [MarshalAs(UnmanagedType.U4)] FileAccess dwDesiredAccess,
            [MarshalAs(UnmanagedType.U4)] FileShare dwShareMode,
            IntPtr lpSecurityAttributes,
            [MarshalAs(UnmanagedType.U4)] FileMode dwCreationDisposition,
            [MarshalAs(UnmanagedType.U4)] FileAttributes dwFlagsAndAttributes,
            IntPtr hTemplateFile);



        public ZeroSector()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread potok = new Thread(readmbr);
            potok.Start();
        }


        void readmbr()
        {
            SafeFileHandle handle = CreateFile(
            lpFileName: @"\\.\" + textBox3.Text + ":",
            dwDesiredAccess: FileAccess.Read,
            dwShareMode: FileShare.ReadWrite,
            lpSecurityAttributes: IntPtr.Zero,
            dwCreationDisposition: System.IO.FileMode.OpenOrCreate,
            dwFlagsAndAttributes: FileAttributes.Normal,
            hTemplateFile: IntPtr.Zero);

            using (FileStream disk = new FileStream(handle, FileAccess.Read))
            {
             this.Invoke(new Action(()=>   { textBox1.Text = "";
                    textBox2.Text = ""; }));
                mbrData = new byte[512];


                disk.Read(mbrData, 0, 512);

                int count = 0;
                foreach (byte b in mbrData)
                {
                   
                    textBox2.Invoke(new Action(() => { textBox2.AppendText( count.ToString() + "\t" + b.ToString() + Environment.NewLine); }));
                    count++;
                    
                }

                for (int i = 384; i < 416; i++)
                {
                   textBox1.Invoke(new Action(()=> { textBox1.Text += (char)mbrData[i]; }));
                   
                }

                MessageBox.Show("МБР прочитан! Пароль получен!");

            }
        }



        private void button2_Click(object sender, EventArgs e)
        {


            SafeFileHandle handle = CreateFile(
            lpFileName: @"\\.\" + textBox3.Text + ":",
            dwDesiredAccess: FileAccess.Read,
            dwShareMode: FileShare.ReadWrite,
            lpSecurityAttributes: IntPtr.Zero,
            dwCreationDisposition: System.IO.FileMode.OpenOrCreate,
            dwFlagsAndAttributes: FileAttributes.Normal,
             hTemplateFile: IntPtr.Zero);

            using (FileStream disk = new FileStream(handle, FileAccess.Read))
            {
                mbrData = new byte[512];
                disk.Read(mbrData, 0, 512);
            }
            
              handle = CreateFile(
              lpFileName: @"\\.\" + textBox3.Text + ":",
              dwDesiredAccess: FileAccess.Write,
              dwShareMode: FileShare.ReadWrite,
              lpSecurityAttributes: IntPtr.Zero,
              dwCreationDisposition: System.IO.FileMode.OpenOrCreate,
              dwFlagsAndAttributes: FileAttributes.Normal,
              hTemplateFile: IntPtr.Zero);

            using (FileStream disk = new FileStream(handle, FileAccess.Write))
            {
               
                String hashpassword = GetMd5Hash(textBox1.Text);
                //String hashpassword = textBox1.Text;
                 for (int i = 0; i < hashpassword.Length; i++)
                mbrData[384 + i] = (byte)hashpassword[i];

                disk.Write(mbrData, 0, 512);
                MessageBox.Show("Пароль записан в MBR");


            }

        }

        static string GetMd5Hash(string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }


    }
}
