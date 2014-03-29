using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IService<T>
    {
        IEnumerable<T> GetAll();
        int Insert(T entity);
        int Update(T entity);
        int Remove(T entity);
        int Remove(int id);
    }
}
