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
    public class TeacherCourseService : Service<TeacherCourse, ITeacherCourseRepository>, ITeacherCourseService
    {
        public TeacherCourseService()
        {
            base.repository = new TeacherCourseRepository();
        }

        public List<Course> GetTeachedCourses(int teacherId)
        {
            ICourseService courseRepository = new CourseService();
            List<TeacherCourse> result = repository.GetTeachedCourses(teacherId);

            return result.Select(tcs => courseRepository.GetById(tcs.CourseId)).ToList();
        }

        public void ReplaceCoursesFor(int teacherId, List<Domain.Dtos.CourseDto> courses)
        {
            repository.RemoveAllFrom(teacherId);
            repository.AddAll(teacherId, courses);
        }


        public List<Teacher> GetAllTeachers(int courseId)
        {
            ITeacherService teacherService = new TeacherService();
            var all = GetAll().Where(q => q.CourseId == courseId).ToList();
            return all.Select(w => teacherService.GetById(w.TeacherId)).ToList();
        }


        public TeacherCourse GetAllTCByIds(int teacherId, int courseId)
        {
            var all = GetAll();
            return all.FirstOrDefault(q => q.CourseId == courseId && q.TeacherId == teacherId);
        }
    }
}
