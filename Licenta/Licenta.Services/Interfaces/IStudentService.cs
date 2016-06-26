using Licenta.Domain.FilterDtos;
using Licenta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.Services.Interfaces
{
    public interface IStudentService : IService<Student>, IAccessGranter
    {
        List<Student> GetStudentsByFilter(StudentsFilter filter);
        Student GetStudentByUsername(string username);
        Student GetByEmail(string email);
    }
}
