using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public static class EncryptionUtility
{


    public static string EncryptString(string plainText, string key)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(key);
            aesAlg.IV = GenerateRandomIV();

            aesAlg.Mode = CipherMode.CBC;
            aesAlg.Padding = PaddingMode.PKCS7;

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

                // Obtener el texto cifrado (sin el IV)
                byte[] cipherTextBytes = msEncrypt.ToArray();

                // Concatenar el IV y el texto cifrado
                byte[] ivAndCipherText = new byte[aesAlg.IV.Length + cipherTextBytes.Length];
                Array.Copy(aesAlg.IV, ivAndCipherText, aesAlg.IV.Length);
                Array.Copy(cipherTextBytes, 0, ivAndCipherText, aesAlg.IV.Length, cipherTextBytes.Length);

                return Convert.ToBase64String(ivAndCipherText);
            }
        }
    }

    public static string DecryptString(string cipherText, string key)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(key);

            aesAlg.Mode = CipherMode.CBC;
            aesAlg.Padding = PaddingMode.PKCS7;

            // Extraer el IV del texto cifrado (primeros 16 bytes)
            byte[] iv = new byte[aesAlg.IV.Length];
            Array.Copy(Convert.FromBase64String(cipherText), iv, iv.Length);
            aesAlg.IV = iv;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            // Ignorar los primeros 16 bytes del texto cifrado (ya que son el IV)
            byte[] cipherBytes = new byte[Convert.FromBase64String(cipherText).Length - aesAlg.IV.Length];
            Array.Copy(Convert.FromBase64String(cipherText), aesAlg.IV.Length, cipherBytes, 0, cipherBytes.Length);

            using (MemoryStream msDecrypt = new MemoryStream(cipherBytes))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }
    }

    private static byte[] GenerateRandomIV()
    {
        using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
        {
            byte[] iv = new byte[16]; // Tamaño del IV (16 bytes para AES)
            rng.GetBytes(iv);
            return iv;
        }
    }
}




