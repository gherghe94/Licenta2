using Licenta.Domain.Models;
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

            if (entity.Id < 1)
                webValidatorResult.Append("Entity has been detached!");

            if (string.IsNullOrWhiteSpace(entity.FirstName))
                webValidatorResult.Append("First name cannot be empty!");

            if (string.IsNullOrWhiteSpace(entity.LastName))
                webValidatorResult.Append("Last name cannot be empty!");

            if (string.IsNullOrWhiteSpace(entity.Email))
                webValidatorResult.Append("Email cannot be empty!");

            if (entity.GroupId < 1)
                webValidatorResult.Append("Group cannot be empty!");

            return webValidatorResult;
        }
    }
}