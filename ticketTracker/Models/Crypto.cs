using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace ticketTracker.Models
{
    public class Crypto
    {
        private static byte[] salt = Encoding.ASCII.GetBytes("1jk44oijed2If0E994jehDie829320219"); // This is not stored anywhere so can be changed while app is offline without any consequences
        private static byte[] pepperEnc = Encoding.ASCII.GetBytes("F15udE75Kwo9d9sS02lkvFieoslakC93C!ls"); // Do Not Change
        private static string pepperHash = "f7&eroisldkKDSlsaoei3(djfoOSIdkdleD24dkd"; // Do Not Change

        /// <summary>
        /// Encrypt the given string using AES.  The string can be decrypted using 
        /// DecryptStringAES().  The sharedSecret parameters must match.
        /// </summary>
        /// <param name="plainText">The text to encrypt.</param>
        /// <param name="sharedSecret">A password used to generate a key for encryption.</param>
        public string EncryptStringAES(string plainTextInp, string sharedSecret, string inpSalt = null, int intSomeValue = 0)
        {
            string plainText = plainTextInp.Trim();
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException("plainText");
            if (string.IsNullOrEmpty(sharedSecret))
                throw new ArgumentNullException("sharedSecret");
            if (!string.IsNullOrEmpty(inpSalt))
            {
                //System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
                //salt = encoding.GetBytes(inpSalt);


                salt = Encoding.ASCII.GetBytes(inpSalt + pepperEnc);
                //salt = Encoding.ASCII.GetBytes(inpSalt);
            }

            byte[] encryptedData;
            string outStr = null;                       // Encrypted string to return
            RijndaelManaged aesAlg = null;              // RijndaelManaged object used to encrypt the data.

            try
            {
                // generate the key from the shared secret and the salt
                Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(sharedSecret, salt);

                // Create a RijndaelManaged object
                // with the specified key and IV.
                aesAlg = new RijndaelManaged();
                aesAlg.Padding = PaddingMode.Zeros;
                aesAlg.BlockSize = 256; // if want interoperability with AES then need block size of 128
                aesAlg.KeySize = 256;
                aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
                aesAlg.IV = key.GetBytes(aesAlg.BlockSize / 8);

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {

                        //csEncrypt.FlushFinalBlock();
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                            swEncrypt.Flush();

                        }
                    }
                    msEncrypt.Close();
                    if (intSomeValue == 1)
                    {
                        outStr = Convert.ToBase64String(msEncrypt.ToArray());
                    }
                    else
                    {
                        encryptedData = msEncrypt.ToArray();
                        SoapHexBinary shb = new SoapHexBinary(encryptedData);
                        outStr = shb.ToString();
                    }
                }
            }
            finally
            {
                // Clear the RijndaelManaged object.
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            // Return the encrypted bytes from the memory stream.
            return outStr;
        }

        /// <summary>
        /// Decrypt the given string.  Assumes the string was encrypted using 
        /// EncryptStringAES(), using an identical sharedSecret.
        /// </summary>
        /// <param name="cipherText">The text to decrypt.</param>
        /// <param name="sharedSecret">A password used to generate a key for decryption.</param>
        public string DecryptStringAES(string cipherText, string sharedSecret, string inpSalt = null, int FromFETPIP = 0)
        {
            if (string.IsNullOrEmpty(cipherText))
                throw new ArgumentNullException("cipherText");
            if (string.IsNullOrEmpty(sharedSecret))
                throw new ArgumentNullException("sharedSecret");
            if (!string.IsNullOrEmpty(inpSalt))
            {
                //System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
                //salt = encoding.GetBytes(inpSalt);

                // UNCOMMENT ME (below)
                salt = Encoding.ASCII.GetBytes(inpSalt + pepperEnc);
                //salt = Encoding.ASCII.GetBytes(inpSalt);
            }

            // Declare the RijndaelManaged object
            // used to decrypt the data.
            RijndaelManaged aesAlg = null;

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            try
            {
                // generate the key from the shared secret and the salt
                Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(sharedSecret, salt);

                // Create a RijndaelManaged object
                // with the specified key and IV.
                aesAlg = new RijndaelManaged();
                aesAlg.Padding = PaddingMode.Zeros;
                aesAlg.BlockSize = 256;
                aesAlg.KeySize = 256;
                aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
                aesAlg.IV = key.GetBytes(aesAlg.BlockSize / 8);

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                // Create the streams used for decryption.
                if (FromFETPIP == 1)
                {
                    byte[] bytes = Convert.FromBase64String(cipherText);
                    using (MemoryStream msDecrypt = new MemoryStream(bytes))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))

                                // Read the decrypted bytes from the decrypting stream
                                // and place them in a string.
                                plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
                else
                {
                    SoapHexBinary shb = SoapHexBinary.Parse(cipherText);
                    byte[] bytes = shb.Value;

                    using (MemoryStream msDecrypt = new MemoryStream(bytes))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))

                                // Read the decrypted bytes from the decrypting stream
                                // and place them in a string.
                                plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            finally
            {
                // Clear the RijndaelManaged object.
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            return plaintext.TrimEnd('\0');  //  TrimEnd shouldn't be necessary but sometimes they're still there (it also gets stripped when inserted into dictionary in LogggedInMaster)
        }

        //public string CreateSalt(int size) // Generate a cryptographic random number using the cryptographic service provider
        //{
        //    RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        //    byte[] buff = new byte[size];
        //    rng.GetBytes(buff);
        //    string result = ASCIIEncoding.ASCII.GetString(buff);
        //    return result;
        //}

        public string GetHash(string txtPassword, string txtSalt)
        {
            string newSalt = txtSalt + pepperHash;
            byte[] salt = System.Text.Encoding.ASCII.GetBytes(newSalt);

            Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(txtPassword, salt, 1000);
            var encryptor = SHA512.Create();
            var hash = encryptor.ComputeHash(bytes.GetBytes(128));

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }

            return sb.ToString();
        }

        public bool verifyHash(string txtPassword, string txtHash, string txtSalt)
        {
            string unverified = GetHash(txtPassword, txtSalt);
            if (unverified == txtHash)
            { return true; }
            else
            { return false; }
        }

    }
}
