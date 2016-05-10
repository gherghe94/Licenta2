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
    public class TeacherRepository : Repository<Teacher>, ITeacherRepository
    {

        public Teacher GetTeacherWithCredentials(string title, string user, string pwd)
        {
            title = title.ToLower();
            user = user.ToLower();

            using (var db = new Database(GlobalUsage.ConnectionString))
            {
                var result = db.FetchBy<Teacher>(exp =>
                    exp.Where(t =>
                        t.Title.ToLower() == title && t.Username == user && t.Password == pwd));
                return result.FirstOrDefault();
            }
        }
    }
}
