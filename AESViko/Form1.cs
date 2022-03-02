using System;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;
using System.Text;

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
                    myAes.Key = crypter.StringToBytes(textBox3.Text);
                    myAes.IV = crypter.StringToBytes(textBox4.Text);

                    textBox2.Text = crypter.EncryptStringToBase64_Aes(text,
                        myAes.Key, myAes.IV, cMode);

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

                // Get CipherMode from combobox
                CipherMode cMode = GetCipherMode();

                // Encrypt the string to an base64 string
                try
                {
                    myAes.Key = crypter.StringToBytes(textBox3.Text);
                    myAes.IV = crypter.StringToBytes(textBox4.Text);

                    textBox1.Text = crypter.DecryptStringFromBase64_Aes(text,
                        myAes.Key, myAes.IV, cMode);
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

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(textBox2.Text))
                {
                    throw new ArgumentNullException("Please encrypt your text first");
                }
                string creationTime = DateTime.Now.ToString("dd_mm_yyyy_HH_mm_ss");
                string fileName = "EncryptedText_" + creationTime + ".txt";

                // Check if file already exists. If yes, delete it.     
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                // Create a new file
                File.WriteAllText(fileName, textBox2.Text);

                if(MessageBox.Show("Do you want to save Key and IV in separate file?",
                    "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    fileName = "KeyIvText_" + creationTime + ".txt";
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }
                    string KeyIv = String.Format("Key: {0}\nIV: {1}.txt", textBox3.Text, textBox4.Text);
                    File.WriteAllText(fileName, KeyIv);

                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                // Show openfiledialog
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.InitialDirectory = "";
                    openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                    openFileDialog.FilterIndex = 2;
                    openFileDialog.RestoreDirectory = true;

                    // Read all content of file to textbox
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        textBox2.Text = File.ReadAllText(openFileDialog.SafeFileName);
                    }
                }
            }
            catch(Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
    }

    
}
