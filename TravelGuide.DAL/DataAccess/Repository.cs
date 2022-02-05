using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TravelGuide.DAL.Common;
using TravelGuide.DAL.IDataAccess;
using TravelGuide.Entities.EntityBase;

namespace TravelGuide.DAL.DataAccess
{
    public class Repository<T> : RepositoryBase, IRepository<T> where T : class
    {
        private DbSet<T> _dbSet;

        public Repository()
        {
            _dbSet = db.Set<T>();
        }

        public int Delete(T obj)
        {
            if (obj is MyEntityCommonBase)
            {
                MyEntityCommonBase m = obj as MyEntityCommonBase;
                m.IsDeleted = true;
                m.DeletedUsername = App.Common.GetUsername();
                m.DeletedOn = DateTime.Now;
            }
            return Save();
        }

        public T Find(Expression<Func<T, bool>> where)
        {
            return _dbSet.FirstOrDefault(where);
        }

        public int Insert(T obj)
        {
            if (obj is MyEntityCommonBase)
            {
                MyEntityCommonBase m = obj as MyEntityCommonBase;
                m.CreatedOn = DateTime.Now;
                m.CreatedUsername = App.Common.GetUsername();
            }
            _dbSet.Add(obj);
            return Save();
        }

        public List<T> List()
        {
            return _dbSet.ToList();
        }

        public List<T> List(Expression<Func<T, bool>> where)
        {
            return _dbSet.Where(where).ToList();
        }

        public IQueryable<T> ListIQueryable()
        {
            return _dbSet.AsQueryable<T>();
        }

        public int Save()
        {
            return db.SaveChanges();
        }

        public int Update(T obj)
        {
            if (obj is MyEntityCommonBase)
            {
                MyEntityCommonBase m = obj as MyEntityCommonBase;
                m.ModifiedUsername = App.Common.GetUsername();
                m.ModifiedOn = DateTime.Now;
            }
            return Save();
        }
    }
}
