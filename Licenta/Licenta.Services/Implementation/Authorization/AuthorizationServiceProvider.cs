using Licenta.DataLayer.SqlDb.Implementation;
using Licenta.DataLayer.SqlDb.Interfaces;
using Licenta.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.Services.Implementation.Authorization
{
    public class AuthorizationServiceProvider
    {
        private IStudentRepository studentRepository;

        public AuthorizationServiceProvider()
        {
            studentRepository = new StudentRepository();
        }

        public IAuthUser Authenticate(string username, string token)
        {
            // Search into students - check
            // Search into teachers
            // Search if is admin
            var stud = studentRepository.Authenticate(username, token);

            return stud;
        }
    }
}
