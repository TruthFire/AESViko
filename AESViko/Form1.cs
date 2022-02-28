using System;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace AESViko
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            
        }

        private void EncryptText(string text)
        {
            using (Aes myAes = Aes.Create())
            {
                AESCipher crypter = new();
                CipherMode cMode;
                //MessageBox.Show(System.Convert.ToBase64String(myAes.IV));
                
                // Get CipherMode from combobox
                if(comboBox1.Text == "CBC")
                {
                    cMode = CipherMode.CBC;
                }
                else
                {
                    cMode = CipherMode.ECB;
                }
             

                // Encrypt the string to an base64 string
                try
                {
                    
                    if (!String.IsNullOrWhiteSpace(textBox3.ToString()))
                    {
                        myAes.Key = crypter.StringToBytes(textBox3.Text);
                        textBox2.Text = crypter.EncryptStringToBase64_Aes(text,
                            myAes.Key, myAes.IV, cMode);
                        textBox2.Text += "\nIV: " + System.Convert.ToBase64String(myAes.IV);
                    }

                    else
                    { 
                        
                        textBox2.Text = crypter.EncryptStringToBase64_Aes(text,
                            myAes.Key,myAes.IV, cMode);
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

        private void button3_Click(object sender, EventArgs e)
        {
            Generator Gen = new();
            textBox3.Text = Gen.Generate(Convert.ToInt32(comboBox2.Text));
            textBox4.Text = Gen.Generate(128);
        }
    }

    
}
