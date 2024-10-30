using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace arkoDT
{
    class PasswordHelper
    {
        // Constant 16-byte key and IV for AES-128
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("MySecureKey12345");
        private static readonly byte[] IV = Encoding.UTF8.GetBytes("MySecureIV1234567");   

        // Method to encrypt a password
        public static string EncryptPassword(string password)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.GenerateIV(); // Generate a new IV for each encryption

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                {
                    using (var ms = new MemoryStream())
                    {
                        // Prepend the IV to the encrypted data
                        ms.Write(aes.IV, 0, aes.IV.Length); // Write IV to the stream
                        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        {
                            using (var sw = new StreamWriter(cs))
                            {
                                sw.Write(password);
                            }
                        }
                        return Convert.ToBase64String(ms.ToArray()); // Return the full encrypted data (IV + ciphertext)
                    }
                }
            }
        }

        // Method to decrypt a password
        public static string DecryptPassword(string encryptedPassword)
        {
            byte[] fullCipher = Convert.FromBase64String(encryptedPassword);

            using (Aes aes = Aes.Create())
            {
                // Extract the IV from the beginning of the cipher
                byte[] iv = new byte[16]; // 16 bytes for IV
                Array.Copy(fullCipher, 0, iv, 0, iv.Length); // Get the IV

                // Get the actual encrypted data (excluding IV)
                byte[] cipher = new byte[fullCipher.Length - iv.Length];
                Array.Copy(fullCipher, iv.Length, cipher, 0, cipher.Length);

                aes.Key = Key; // Use the same key for decryption
                aes.IV = iv;   // Set the extracted IV

                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                {
                    using (var ms = new MemoryStream(cipher))
                    {
                        using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                        {
                            using (var sr = new StreamReader(cs))
                            {
                                return sr.ReadToEnd();
                            }
                        }
                    }
                }
            }
        }
    }
}
