using Licenta.DataLayer.SqlDb.Interfaces;
using Licenta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.DataLayer.SqlDb.Implementation
{
    public class AdminRepository : Repository<Admin> , IAdminRepository
    {
        public Admin GetAdmin()
        {
            return Admin.GetInstance();
        }
    }
}
