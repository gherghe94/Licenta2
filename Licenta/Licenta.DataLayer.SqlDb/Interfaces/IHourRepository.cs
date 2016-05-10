﻿using Licenta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.DataLayer.SqlDb.Interfaces
{
    public interface IHourRepository : IRepository<Hour>
    {
        Hour GetFullHour(Hour hour);
        List<Hour> GetHourForGroup(int groupId);
    }
}
