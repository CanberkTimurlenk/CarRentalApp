using Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.DataAccess.EntityFramework
{
    //  car, brand, color için gerekli methodların içeriğini barındıran generic repository

    public class EfEntityRepositoryBase<TEntity,TContext> : IEntityRepository<TEntity>
        
        where TEntity : class,IEntity,new()
        where TContext : DbContext, new()
        
    {
    
        public void Add(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added; //state i added olarak deklare ettik, saveChanges çağrıldığı vakit eklenece
                context.SaveChanges();


            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();


            }

        }
        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();


            }
        }



        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);

            }

        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            //  heap tarafındaki CarpAppContext using bloğu using() {} tamamlandıktan sonra bellekten yok edilecek
            //  IDisposible

            using (TContext context = new TContext())
            {
                return filter == null
                    ? context.Set<TEntity>().ToList() //null ise tümünü getir return et
                    : context.Set<TEntity>().Where(filter).ToList(); //where ile filtreyi dahil et, return et


            }


        }

    }
}
