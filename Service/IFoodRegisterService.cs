using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Capture;
using Entity.Security;

namespace Service
{
    public interface IFoodRegisterService : IService<FoodRegister>
    {
        List<FoodRegister> GetByUserAndDate(int userId, DateTime dateTime);
        List<FoodRegister> GetByUserAndDate(User user, DateTime dateTime);
        FoodRegister GetById(int id);
    }
}
