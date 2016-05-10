using Licenta.DataLayer.SqlDb.Implementation;
using Licenta.DataLayer.SqlDb.Interfaces;
using Licenta.Domain.Models;
using Licenta.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.Services.Implementation
{
    public class AdminService : Service<Admin, IAdminRepository>, IAdminService
    {
        public AdminService()
        {
            base.repository = new AdminRepository();
        }

        public bool HasAccess(string username, string password)
        {
            Admin admin = repository.GetAdmin();
            return admin.Validate(username, password);
        }

        public EUserType GetUserType()
        {
            return EUserType.Admin;
        }
    }
}
