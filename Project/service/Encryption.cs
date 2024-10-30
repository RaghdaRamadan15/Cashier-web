using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Project.service
{
    class Encryption
    { 
            
            private static readonly byte[] Key = Encoding.ASCII.GetBytes("asegftr54wequil8");
        private static readonly byte[] IV = Encoding.ASCII.GetBytes("12kjy879uytri81*");

        public static string EncryptStringToBytes_Aes(string plainText)
            {
                if (string.IsNullOrEmpty(plainText))
                    throw new ArgumentNullException(nameof(plainText));

                byte[] encrypted;

                using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
                {
                    aesAlg.Key = Key;
                    aesAlg.IV = IV;

                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {
                                swEncrypt.Write(plainText);
                            }
                        }

                        encrypted = msEncrypt.ToArray();
                    }
                }

                return Convert.ToBase64String(encrypted);
            }

            public static string DecryptStringFromBytes_Aes(string cipherText)
            {
                if (string.IsNullOrEmpty(cipherText))
                    throw new ArgumentNullException(nameof(cipherText));

                string plaintext = null;
                byte[] cipherBytes = Convert.FromBase64String(cipherText);

                using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
                {
                    aesAlg.Key = Key;
                    aesAlg.IV = IV;

                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    using (MemoryStream msDecrypt = new MemoryStream(cipherBytes))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                plaintext = srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }

                return plaintext;
            }


    }
}