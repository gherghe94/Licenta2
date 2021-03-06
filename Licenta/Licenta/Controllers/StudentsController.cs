﻿using Licenta.Domain.DtoMappers;
using Licenta.Domain.Dtos;
using Licenta.Domain.FilterDtos;
using Licenta.Domain.Models;
using Licenta.Models.DTO;
using Licenta.Models.DTO.WebValidators;
using Licenta.Services;
using Licenta.Services.Implementation;
using Licenta.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Licenta.Controllers
{
    [Authorize]
    public class StudentsController : ApiController
    {
        private readonly IStudentService studentService;
        private readonly IAnnouncementService announcementService;

        public StudentsController()
        {
            studentService = new StudentService();
            announcementService = new AnnouncementService();
        }

        [HttpGet]
        public IHttpActionResult GetGroupAnnouncements([FromUri] int groupId)
        {
            var announcements = announcementService.GetGroupAnnouncements(groupId);
            return Ok(announcements);
        }

        [HttpPost]
        public IHttpActionResult GetStudents([FromBody] StudentsFilter studentsFilter)
        {
            try
            {
                List<Student> matchingStudents = studentService.GetStudentsByFilter(studentsFilter);
                List<StudentDto> matchingDtos = StudentMapper.GetListDtosFrom(matchingStudents);

                return Ok(matchingDtos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private void DecorateStudent(Student entity)
        {
            if (entity.Id == 0)
            {
                entity.Password = Student.GeneratePassword();
                entity.Username = Student.GenerateUsername(entity.FirstName, entity.LastName);
            }
        }

        [HttpPut]
        public IHttpActionResult EditStudent([FromBody] Student entity)
        {
            try
            {
                StudentValidator validator = new StudentValidator();
                WebValidatorResult validationResult = validator.Validate(entity);
                if (!validationResult.IsOk)
                {
                    return Ok(validationResult);
                }

                DecorateStudent(entity);
                SendPassword(entity);
                validationResult.IsOk = studentService.Save(entity);
                return Ok(validationResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        private void SendPassword(Student entity)
        {
            if (entity.Id == 0)
            {
                var task = Task.Run(() =>
                {
                    NotificationManager.SendNewCommerNotification(entity);
                });
            }
        }

        [HttpDelete]
        public IHttpActionResult RemoveStudent([FromBody] Student entity)
        {
            try
            {
                StudentValidator validator = new StudentValidator();
                WebValidatorResult validationResult = validator.Validate(entity);
                if (!validationResult.IsOk)
                {
                    return Ok(validationResult);
                }

                validationResult.IsOk = studentService.Delete(entity);

                return Ok(validationResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
