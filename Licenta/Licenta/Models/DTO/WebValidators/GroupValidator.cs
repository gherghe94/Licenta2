using Licenta.Domain.Models;
using Licenta.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Licenta.Models.DTO.WebValidators
{
    public class GroupValidator : IWebValidator<Group>
    {
        private readonly IGroupService _groupService;

        public GroupValidator(IGroupService groupService)
        {
            _groupService = groupService;
        }

        public WebValidatorResult Validate(Group entity)
        {
            WebValidatorResult result = new WebValidatorResult();

            if (entity == null)
            {
                result.Append("Entity is null!");
                return result;
            }

            if (string.IsNullOrWhiteSpace(entity.Name))
                result.Append("Name is not completed!");

            if (string.IsNullOrWhiteSpace(entity.Specialization))
                result.Append("Specialization is not completed!");

            if (entity.Year <= 0 || entity.Year > 5)
                result.Append("The year has to be between 1 - 5 !");

            var unique = _groupService.IsUnique(entity);
            if (!unique)
                result.Append("Group name has to be unique!");

            return result;
        }
    }
}