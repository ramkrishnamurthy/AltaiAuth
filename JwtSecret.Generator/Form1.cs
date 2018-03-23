using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JwtSecret.Generator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            int keySize;
            if (int.TryParse(keySizeTextBox.Text, out keySize))
            {
                if (keySize < 256)
                {
                    MessageBox.Show("Key size must be 256 or greater");
                }
                else
                {
                    using (var rngCsp = new RNGCryptoServiceProvider())
                    {
                        var bytes = new byte[keySize];
                        rngCsp.GetBytes(bytes);
                        var base64 = Convert.ToBase64String(bytes);
                        keyValueTextBox.Text = base64;
                    }
                }
            }
            else
            {
                MessageBox.Show("Invalid key size, use 512, 1024, ...");
            }
        }
    }
}
