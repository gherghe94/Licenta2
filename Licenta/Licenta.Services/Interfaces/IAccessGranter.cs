using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.Services.Interfaces
{
    public enum EUserType
    {
        Student = 0,
        Teacher = 1,
        Admin = 2
    }

    public interface IAccessGranter
    {
        bool HasAccess(string username, string password);
        EUserType GetUserType();
    }
}
