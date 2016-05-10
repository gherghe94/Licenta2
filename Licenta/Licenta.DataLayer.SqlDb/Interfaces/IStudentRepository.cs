using Licenta.Domain.FilterDtos;
using Licenta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.DataLayer.SqlDb.Interfaces
{
    public interface IStudentRepository : IRepository<Student>
    {
        Student GetStudentByUsername(string username);
        List<Student> GetStudentsByFilter(StudentsFilter filter);
    }
}
