using Licenta.DataLayer.SqlDb.Interfaces;
using Licenta.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.Services.Implementation
{
    public abstract class Service<T, U> : IService<T>
        where U : IRepository<T>
        where T : class
    {
        protected U repository;

        public virtual List<T> GetAll()
        {
            return repository.GetAll();
        }

        public bool Save(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Entity cannot be null at saving!");

            try
            {
                repository.Save(entity);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public bool Delete(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Entity cannot be null at removing!");

            try
            {
                repository.Delete(entity);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public T GetById(int id)
        {
            return repository.GetById(id);
        }
    }
}
