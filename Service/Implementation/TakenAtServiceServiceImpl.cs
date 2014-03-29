using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Capture;

namespace Service.Implementation
{
    public class TakenAtServiceServiceImpl : AbstractService, ITakenAtService
    {
        public IEnumerable<TakenAt> GetAll()
        {
            return context.TakenAts.AsNoTracking().ToList();
        }

        public int Insert(TakenAt entity)
        {
            try
            {
                entity.Active = true;
                context.TakenAts.Add(entity);
                return context.SaveChanges();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int Update(TakenAt entity)
        {
            var states = new[] { EntityState.Added, EntityState.Deleted };
            if (states.Any(s => s == context.Entry(entity).State))
                return 0;
            if (context.Entry(entity).State == EntityState.Modified)
                return context.SaveChanges();
            if (context.Entry(entity).State == EntityState.Detached)
                context.TakenAts.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            return context.SaveChanges();
        }

        public int Remove(TakenAt entity)
        {
            var states = new[] { EntityState.Added, EntityState.Modified };
            if (states.Any(s => s == context.Entry(entity).State))
                return 0;
            if (context.Entry(entity).State == EntityState.Deleted)
                context.SaveChanges();
            if (context.Entry(entity).State == EntityState.Detached)
                context.TakenAts.Attach(entity);
            context.TakenAts.Remove(entity);
            return context.SaveChanges();
        }

        public int Remove(int id)
        {
            context.TakenAts.Remove(context.TakenAts.FirstOrDefault(f => f.Id == id));
            return context.SaveChanges();
        }
    }
}
