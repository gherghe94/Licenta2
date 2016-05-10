using Licenta.Domain.DtoMappers;
using Licenta.Domain.Dtos;
using Licenta.Domain.FilterDtos;
using Licenta.Domain.Models;
using Licenta.Models;
using Licenta.Models.DTO;
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
    public class MainAdministrationController : ApiController
    {
        private List<BarItem> GetItems()
        {
            var items = new List<BarItem>();

            items.Add(new BarItem() { Name = "Manage Students", Location = "mainAdmin/manageStudents/manageStudents.html" });
            items.Add(new BarItem() { Name = "Manage Teachers", Location = "mainAdmin/manageTeachers/manageTeachers.html" });
            items.Add(new BarItem() { Name = "Manage Rooms", Location = "mainAdmin/manageRooms/manageRooms.html" });
            items.Add(new BarItem() { Name = "Manage Groups", Location = "mainAdmin/manageGroups/manageGroups.html" });
            items.Add(new BarItem() { Name = "Manage Courses", Location = "mainAdmin/manageCourses/manageCourses.html" });
            items.Add(new BarItem() { Name = "Manage Schedule", Location = "mainAdmin/manageSchedule/manageSchedule.html" });

            return items;
        }

        [HttpGet]
        public IHttpActionResult GetBarItems()
        {
            List<BarItem> items = GetItems();
            return Ok(items);
        }
    }
}
