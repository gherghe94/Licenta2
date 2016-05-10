using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Licenta.Models.DTO
{
    public class LoginData
    {
        public string Username { get; set; }
        public string Password { get; set; }
        //Create enum for user type

        private byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public string ToBase64()
        {
            string concat = Username + ":" + Password;
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(concat));
        }
    }
}