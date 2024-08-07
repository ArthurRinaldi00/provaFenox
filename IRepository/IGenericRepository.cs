using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace provaFenox.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAllList();
        Task<List<T>> GetList(T entity);
        Task<bool> Save(T entity);
        Task<bool> Edit(T entity);
        Task<bool> Delete(int id);
    }
}