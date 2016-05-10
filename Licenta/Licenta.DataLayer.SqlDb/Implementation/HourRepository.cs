using Licenta.DataLayer.SqlDb.Interfaces;
using Licenta.Domain.Models;
using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.DataLayer.SqlDb.Implementation
{
    public class HourRepository : Repository<Hour>, IHourRepository
    {
        public List<Hour> GetHourForGroup(int groupId)
        {
            using (var db = new Database(GlobalUsage.ConnectionString))
            {
                var lazyHours = db.FetchBy<Hour>(exp => exp.Where(h => h.GroupId == groupId));
                return lazyHours.Select(e => GetFullHour(e)).ToList();
            }
        }

        public Hour GetFullHour(Hour hour)
        {
            ITeacherRepository teacherRepo = new TeacherRepository();
            IRoomRepository roomRepo = new RoomRepository();
            ICourseRepository courseRepo = new CourseRepository();
            IGroupRepository groupRepo = new GroupRepository();

            hour.Teacher = teacherRepo.GetById(hour.TeacherId);
            hour.Room = roomRepo.GetById(hour.RoomId);
            hour.Course = courseRepo.GetById(hour.CourseId);
            hour.Group = groupRepo.GetById(hour.GroupId);

            return hour;
        }

        public override List<Hour> GetAll()
        {
            var lazyHours = base.GetAll();
            return lazyHours.Select(e => GetFullHour(e)).ToList();
        }
    }
}
