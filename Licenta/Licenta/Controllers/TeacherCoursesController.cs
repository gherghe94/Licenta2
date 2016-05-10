using Licenta.Domain.Dtos;
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
    public class TeacherCoursesController : ApiController
    {
        private readonly ITeacherCourseService _teacherCourseService;

        public TeacherCoursesController()
        {
            _teacherCourseService = new TeacherCourseService();
        }

        [HttpPost]
        public IHttpActionResult Save([FromBody] TeacherWithCoursesDto dto)
        {
            try
            {
                _teacherCourseService.ReplaceCoursesFor(dto.TeacherId, dto.Courses);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetAllTeachersFor([FromUri] int courseId)
        {
            try
            {
                var tcs = _teacherCourseService.GetAllTeachers(courseId);
                return Ok(tcs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
