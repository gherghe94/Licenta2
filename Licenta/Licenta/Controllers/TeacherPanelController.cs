using Licenta.Domain.Models;
using Licenta.Models;
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
    public class TeacherPanelController : ApiController
    {

        private readonly IHourService _hourService;
        private readonly ITeacherService _teacherService;
        private List<BarItem> GetItems()
        {
            var items = new List<BarItem>();
            items.Add(new BarItem() { Id = 1, Name = "My Profile", Location = "mainTeacher/myProfile/myProfile.html" });
            items.Add(new BarItem() { Id = 2, Name = "My Hours", Location = "mainTeacher/myHours/myHours.html" });
            items.Add(new BarItem() { Id = 3, Name = "My Announcements", Location = "mainTeacher/myAnnouncements/myAnnouncements.html" });
            return items;
        }

        public TeacherPanelController()
        {
            _hourService = new HourService();
            _teacherService = new TeacherService();
        }

        [HttpGet]
        public IHttpActionResult GetBarItems()
        {
            var items = GetItems();

            return Ok(items);
        }

        [HttpGet]
        public IHttpActionResult GetHours([FromUri] int teacherId)
        {
            try
            {
                List<Hour> hours = _hourService.GetHoursForTeacher(teacherId);

                return Ok(hours);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
