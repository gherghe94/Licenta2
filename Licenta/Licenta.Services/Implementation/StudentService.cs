using Licenta.DataLayer.SqlDb.Implementation;
using Licenta.DataLayer.SqlDb.Interfaces;
using Licenta.Domain.FilterDtos;
using Licenta.Domain.Models;
using Licenta.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.Services.Implementation
{
    public class StudentService : Service<Student, IStudentRepository>, IStudentService
    {
        public StudentService()
        {
            base.repository = new StudentRepository();
        }

        public bool HasAccess(string username, string password)
        {
            Student student = repository.GetStudentByUsername(username);
            if (student == null)
                return false;

            return student.Password == password;
        }

        public EUserType GetUserType()
        {
            return EUserType.Student;
        }

        private void NullifyPropertiesIfStringEmpty(StudentsFilter filter)
        {
            var properties = filter.GetType().GetProperties();
            foreach (var property in properties)
            {
                string value = property.GetValue(filter) as string;
                if (value != null) // just strings
                {
                    if (value == string.Empty)
                    {
                        property.SetValue(filter, null);
                    }
                }
            }
        }

        public List<Student> GetStudentsByFilter(StudentsFilter filter)
        {
            if (filter == null || filter.RetrieveAll)
            {
                return repository.GetAll();
            }

            NullifyPropertiesIfStringEmpty(filter);
            return repository.GetStudentsByFilter(filter); // introduce pagination
        }


        public Student GetStudentByUsername(string username)
        {
            return repository.GetStudentByUsername(username);
        }
    }
}
