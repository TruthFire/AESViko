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

namespace AESViko
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            
        }

        private void EncryptText(string text)
        {
            using (Aes myAes = Aes.Create())
            {
                AESCipher crypter = new();

                //MessageBox.Show(System.Convert.ToBase64String(myAes.IV));
                
                // Encrypt the string to an base64 string
                try
                {
                    myAes.Mode = CipherMode.ECB;
                    if (!String.IsNullOrWhiteSpace(textBox3.ToString()))
                    {
                        
                        textBox2.Text = crypter.EncryptStringToBase64_Aes(text,
                            myAes.Key, myAes.IV);
                        textBox2.Text += "\nIV: " + System.Convert.ToBase64String(myAes.IV);
                    }
                    else
                    { 
                        myAes.Key = crypter.StringToBytes(textBox3.Text);
                        textBox2.Text = crypter.EncryptStringToBase64_Aes(text,
                            myAes.Key,myAes.IV);
                        textBox2.Text += "\nIV: " + System.Convert.ToBase64String(myAes.IV);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EncryptText(textBox1.Text);   
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }

    
}
