﻿using System.Collections.Generic;

namespace Entity.Capture
{
    public class TakenAt
    {
        public TakenAt()
        {
            FoodRegisters = new List<FoodRegister>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte OrderNumber { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<FoodRegister> FoodRegisters { get; set; }
    }
}
