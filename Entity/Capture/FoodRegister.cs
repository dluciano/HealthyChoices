using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Security;

namespace Entity.Capture
{
    public class FoodRegister
    {
        public FoodRegister()
        {
            //TakenAt = new TakenAt();
            //FoodType = new FoodType();
            //User = new User();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public int FoodTypeOther { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Active { get; set; }


        public int TakenAtId { get; set; }
        public int FoodTypeId { get; set; }
        public int UserId { get; set; }

        public virtual TakenAt TakenAt { get; set; }
        public virtual FoodType FoodType { get; set; }
        public virtual User User { get; set; }
    }
}
