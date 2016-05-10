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
    public class GroupService : Service<Group, IGroupRepository>, IGroupService
    {
        public GroupService()
        {
            base.repository = new GroupRepository();
        }

        public Group GetGroupByName(string groupName)
        {
            if (groupName == null)
                throw new ArgumentNullException("Group name is null");
            return repository.GetGroupByName(groupName);
        }


        public bool IsUnique(Group entity)
        {
            var allGroups = GetAll();
            allGroups.RemoveAll(q => q.Id == entity.Id);
            return !allGroups.Any(q => q.Name == entity.Name);
        }


        public List<Group> GetAllFiltered(Domain.FilterDtos.GroupFilter filter)
        {
            filter.ToLower();

            var result = GetAll();

            result = result.Where(c => c.Name.ToLower().Contains(filter.Name)).ToList();
            result = result.Where(c => c.Specialization.ToLower().Contains(filter.Specialization)).ToList();

            if (filter.Year.HasValue)
                result = result.Where(c => c.Year == filter.Year.Value).ToList();

            return result;
        }
    }
}
