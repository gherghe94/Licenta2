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
    public class HoursController : ApiController
    {
        private readonly IHourService _hourService;
        private readonly ITeacherCourseService _teacherCourseService;

        public HoursController()
        {
            _hourService = new HourService();
            _teacherCourseService = new TeacherCourseService();
        }

        [HttpGet]
        public IHttpActionResult GetHours([FromUri] int groupId)
        {
            try
            {
                var all = _hourService.GetHourForGroup(groupId);
                for (int i = 0; i < all.Count; ++i)
                {
                    var hour = all[i];
                    var x = _teacherCourseService.GetAllTCByIds(hour.TeacherId, hour.CourseId);
                    if (x == null)
                    {
                        all.RemoveAll(q => q.Id == hour.Id);
                        _hourService.Delete(hour);
                    }
                }

                return Ok(all);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private void CompleteHour(Hour hour)
        {
            if (hour == null)
                return;

            if (hour.Course != null)
                hour.CourseId = hour.Course.Id;

            if (hour.Group != null)
                hour.GroupId = hour.Group.Id;

            if (hour.Teacher != null)
                hour.TeacherId = hour.Teacher.Id;

            if (hour.Room != null)
                hour.RoomId = hour.Room.Id;
        }

        [HttpPost]
        public IHttpActionResult Save([FromBody] Hour hour)
        {
            try
            {
                CompleteHour(hour);
                IWebValidator<Hour> validator = new HourValidator(_hourService);
                WebValidatorResult result = validator.Validate(hour);
                if (result.IsOk)
                    _hourService.Save(hour);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromBody] Hour hour)
        {
            try
            {
                var result = _hourService.Delete(hour);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
