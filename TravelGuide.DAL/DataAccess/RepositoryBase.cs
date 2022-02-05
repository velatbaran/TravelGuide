using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelGuide.DAL.DataAccess
{
    public abstract class RepositoryBase
    {
        protected static DataContext db;
        private static object _lock = new object();

        protected RepositoryBase()
        {
            db = CreateContext();
        }

        private static DataContext CreateContext()
        {
            if (db == null)
            {
                lock (_lock)
                {
                    if (db == null)
                    {
                        db = new DataContext();
                    }
                }
            }
            return db;
        }
    }
}
