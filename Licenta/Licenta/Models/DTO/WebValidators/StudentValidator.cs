using Licenta.Domain.Models;
using Licenta.Services.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Licenta.Models.DTO.WebValidators
{
    public class StudentValidator : IWebValidator<Student>
    {
        public WebValidatorResult Validate(Student entity)
        {
            WebValidatorResult webValidatorResult = new WebValidatorResult();

            if (string.IsNullOrWhiteSpace(entity.FirstName))
                webValidatorResult.Append("First name cannot be empty!");

            if (string.IsNullOrWhiteSpace(entity.LastName))
                webValidatorResult.Append("Last name cannot be empty!");

            if (string.IsNullOrWhiteSpace(entity.Email))
                webValidatorResult.Append("Email cannot be empty!");

            if (entity.GroupId < 1)
                webValidatorResult.Append("Group cannot be empty!");

            if (!IsUnique(entity) && IsNew(entity))
                webValidatorResult.Append("It's not unique! Email must be unique also the name!");

            return webValidatorResult;
        }

        private bool IsNew(Student entity)
        {
            return entity.Id == 0;
        }

        private bool IsUnique(Student entity)
        {
            StudentService studentService = new StudentService();
            var stud = studentService.GetByEmail(entity.Email);
            if (stud != null)
                return false;

            stud = studentService.GetStudentByUsername(Student.GenerateUsername(entity.FirstName, entity.LastName));
            if (stud != null)
                return false;

            return true;
        }
    }
}