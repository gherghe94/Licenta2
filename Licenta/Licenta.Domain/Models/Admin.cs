using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.Domain.Models
{
    public class Admin : BaseModel
    {
        public string Username { get; private set; }
        public string Password { get; private set; }

        private Admin()
        {
            Username = "Admin";
            Password = "1234";
        }

        private static Admin _instance;

        public static Admin GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Admin();
            }

            return _instance;
        }

        public bool Validate(string username, string password)
        {
            return Username == username && Password == password;
        }
    }
}
