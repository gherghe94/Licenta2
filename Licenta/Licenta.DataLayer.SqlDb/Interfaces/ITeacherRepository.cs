﻿using Licenta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.DataLayer.SqlDb.Interfaces
{
    public interface ITeacherRepository : IRepository<Teacher>
    {
        Teacher GetTeacherWithCredentials(string title, string user, string pwd);
    }
}
