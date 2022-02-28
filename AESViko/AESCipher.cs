using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace AESViko
{
    public class AESCipher
    {
        public AESCipher()
        {

        }

        public byte[] StringToBytes(String key)
        {
            int stringSizeInBytes = System.Text.ASCIIEncoding.Unicode.GetByteCount(key);
            //Check if key string is 128, 192 or 256 bits;
            if( stringSizeInBytes == 16 || stringSizeInBytes == 24 || stringSizeInBytes == 32)
            {
                return Encoding.ASCII.GetBytes(key);
            }
            else
            {
                throw new ArgumentOutOfRangeException("Key should be 128, 192, 256 bits\n" +
                    "Random key will be generated, if key input is empty");
            }
        }

        public string EncryptStringToBase64_Aes(string rawText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (rawText == null || rawText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            byte[] encrypted;

            
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(rawText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return System.Convert.ToBase64String(encrypted);
        }
    }
}
