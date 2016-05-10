using Licenta.Domain.FilterDtos;
using Licenta.Domain.Models;
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
    public class AnnouncementsController : ApiController
    {
        private readonly IAnnouncementService _announcementService;
        private readonly IGroupService _groupService;
        private readonly ITeacherService _teacherService;
        private readonly IStudentService _studentService;

        public AnnouncementsController()
        {
            _announcementService = new AnnouncementService();
            _groupService = new GroupService();
            _teacherService = new TeacherService();
            _studentService = new StudentService();
        }

        [HttpPost]
        public IHttpActionResult Save([FromBody] Announcement announcement)
        {
            try
            {
                IWebValidator<Announcement> validator = new AnnouncementValidator();
                var result = validator.Validate(announcement);
                if (result.IsOk)
                {
                    announcement.Date = DateTime.Now;
                    SendNotifications(announcement);
                    _announcementService.Save(announcement);
                    result.EntityId = announcement.Id;
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        private void FillAnnouncement(Announcement entity)
        {
            if (entity.Group == null)
                entity.Group = _groupService.GetById(entity.GroupId);

            if (entity.Teacher == null)
                entity.Teacher = _teacherService.GetById(entity.TeacherId);
        }

        private bool IsNew(Announcement anc)
        {
            return (anc.Id == 0);
        }

        private void SendNotifications(Announcement announcement)
        {
            if (!IsNew(announcement))
                return;

            FillAnnouncement(announcement);

            var allStudents = _studentService.GetStudentsByFilter(
                new StudentsFilter()
            {
                GroupName = announcement.Group.Name
            });

            foreach (var student in allStudents)
            {
                var task = Task.Run(() =>
                {
                    NotificationManager.SendNotification(announcement, student.Email);
                });
            }
        }

        [HttpGet]
        public IHttpActionResult GetAnnouncements([FromUri] int teacherId)
        {
            try
            {
                var teacherAnnouncements = _announcementService.GetTeacherAnnouncements(teacherId);
                teacherAnnouncements = teacherAnnouncements.OrderByDescending(a => a.Date).Take(10).ToList();
                return Ok(teacherAnnouncements);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
