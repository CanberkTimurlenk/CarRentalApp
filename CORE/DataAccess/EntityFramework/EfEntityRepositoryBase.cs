using Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Core.Entities;
using Entities.Abstract;
using Microsoft.EntityFrameworkCore.Design;

namespace Core.DataAccess.EntityFramework
{
    //  car, brand, color için gerekli methodların içeriğini barındıran generic repository

    public class EfEntityRepositoryBase<TEntity,TContext> : IRepositoryBase<TEntity>
        
        where TEntity : class,IEntity,new()
        where TContext : DbContext, new()

        
    {
        private readonly IDesignTimeDbContextFactory<TContext> _contextFactory;
        public EfEntityRepositoryBase(IDesignTimeDbContextFactory<TContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
    
        public void Add(TEntity entity)
        {
            using (var context = _contextFactory.CreateDbContext(new String[0]))
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added; //state i added olarak deklare ettik, saveChanges çağrıldığı vakit eklenece
                context.SaveChanges();


            }
        }

        public void Delete(TEntity entity)
        {
            using (var context = _contextFactory.CreateDbContext(new String[0]))
            {
                var deletedEntity = context.Entry(entity);                
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();


            }

        }
        public void Update(TEntity entity)
        {
            using (var context = _contextFactory.CreateDbContext(new String[0]))
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();


            }
        }



        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = _contextFactory.CreateDbContext(new String[0]))
            {
                return context.Set<TEntity>().SingleOrDefault(filter);

            }

        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            //  heap tarafındaki CarpAppContext using bloğu using() {} tamamlandıktan sonra bellekten yok edilecek
            //  IDisposible

            using (var context = _contextFactory.CreateDbContext(new String[0]))
            {
                return filter == null
                    ? context.Set<TEntity>().ToList() //null ise tümünü getir return et
                    : context.Set<TEntity>().Where(filter).ToList(); //where ile filtreyi dahil et, return et


            }


        }

    }
}
