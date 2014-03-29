using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Capture;

namespace Service.Implementation
{
    public class FoodTypeServiceServiceImpl : AbstractService, IFoodTypeService
    {
        public IEnumerable<FoodType> GetAll()
        {
            return context.FoodTypes.AsNoTracking().ToList();
        }

        public int Insert(FoodType entity)
        {
            try
            {
                entity.Active = true;
                context.FoodTypes.Add(entity);
                return context.SaveChanges();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int Update(FoodType entity)
        {
            var states = new[] { EntityState.Added, EntityState.Deleted };
            if (states.Any(s => s == context.Entry(entity).State))
                return 0;
            if (context.Entry(entity).State == EntityState.Modified)
                return context.SaveChanges();
            if (context.Entry(entity).State == EntityState.Detached)
                context.FoodTypes.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            return context.SaveChanges();
        }

        public int Remove(FoodType entity)
        {
            var states = new[] { EntityState.Added, EntityState.Modified };
            if (states.Any(s => s == context.Entry(entity).State))
                return 0;
            if (context.Entry(entity).State == EntityState.Deleted)
                context.SaveChanges();
            if (context.Entry(entity).State == EntityState.Detached)
                context.FoodTypes.Attach(entity);
            context.FoodTypes.Remove(entity);
            return context.SaveChanges();
        }

        public int Remove(int id)
        {
            context.FoodTypes.Remove(context.FoodTypes.FirstOrDefault(f => f.Id == id));
            return context.SaveChanges();
        }
    }
}
