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

            if (string.IsNullOrWhiteSpace(entity.Title))
                webValidatorResult.Append("Title cannot be empty!");

            if (string.IsNullOrWhiteSpace(entity.FirstName))
                webValidatorResult.Append("First name cannot be empty!");

            if (string.IsNullOrWhiteSpace(entity.LastName))
                webValidatorResult.Append("Last name cannot be empty!");

            if (string.IsNullOrWhiteSpace(entity.Email))
                webValidatorResult.Append("Email cannot be empty!");

            if (!IsUnique(entity, allTeachers))
                webValidatorResult.Append("Is not unique! Please search for a name, email combination that isn't already into the app!");

            return webValidatorResult;
        }

        private bool IsUnique(Teacher entity, List<Teacher> allTeachers)
        {
            if (entity == null)
                return true;

            if (entity.FirstName == null || entity.LastName == null || entity.Email == null)
                return true;

            bool nameCombination = allTeachers.Any(t =>
                        t.FirstName.ToLower() == entity.FirstName.ToLower()
                        && t.LastName.ToLower() == entity.LastName.ToLower());

            bool isEmailUnique = allTeachers.Any(t => t.Email.ToLower() == entity.Email.ToLower());

            if (nameCombination == true || isEmailUnique == true)
                return false;
            return true;
        }
    }
}