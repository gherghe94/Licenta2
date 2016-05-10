using Licenta.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Licenta.Models.DTO.ResponseDto
{
    public class LoginResponse
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public EUserType Type { get; set; }

        public string TypeToken
        {
            get
            {
                return Type.ToString();
            }
        }
    }
}