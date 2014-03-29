using Entity.Capture;

namespace DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DataAccess.HealthyChoicesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DataAccess.HealthyChoicesContext context)
        {
            context.FoodTypes.AddOrUpdate(c => c.Name,
                new[]
                {
                    new FoodType{Active = true, Description = "Fast Food", Name = "FastFood"},
                    new FoodType{Active = true, Description = "Vegetarian", Name = "Vegetarian"},
                });

            context.TakenAts.AddOrUpdate(c => c.Name,
                new[]
                {
                    new TakenAt{Active = true, Description = "Breakfast", Name = "Breakfast", OrderNumber = 1},
                    new TakenAt{Active = true, Description = "Launch", Name = "Launch", OrderNumber = 2},
                    new TakenAt{Active = true, Description = "Dinner", Name = "Dinner", OrderNumber = 3}
                });
        }
    }
}
