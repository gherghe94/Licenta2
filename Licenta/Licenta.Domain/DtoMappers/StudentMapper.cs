using Licenta.Domain.Dtos;
using Licenta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.Domain.DtoMappers
{
    public static class StudentMapper
    {
        public static StudentDto GetDtoFrom(Student student)
        {
            return new StudentDto
            {
                Email = student.Email,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Username = student.Username,
                Id = student.Id,
                Group = student.Group,
                GroupId = student.Group.Id
            };
        }

        public static List<StudentDto> GetListDtosFrom(List<Student> students)
        {
            return students.Select(stud => StudentMapper.GetDtoFrom(stud)).ToList();
        }

        //todo: rest of the methods for groups and retranslate
    }
}
