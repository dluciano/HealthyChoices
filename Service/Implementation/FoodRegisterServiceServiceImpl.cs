using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Entity.Capture;
using Entity.Security;

namespace Service.Implementation
{
    public class FoodRegisterServiceServiceImpl : AbstractService, IFoodRegisterService
    {
        public IEnumerable<FoodRegister> GetAll()
        {
            return context.FoodRegisters.Where(fr => fr.Active).ToList();
        }

        public FoodRegister GetById(int id)
        {
            return context.FoodRegisters.FirstOrDefault(f => f.Id == id && f.Active);
        }


        public List<FoodRegister> GetByUserAndDate(int userId, DateTime dateTime)
        {
            return context.FoodRegisters.Where(fr => fr.CreationDate.Equals(dateTime) && fr.UserId == userId && fr.Active).ToList();
        }

        public List<FoodRegister> GetByUserAndDate(User user, DateTime dateTime)
        {
            var aux = context.FoodRegisters.Where(fr =>
                (dateTime.Day == fr.CreationDate.Day && dateTime.Month == fr.CreationDate.Month &&
                    dateTime.Year == fr.CreationDate.Year) && fr.UserId == user.Id && fr.Active).ToList();
            return aux;
        }

        public int Insert(FoodRegister entity)
        {
            //context.Entry(entity.User).State = EntityState.Unchanged;
            //context.UserProfiles.Attach(entity.User);

            //context.Entry(entity.TakenAt).State = EntityState.Unchanged;
            //context.TakenAts.Attach(entity.TakenAt);

            //context.Entry(entity.FoodType).State = EntityState.Unchanged;
            //context.FoodTypes.Attach(entity.FoodType);

            //context.Entry(entity.User).State = EntityState.Unchanged;
            //context.UserProfiles.Attach(entity.User);

            entity.CreationDate = DateTime.Now;
            entity.Active = true;
            context.FoodRegisters.Add(entity);
            return context.SaveChanges();
        }

        public int Update(FoodRegister entity)
        {
            var states = new[] { EntityState.Added, EntityState.Deleted };
            if (states.Any(s => s == context.Entry(entity).State))
                return 0;

            var aux = context.FoodRegisters.FirstOrDefault(m => m.Id == entity.Id && m.Active);
            if (aux == null || IsNotToday(aux.CreationDate)) return -1;
            aux.Description = entity.Description;
            aux.FoodTypeId = entity.FoodTypeId;

            //if (context.Entry(entity).State == EntityState.Modified)
            //    return context.SaveChanges();
            //if (context.Entry(entity).State == EntityState.Detached)
            //    context.FoodRegisters.Attach(entity);
            //context.Entry(entity).State = EntityState.Modified;
            return context.SaveChanges();
        }

        public int Remove(FoodRegister entity)
        {
            if (IsNotToday(entity.CreationDate))
                return -1;
            var states = new[] { EntityState.Added, EntityState.Modified };
            if (states.Any(s => s == context.Entry(entity).State))
                return 0;
            if (context.Entry(entity).State == EntityState.Deleted)
                context.SaveChanges();
            if (context.Entry(entity).State == EntityState.Detached)
                context.FoodRegisters.Attach(entity);
            context.FoodRegisters.Remove(entity);
            return context.SaveChanges();
        }

        public int Remove(int id)
        {
            var aux = context.FoodRegisters.FirstOrDefault(f => f.Id == id);
            if (aux == null || IsNotToday(aux.CreationDate)) return -1;
            context.FoodRegisters.Remove(aux);
            return context.SaveChanges();
        }

        private static bool IsNotToday(DateTime dateTime)
        {
            var now = new DateTime();
            return dateTime.Day == now.Day &&
                    dateTime.Month == now.Month &&
                    dateTime.Year == now.Year;
        }
    }
}
