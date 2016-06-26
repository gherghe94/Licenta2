using Licenta.Domain.Models;
using Licenta.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Licenta.Models.DTO.WebValidators
{
    public class CourseValidator : IWebValidator<Course>
    {
        private readonly ICourseService _courseService;

        public CourseValidator(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public WebValidatorResult Validate(Course entity)
        {
            WebValidatorResult result = new WebValidatorResult();

            if (entity == null)
            {
                result.Append("Entity is null!");
                return result;
            }

            if (string.IsNullOrEmpty(entity.Name))
                result.Append("Name is mandatory!");

            if (entity.Year <= 0 || entity.Year > 5)
                result.Append("Year has to be in 1-5 range!");

            if (entity.Credits <= 0 || entity.Credits > 6)
                result.Append("Credits has to be in 1-6 range!");

            if (!IsUnique(entity) && IsNew(entity))
                result.Append("Course is not unique! There is already a course named like this: " + entity.Name);

            return result;
        }

        private bool IsNew(Course entity)
        {
            return entity.Id == 0;
        }

        private bool IsUnique(Course entity)
        {
            var allCourses = _courseService.GetAll();
            if (entity == null)
                return true;

            if (entity.Name == null)
                return true;

            return allCourses.All(c => c.Name.ToLower() != entity.Name.ToLower());
        }
    }
}