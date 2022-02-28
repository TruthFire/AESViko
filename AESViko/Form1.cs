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

                //MessageBox.Show(System.Convert.ToBase64String(myAes.IV));

                // Get CipherMode from combobox
                CipherMode cMode = GetCipherMode();

                // Encrypt the string to an base64 string
                try
                {
                    
                    if (!String.IsNullOrWhiteSpace(textBox3.ToString()))
                    {
                        myAes.Key = crypter.StringToBytes(textBox3.Text);
                        myAes.IV = crypter.StringToBytes(textBox4.Text);

                        textBox2.Text = crypter.EncryptStringToBase64_Aes(text,
                            myAes.Key, myAes.IV, cMode);
                        //textBox2.Text += "\nIV: " + System.Convert.ToBase64String(myAes.IV);
                    }

                    else
                    {

                        GenerateKeyAndIV();
                        EncryptText(text);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }
        }

        private void DecryptText(string text)
        {
            using (Aes myAes = Aes.Create())
            {
                AESCipher crypter = new();

                //MessageBox.Show(System.Convert.ToBase64String(myAes.IV));

                // Get CipherMode from combobox
                CipherMode cMode = GetCipherMode();

                // Encrypt the string to an base64 string
                try
                {

                    if (!String.IsNullOrWhiteSpace(textBox3.ToString()))
                    {
                        myAes.Key = crypter.StringToBytes(textBox3.Text);
                        myAes.IV = crypter.StringToBytes(textBox4.Text);

                        textBox1.Text = crypter.DecryptStringFromBase64_Aes(text,
                            myAes.Key, myAes.IV, cMode);
                        //textBox2.Text += "\nIV: " + System.Convert.ToBase64String(myAes.IV);
                    }

                    else
                    {
                        throw new ArgumentNullException("There is no encrypted text"); 
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
            DecryptText(textBox2.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GenerateKeyAndIV();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex == 1)
            {
                textBox4.ReadOnly = true;
            }
            else
            {
                textBox4.ReadOnly = false;
            }
        }

        private void GenerateKeyAndIV()
        {
            Generator Gen = new();
            textBox3.Text = Gen.Generate(Convert.ToInt32(comboBox2.Text));
            textBox4.Text = Gen.Generate(128);
        }

        private CipherMode GetCipherMode()
        {
            CipherMode cMode;
            if (comboBox1.SelectedIndex == 0)
            {
                cMode = CipherMode.CBC;
            }
            else
            {
                cMode = CipherMode.ECB;
            }

            return cMode;
        }
    }

    
}
