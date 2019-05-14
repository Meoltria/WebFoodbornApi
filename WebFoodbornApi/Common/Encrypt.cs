using System;
using System.Security.Cryptography;
using System.Text;

namespace WebFoodbornApi.Common
{
    public static class Encrypt
    {
        public static string Md5Encrypt(string input)
        {
            MD5 md5 = MD5.Create();
            byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public static string Base64Encode(string input)
        {
            string encode = string.Empty;
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = input;
            }
            return encode;
        }

        public static string Base64Decode(string input)
        {
            string decode = string.Empty;
            byte[] bytes = Convert.FromBase64String(input);
            try
            {
                decode = Encoding.UTF8.GetString(bytes);
            }
            catch
            {

                decode = input;
            }
            return decode;
        }

        public static string UrlEncode(string input)
        {
            return System.Web.HttpUtility.UrlEncode(input, Encoding.UTF8);
        }

        public static string UrlDecode(string input)
        {
            return System.Web.HttpUtility.UrlDecode(input, Encoding.UTF8);
        }
    }
}
