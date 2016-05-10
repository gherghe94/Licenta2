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
    public class TeacherCourseRepository : Repository<TeacherCourse>, ITeacherCourseRepository
    {
        public List<TeacherCourse> GetTeachedCourses(int teacherId)
        {
            using (var database = new Database(GlobalUsage.ConnectionString))
            {
                var tcs = database.FetchBy<TeacherCourse>(exp => exp.Where(q => q.TeacherId == teacherId));
                return tcs;
            }
        }

        public void RemoveAllFrom(int teacherId)
        {
            using (var database = new Database(GlobalUsage.ConnectionString))
            {
                var allTC = GetTeachedCourses(teacherId);
                allTC.ForEach(q => database.Delete(q));
            }
        }

        public void AddAll(int teacherId, List<Domain.Dtos.CourseDto> courses)
        {
            using (var database = new Database(GlobalUsage.ConnectionString))
            {
                var justChecked = courses.Where(q => q.IsChecked == true).ToList();

                justChecked.ForEach(q => database.Save<TeacherCourse>(
                    new TeacherCourse
                    {
                        TeacherId = teacherId,
                        CourseId = q.Id
                    }));
            }
        }
    }
}
