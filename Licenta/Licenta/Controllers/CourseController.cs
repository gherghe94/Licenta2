using Licenta.Domain.FilterDtos;
using Licenta.Domain.Models;
using Licenta.Models.DTO.WebValidators;
using Licenta.Services.Implementation;
using Licenta.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Licenta.Controllers
{
    [Authorize]
    public class CourseController : ApiController
    {
        private readonly ICourseService _courseService;

        public CourseController()
        {
            _courseService = new CourseService();
        }

        [HttpGet]
        public IHttpActionResult GetAll([FromUri] CourseFilter filter)
        {
            try
            {
                filter.ToLower();
                var result = _courseService.GetFilteredCourses(filter);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult Save([FromBody] Course course)
        {
            try
            {
                IWebValidator<Course> validator = new CourseValidator(_courseService);
                WebValidatorResult result = validator.Validate(course);
                if (result.IsOk)
                    _courseService.Save(course);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromBody] Course entity)
        {
            try
            {
                var result = _courseService.Delete(entity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
