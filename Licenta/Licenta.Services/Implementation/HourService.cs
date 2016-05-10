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
    public class HourService : Service<Hour, IHourRepository>, IHourService
    {
        public HourService()
        {
            base.repository = new HourRepository();
        }

        public List<Hour> GetHourForGroup(int groupId)
        {
            return repository.GetHourForGroup(groupId);
        }

        public List<Hour> GetHoursForTeacher(int teacherId)
        {
            var allHours = GetAll();
            return allHours.Where(h => h.TeacherId == teacherId).ToList();
        }
    }
}
