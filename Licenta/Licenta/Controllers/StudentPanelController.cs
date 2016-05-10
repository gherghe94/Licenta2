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
    public class StudentPanelController : ApiController
    {
        private readonly IHourService _hourService;

        public StudentPanelController()
        {
            _hourService = new HourService();
        }

        private List<BarItem> GetItems()
        {
            var items = new List<BarItem>();
            items.Add(new BarItem() { Id = 1, Name = "My Profile", Location = "mainStudent/myProfile/myStudentProfile.html" });
            items.Add(new BarItem() { Id = 2, Name = "My Hours", Location = "mainStudent/myHours/myStudentHours.html" });
            items.Add(new BarItem() { Id = 3, Name = "Group Announcements", Location = "mainStudent/myStudentAnnouncements/myStudentAnnouncements.html" });
            return items;
        }

        [HttpGet]
        public IHttpActionResult GetBarItems()
        {
            var items = GetItems();
            return Ok(items);
        }

        [HttpGet]
        public IHttpActionResult GetHours([FromUri] int groupId)
        {
            try
            {
                List<Hour> hours = _hourService.GetHourForGroup(groupId);
                return Ok(hours);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
