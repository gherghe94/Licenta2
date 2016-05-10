using Licenta.DataLayer.SqlDb.Implementation;
using Licenta.DataLayer.SqlDb.Interfaces;
using Licenta.Domain.Models;
using Licenta.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.Services.Implementation
{
    public class AnnouncementService : Service<Announcement, IAnnouncementRepository>, IAnnouncementService
    {
        public AnnouncementService()
        {
            base.repository = new AnnouncementRepository();
        }

        public override List<Announcement> GetAll()
        {
            var all = base.GetAll();

            IGroupService groupService = new GroupService();
            ITeacherService teacherService = new TeacherService();

            all.ForEach(a => a.Group = groupService.GetById(a.GroupId));
            all.ForEach(a => a.Teacher = teacherService.GetById(a.TeacherId));

            return all;
        }

        public List<Announcement> GetTeacherAnnouncements(int teacherId)
        {
            List<Announcement> allAnnouncements = GetAll();
            return allAnnouncements.Where(a => a.TeacherId == teacherId).ToList();
        }

        public List<Announcement> GetGroupAnnouncements(int groupId)
        {
            var allAnnouncements = GetAll();
            return allAnnouncements
                .Where(a => a.GroupId == groupId)
                .OrderByDescending(a => a.Date)
                .Take(10)
                .ToList();
        }
    }
}
