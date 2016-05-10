using Licenta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.Domain
{
    public interface IAuthUser
    {
        int Id { get; set; }
        string Username { get; set; }
        string Token { get; set; }
        UserType Type { get; }
    }
}
