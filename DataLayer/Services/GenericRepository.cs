using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;

namespace DataLayer
{
   public class GenericRepository<Entity> where Entity : class
    {
        DbShopContext _db;
        DbSet<Entity> _dbSet;
        public GenericRepository(DbShopContext db)
        {
            this._db = db;
            this._dbSet = db.Set<Entity>();
        }
        public IEnumerable<Entity> Get(Expression<Func<Entity, bool>> where = null)
        {
            IQueryable<Entity> query;
            query = _dbSet;
            if (where != null)
            {
                query = query.Where(where);
            }
            return query;
        }
        public Entity GetByID(Object id)
        {
            return _dbSet.Find(id);
        }
        public bool Insert(Entity entity)
        {
            try
            {
                _dbSet.Add(entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Delete(Entity entity)
        {
            try
            {
                if (_db.Entry(entity).State == EntityState.Detached)
                {
                    _dbSet.Attach(entity);
                }
                _dbSet.Remove(entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(object id)
        {
            Delete(GetByID(id));
            return true;
        }

        public bool Update(Entity entity)
        {
            try
            {
                if (_db.Entry(entity).State == EntityState.Detached) 
                {
                    _dbSet.Attach(entity);
                }
                _db.Entry(entity).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
