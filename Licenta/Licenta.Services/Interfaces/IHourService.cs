using Licenta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.Services.Interfaces
{
    public interface IHourService : IService<Hour>
    {
        List<Hour> GetHourForGroup(int groupId);
        List<Hour> GetHoursForTeacher(int teacherId);
    }
}
