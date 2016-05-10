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
    public class TeacherController : ApiController
    {
        private readonly ITeacherService _teacherService;

        public TeacherController()
        {
            _teacherService = new TeacherService();
        }

        [HttpGet]
        public IHttpActionResult GetAll([FromUri] TeacherFilter filter)
        {
            try
            {
                var result = _teacherService.GetAllFiltered(filter);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult Add([FromBody] Teacher entity)
        {
            try
            {
                IWebValidator<Teacher> validator = new TeacherValidator(_teacherService);
                WebValidatorResult webValidationResult = validator.Validate(entity);
                if (webValidationResult.IsOk)
                {
                    CompleteRegistration(entity);
                    _teacherService.Save(entity);
                }

                return Ok(webValidationResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private bool IsNewAccount(Teacher entity)
        {
            return entity.Id == 0;
        }

        private void CompleteRegistration(Teacher entity)
        {
            if (!IsNewAccount(entity))
                return;

            entity.Password = Guid.NewGuid().ToString();
            entity.Username = string.Format("{0}.{1}", entity.FirstName.ToLower() , entity.LastName.ToLower());
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromBody] Teacher entity)
        {
            try
            {
                var result = _teacherService.Delete(entity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
