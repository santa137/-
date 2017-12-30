using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace WindowsFormsApplication1
{
    class Class1
    {
        private string path1 = @"f:\new_file.txt";
        public string Path1
        {
            get
            { return path1; }
        }

        private string GenerationKey()
        {
            string b64key;
            const string alphabet = "ABCDEFGH";
            char[] letters = alphabet.ToCharArray();
            StringBuilder builder = new StringBuilder();
            Random rand = new Random();
            string key = null;
            for (int i = 0; i < 8; i++)
            {
                key += letters[rand.Next(letters.Length)].ToString();
            }
            b64key = Convert.ToString(key);
            return b64key;
        }
        public string Encrypt(string pass)
        {
            string b64key = GenerationKey();
            FileStream stream = new FileStream(Path1, FileMode.OpenOrCreate, FileAccess.Write);
            DESCryptoServiceProvider cryptic = new DESCryptoServiceProvider();
            cryptic.Key = ASCIIEncoding.ASCII.GetBytes(b64key);
            cryptic.IV = ASCIIEncoding.ASCII.GetBytes(b64key);
            CryptoStream crStream = new CryptoStream(stream, cryptic.CreateEncryptor(), CryptoStreamMode.Write);
            byte[] data = ASCIIEncoding.ASCII.GetBytes(pass);
            crStream.Write(data, 0, data.Length);
            crStream.Close();
            stream.Close();
            return b64key;
        }
        public string Decod(string key)
        {
            FileStream stream = new FileStream(Path1, FileMode.Open, FileAccess.Read);
            DESCryptoServiceProvider cryptic = new DESCryptoServiceProvider();
            cryptic.Key = ASCIIEncoding.ASCII.GetBytes(key);
            cryptic.IV = ASCIIEncoding.ASCII.GetBytes(key);
            CryptoStream crStream = new CryptoStream(stream,cryptic.CreateDecryptor(), CryptoStreamMode.Read);
            StreamReader reader = new StreamReader(crStream);
            string data = reader.ReadToEnd();
            reader.Close();
            stream.Close();
            return data;
        }
    }
}
