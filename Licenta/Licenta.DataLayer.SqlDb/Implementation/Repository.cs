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
    public abstract class Repository<T> : IRepository<T>
        where T : BaseModel
    {
        public T GetById(int id)
        {
            using (var db = new Database(GlobalUsage.ConnectionString))
            {
                return db.FetchBy<T>(exp => exp.Where(item => item.Id == id)).FirstOrDefault();
            }
        }

        public virtual List<T> GetAll()
        {
            using (var db = new Database(GlobalUsage.ConnectionString))
            {
                return db.Fetch<T>();
            }
        }

        public bool Save(T entity)
        {
            using (var db = new Database(GlobalUsage.ConnectionString))
            {
                db.Save<T>(entity);
                return true;
            }
        }

        public bool Delete(T entity)
        {
            using (var db = new Database(GlobalUsage.ConnectionString))
            {
                db.Delete<T>(entity);
                return true;
            }
        }
    }
}
