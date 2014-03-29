using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Capture;
using Entity.Security;

namespace Service.Implementation
{
    public class UserServiceServiceImpl : AbstractService, IUserService
    {
        public IEnumerable<User> GetAll()
        {
            return context.UserProfiles.AsNoTracking().ToList();
        }

        public User GetById(int id)
        {
            return context.UserProfiles.AsNoTracking().FirstOrDefault(up => up.Id == id);
        }

        public int Insert(User entity)
        {
            try
            {
                entity.Active = true;
                context.UserProfiles.Add(entity);
                return context.SaveChanges();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int Update(User entity)
        {
            var states = new[] { EntityState.Added, EntityState.Deleted };
            if (states.Any(s => s == context.Entry(entity).State))
                return 0;
            if (context.Entry(entity).State == EntityState.Modified)
                return context.SaveChanges();
            if (context.Entry(entity).State == EntityState.Detached)
                context.UserProfiles.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            return context.SaveChanges();
        }

        public int Remove(User entity)
        {
            var states = new[] { EntityState.Added, EntityState.Modified };
            if (states.Any(s => s == context.Entry(entity).State))
                return 0;
            if (context.Entry(entity).State == EntityState.Deleted)
                context.SaveChanges();
            if (context.Entry(entity).State == EntityState.Detached)
                context.UserProfiles.Attach(entity);
            context.UserProfiles.Remove(entity);
            return context.SaveChanges();
        }

        public int Remove(int id)
        {
            context.UserProfiles.Remove(context.UserProfiles.FirstOrDefault(f => f.Id == id));
            return context.SaveChanges();
        }
    }
}
