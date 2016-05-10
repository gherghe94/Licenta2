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
    public class StudentProfileController : ApiController
    {
        private readonly IStudentService _studentService;

        public StudentProfileController()
        {
            _studentService = new StudentService();
        }

        [HttpPost]
        public IHttpActionResult Save([FromBody] Student teacher)
        {
            try
            {
                var dbTeacher = _studentService.GetById(teacher.Id);
                dbTeacher.Email = teacher.Email;
                var result = _studentService.Save(dbTeacher);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
