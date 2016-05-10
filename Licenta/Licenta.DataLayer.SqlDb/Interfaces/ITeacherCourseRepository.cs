using Licenta.Domain.Dtos;
using Licenta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.DataLayer.SqlDb.Interfaces
{
    public interface ITeacherCourseRepository : IRepository<TeacherCourse>
    {
        List<TeacherCourse> GetTeachedCourses(int teacherId);
        void RemoveAllFrom(int teacherId);
        void AddAll(int teacherId, List<CourseDto> courses);
    }
}
