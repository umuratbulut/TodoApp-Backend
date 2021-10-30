using Core.DataAccess;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.DAL.Concrete.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IRepository<TEntity>
         where TEntity : class, IEntity, new()
         where TContext : DbContext, new()
    {
        public void Add(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var entities = context.Entry(entity);
                entities.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                return filter == null ?
                       context.Set<TEntity>().ToList() :
                       context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public TEntity Find(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                // Burada set<> bize parametre oalrak gelen nesneyi temsil eder.
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public void Remove(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var entities = context.Entry(entity);
                entities.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var entities = context.Entry(entity);
                entities.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

       

        
    }
}
