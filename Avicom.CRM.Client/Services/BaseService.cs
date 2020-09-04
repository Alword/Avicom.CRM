using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Avicom.CRM.Client.Services
{
    public abstract class BaseService<T> where T : class
    {
        public abstract T[] All(Expression<Func<T, bool>> expression);
        public abstract Task<T> AddAsync(T user);
        public abstract Task<int> RemoveAsync(Expression<Func<T, bool>> expression);
        public abstract Task UpdateAsync(T entity);
    }
}
