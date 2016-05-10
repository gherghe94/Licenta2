using Licenta.Domain.Models;
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
    public class TeacherProfileController : ApiController
    {
        private readonly ITeacherCourseService _teacherCourseService;
        private readonly ICourseService _courseService;
        private readonly ITeacherService _teacherService;

        public TeacherProfileController()
        {
            _teacherCourseService = new TeacherCourseService();
            _courseService = new CourseService();
            _teacherService = new TeacherService();
        }

        [HttpGet]
        public IHttpActionResult GetCourses([FromUri] int teacherId)
        {
            try
            {
                var allTcs = _teacherCourseService.GetTeachedCourses(teacherId);

                return Ok(allTcs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult Save([FromBody] Teacher teacher)
        {
            try
            {
                var dbTeacher = _teacherService.GetById(teacher.Id);
                dbTeacher.Notes = teacher.Notes;
                dbTeacher.Email = teacher.Email;
                var result = _teacherService.Save(dbTeacher);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
