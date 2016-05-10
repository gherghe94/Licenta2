using Licenta.DataLayer.SqlDb.Interfaces;
using Licenta.Domain.FilterDtos;
using Licenta.Domain.Models;
using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.DataLayer.SqlDb.Implementation
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        private const string pleaseSelectToken = "Please select!";

        private Student DecorateStudentWithGroupDetails(Student student, Database database)
        {
            if (student == null) return null;

            student.Group = database.FetchBy<Group>(
                exp =>
                    exp.Where(group => group.Id == student.GroupId)).FirstOrDefault();
            return student;
        }

        private List<Student> DecorateStudentWithGroupDetails(List<Student> students, Database database)
        {
            return students.Select(stud => DecorateStudentWithGroupDetails(stud, database)).ToList();
        }

        public Student GetStudentByUsername(string username)
        {
            using (var db = new Database(GlobalUsage.ConnectionString))
            {
                var foundStud = db.FetchBy<Student>(p => p.Where(stud => stud.Username == username)).FirstOrDefault();
                return DecorateStudentWithGroupDetails(foundStud, db);
            }
        }

        public List<Student> GetStudentsByFilter(StudentsFilter filter)
        {
            List<Student> students = GetAll();

            if (filter.FirstName != null)
                students.RemoveAll(stud => !stud.FirstName.ToLower().Contains(filter.FirstName.ToLower()));

            if (filter.LastName != null)
                students.RemoveAll(stud => !stud.LastName.ToLower().Contains(filter.LastName.ToLower()));

            if (filter.GroupName != null && filter.GroupName != pleaseSelectToken)
                students.RemoveAll(stud => !stud.Group.Name.Contains(filter.GroupName));

            return students;
        }

        public override List<Student> GetAll()
        {
            using (var db = new Database(GlobalUsage.ConnectionString))
            {
                var foundStuds = db.Fetch<Student>();
                return DecorateStudentWithGroupDetails(foundStuds.OrderBy(s => s.LastName).ToList(), db);
            }
        }
    }
}
