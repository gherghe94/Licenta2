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
    public class TeacherService : Service<Teacher, ITeacherRepository>, ITeacherService
    {
        public TeacherService()
        {
            base.repository = new TeacherRepository();
        }

        public override List<Teacher> GetAll()
        {
            ITeacherCourseService teacherCourseService = new TeacherCourseService();

            var result = base.GetAll();
            result.ForEach(t => t.Courses = teacherCourseService.GetTeachedCourses(t.Id));

            return result;
        }

        public List<Teacher> GetAllFiltered(Domain.FilterDtos.TeacherFilter filter)
        {
            filter.ToLower();

            var result = GetAll();

            result = result.Where(t => t.FullName.ToLower().Contains(filter.FullName)).ToList();
            result = result.Where(t => t.Title.ToLower().Contains(filter.Title)).ToList();

            return result;
        }

        public bool HasAccess(string username, string password)
        {
            var splitted = username.Split('.');
            if (splitted.Length > 2)
            {
                string title = username.Split('.')[0];
                string firstName = username.Split('.')[1];
                string lastName = username.Split('.')[2];

                var teacher = repository.GetTeacherWithCredentials(
                    title, string.Format("{0}.{1}", firstName, lastName), password);

                return (teacher != null);
            }

            return false;
        }

        public EUserType GetUserType()
        {
            return EUserType.Teacher;
        }

        public Teacher GetLoggedTeacher(string usernameWithTitle)
        {
            var splitted = usernameWithTitle.Split('.');
            if (splitted.Length > 2)
            {
                string title = usernameWithTitle.Split('.')[0].ToLower();
                string firstName = usernameWithTitle.Split('.')[1].ToLower();
                string lastName = usernameWithTitle.Split('.')[2].ToLower();

                string uName = string.Format("{0}.{1}", firstName, lastName);

                var allTeachers = repository.GetAll();
                return allTeachers.FirstOrDefault(t =>
                    t.Title.ToLower() == title && t.Username == uName);
            }

            return null;
        }
    }
}
