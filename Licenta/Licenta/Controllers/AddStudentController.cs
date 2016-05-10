using Licenta.Domain.Models;
using Licenta.Models.DTO;
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
    public class AddStudentController : ApiController
    {
        private readonly IStudentService studentService;
        private readonly IGroupService groupService;

        public AddStudentController()
        {
            studentService = new StudentService();
            groupService = new GroupService();
        }

        private Student GetStudent(AddStudentDto dto)
        {
            Group group = groupService.GetGroupByName(dto.Group);
            Student stud = new Student()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Group = group,
                GroupId = group.Id,
                Password = Student.GeneratePassword(),
                Username = Student.GenerateUsername(dto.FirstName, dto.LastName)
            };

            return stud;
        }

        [HttpPost]
        public IHttpActionResult AddStudent([FromBody] AddStudentDto student)
        {
            try
            {
                StudentDtoValidator validator = new StudentDtoValidator(studentService);
                WebValidatorResult requestValidationResult = validator.Validate(student);
                if (!requestValidationResult.IsOk)
                {
                    return Ok(requestValidationResult);
                }

                requestValidationResult.IsOk = studentService.Save(GetStudent(student));

                return Ok(requestValidationResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
