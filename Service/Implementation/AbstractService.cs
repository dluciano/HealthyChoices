using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace Service.Implementation
{
    public abstract class AbstractService
    {
        protected HealthyChoicesContext context = new HealthyChoicesContext();
    }
}
