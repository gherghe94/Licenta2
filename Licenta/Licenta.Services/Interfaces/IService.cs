using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.Services.Interfaces
{
    public interface IService<T>
    {
        List<T> GetAll();
        bool Save(T entity);
        bool Delete(T entity);
        T GetById(int id);
    }
}
