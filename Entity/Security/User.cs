using System.Collections.Generic;
using Entity.Capture;

namespace Entity.Security
{
    public class User
    {
        public User()
        {
            FoodRegisters = new List<FoodRegister>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        /// <summary>
        /// Represent the gender of an user. The posibles values are male = male and f = female
        /// </summary>
        public string Gender { get; set; }//TODO: Change the property type to char. I was having problems with migration for that reason I didn't continue.
        public bool Active { get; set; }

        public virtual IList<FoodRegister> FoodRegisters { get; set; }
    }
}
