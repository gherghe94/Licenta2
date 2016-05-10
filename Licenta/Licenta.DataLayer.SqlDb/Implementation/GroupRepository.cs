using Licenta.DataLayer.SqlDb.Interfaces;
using Licenta.Domain.Models;
using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.DataLayer.SqlDb.Implementation
{
    public class GroupRepository : Repository<Group>, IGroupRepository
    {

        public Group GetGroupByName(string groupName)
        {
            using (var db = new Database(GlobalUsage.ConnectionString))
            {
                return db.FetchBy<Group>(exp => exp.Where(group => group.Name == groupName)).FirstOrDefault();
            }
        }
    }
}
