using Licenta.Domain.Models;
using Licenta.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Licenta.Models.DTO.WebValidators
{
    public class StudentDtoValidator : IWebValidator<AddStudentDto>
    {
        private const string NullEntity = "The entity is null";
        private const string MissingPropertyToken = "{0} is missing!";

        private readonly IStudentService studentService;

        public StudentDtoValidator(IStudentService service)
        {
            studentService = service;
        }

        private void AppendMissingPropertiesErrors(AddStudentDto entity, WebValidatorResult result)
        {
            foreach (var property in entity.GetType().GetProperties())
            {
                var stringValueOfProperty = property.GetValue(entity) as string;
                if (string.IsNullOrEmpty(stringValueOfProperty))
                {
                    result.Append(string.Format(MissingPropertyToken, property.Name));
                }
            }
        }

        private void CheckUniqueStudent(AddStudentDto entity, WebValidatorResult result)
        {
            
            var foundStud = studentService.GetStudentByUsername(Student.GenerateUsername(entity.FirstName, entity.LastName));
            if (foundStud != null)
                result.Append("The student already exists!");
        }

        public WebValidatorResult Validate(AddStudentDto entity)
        {
            WebValidatorResult result = new WebValidatorResult();
            if (entity == null)
            {
                result.Append(NullEntity);
                return result;
            }

            AppendMissingPropertiesErrors(entity, result);

            CheckUniqueStudent(entity, result);

            return result;
        }
       
    }
}