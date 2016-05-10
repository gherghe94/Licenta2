using Licenta.Domain.Models;
using Licenta.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Licenta.Models.DTO.WebValidators
{
    public class TeacherValidator : IWebValidator<Teacher>
    {
        private readonly ITeacherService _teacherService;

        public TeacherValidator(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        public WebValidatorResult Validate(Teacher entity)
        {
            WebValidatorResult webValidatorResult = new WebValidatorResult();

            if (entity == null)
            {
                webValidatorResult.Append("Entity is null!");
                return webValidatorResult;
            }

            var allTeachers = _teacherService.GetAll();
            allTeachers.RemoveAll(t => t.Id == entity.Id);

            if (allTeachers.Any(t => t.FirstName == entity.FirstName && t.LastName == entity.LastName))
                webValidatorResult.Append("The teacher has to be unique (First name & Last name)!");

            if (string.IsNullOrWhiteSpace(entity.Title))
                webValidatorResult.Append("Title cannot be empty!");

            if (string.IsNullOrWhiteSpace(entity.FirstName))
                webValidatorResult.Append("First name cannot be empty!");

            if (string.IsNullOrWhiteSpace(entity.LastName))
                webValidatorResult.Append("Last name cannot be empty!");

            if (string.IsNullOrWhiteSpace(entity.Email))
                webValidatorResult.Append("Email cannot be empty!");

            return webValidatorResult;
        }
    }
}