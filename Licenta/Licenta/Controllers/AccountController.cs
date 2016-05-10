using Licenta.Domain.Models;
using Licenta.Filters;
using Licenta.Models.DTO;
using Licenta.Models.DTO.ResponseDto;
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
    public class AccountController : ApiController
    {
        private List<IAccessGranter> _granters;

        public AccountController()
        {
            _granters = new List<IAccessGranter>();
            _granters.Add(new TeacherService());
            _granters.Add(new StudentService());
            _granters.Add(new AdminService());
        }

        private IAccessGranter GetAllowedGranter(string username, string password)
        {
            return _granters.FirstOrDefault(granter => granter.HasAccess(username, password));
        }

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult Login([FromBody]LoginData loginCredentials)
        {
            IAccessGranter granter = GetAllowedGranter(loginCredentials.Username, loginCredentials.Password);
            if (granter != null)
            {
                LoginResponse response = new LoginResponse()
                {
                    Username = loginCredentials.Username,
                    Token = loginCredentials.ToBase64(),
                    Type = granter.GetUserType()
                };

                return Ok(response);
            }
            else
            {
                return BadRequest("Bad login");
            }
        }

        [HttpGet]
        public IHttpActionResult GetLoggedInUser([FromUri] string username, string userType)
        {
            switch (userType)
            {
                case "Student":
                    {
                        IStudentService studentService = new StudentService();
                        var student = studentService.GetStudentByUsername(username);
                        return Ok(student);
                    }
                case "Teacher":
                    {
                        ITeacherService teacherService = new TeacherService();
                        var teacher = teacherService.GetLoggedTeacher(username);
                        return Ok(teacher);
                    }
                default:
                    {
                        return Ok();
                    }
            }
        }
    }
}
