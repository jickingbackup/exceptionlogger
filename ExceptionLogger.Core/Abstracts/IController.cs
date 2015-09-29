using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionLogger.Core.Abstracts
{
    public interface IController<T> where T : class, new()
    {
         T Get(string id);
        IEnumerable<T> Get();
        bool Add(T entity);
        bool Update(T entity);
        bool Delete(string id);
    }
}
