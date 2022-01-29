using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace swmsweb
{
    public class DESProvider
    {
        public static string Encrypt(string encryptString, string encryptKey)
        {
            DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
            dCSP.Padding = PaddingMode.PKCS7;
            byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey);
            byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, Encoding.UTF8.GetBytes("hotwind.")), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Convert.ToBase64String(mStream.ToArray());
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="decryptString"></param>
        /// <param name="decryptKey"></param>
        /// <returns></returns>
        public static string Decrypt(string decryptString, string decryptKey)
        {
            byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
            byte[] inputByteArray = Convert.FromBase64String(decryptString);
            DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
            DCSP.Padding = PaddingMode.PKCS7;
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, Encoding.UTF8.GetBytes("hotwind.")), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Encoding.UTF8.GetString(mStream.ToArray());
        }

    }
}
