using Microsoft.EntityFrameworkCore;
using Services.IDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Services.Services
{
    public abstract class BaseService<T> where T : class 
    {
        private readonly AppDbContext _context;
        public BaseService(AppDbContext context)
        {
            _context = context;
        }
        public bool InsertOrUpdate(T entity, string key)
        {
            try
            {
                var obj = _context.Set<T>().Find(key);
                if (obj != null)
                {
                    _context.Set<T>().Update(entity);
                }
                else
                {
                    _context.Add(entity);
                }
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //Logger.WriteLog(Logger.LogType.Error, ex.Message);

                return false;
            }
        }
        public bool Add(T entity)
        {
            try
            {
                _context.Add(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //Logger.WriteLog(Logger.LogType.Error, ex.Message);

                return false;
            }
        }
        public List<T> FindAll(params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                IQueryable<T> items = _context.Set<T>();
                if (includeProperties != null)
                {
                    foreach (var includeProperty in includeProperties)
                    {
                        items = items.Include(includeProperty);
                    }
                }
                return items.ToList();
            }
            catch (Exception ex)
            {

            }
            return new List<T>();
        }
    }
}
