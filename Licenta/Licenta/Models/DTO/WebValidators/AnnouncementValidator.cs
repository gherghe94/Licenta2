using Licenta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Licenta.Models.DTO.WebValidators
{
    public class AnnouncementValidator : IWebValidator<Announcement>
    {
        public WebValidatorResult Validate(Announcement entity)
        {
            WebValidatorResult webValidatorResult = new WebValidatorResult();

            if (entity == null)
            {
                webValidatorResult.Append("Entity is null!");
                return webValidatorResult;
            }

            if (string.IsNullOrWhiteSpace(entity.Description))
                webValidatorResult.Append("The desciption is empty!");

            if (entity.GroupId == 0)
                webValidatorResult.Append("Group is empty!");

            if (entity.TeacherId == 0)
                webValidatorResult.Append("Teacher is missing!");

            return webValidatorResult;
        }
    }
}