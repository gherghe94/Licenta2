using Licenta.Domain.FilterDtos;
using Licenta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.Services.Interfaces
{
    public interface IGroupService : IService<Group>
    {
        Group GetGroupByName(string groupName);
        bool IsUnique(Group entity);
        List<Group> GetAllFiltered(GroupFilter filter);
    }
}
