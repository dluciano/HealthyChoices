using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Capture;
using Entity.Security;

namespace Service
{
    public interface IUserService : IService<User>
    {
        User GetById(int id);
    }
}
