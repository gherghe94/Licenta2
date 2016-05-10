using Licenta.Domain.Dtos;
using Licenta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.Services.Interfaces
{
    public interface ITeacherCourseService : IService<TeacherCourse>
    {
        List<Course> GetTeachedCourses(int teacherId);
        void ReplaceCoursesFor(int teacherId, List<CourseDto> courses);
        List<Teacher> GetAllTeachers(int courseId);

        TeacherCourse GetAllTCByIds(int teacherId, int courseId);
    }
}
