using Licenta.Domain.FilterDtos;
using Licenta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.Services.Interfaces
{
    public interface ITeacherService : IService<Teacher>, IAccessGranter
    {
        List<Teacher> GetAllFiltered(TeacherFilter filter);
        Teacher GetLoggedTeacher(string usernameWithTitle);
    }
}
