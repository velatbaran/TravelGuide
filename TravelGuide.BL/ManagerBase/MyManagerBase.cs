using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TravelGuide.DAL.DataAccess;
using TravelGuide.DAL.IDataAccess;

namespace TravelGuide.BL.ManagerBase
{
    public abstract class MyManagerBase<T> : IRepository<T> where T : class
    {
        private Repository<T> repo = new Repository<T>();
        public int Delete(T obj)
        {
           return repo.Delete(obj);
        }

        public T Find(Expression<Func<T, bool>> where)
        {
            return repo.Find(where);
        }

        public int Insert(T obj)
        {
            return repo.Insert(obj);
        }

        public List<T> List()
        {
            return repo.List();
        }

        public List<T> List(Expression<Func<T, bool>> where)
        {
            return repo.List(where);
        }

        public IQueryable<T> ListIQueryable()
        {
            return repo.ListIQueryable();
        }

        public int Save()
        {
            return repo.Save();
        }

        public int Update(T obj)
        {
            return repo.Update(obj);
        }
    }
}
