using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using DataAccess.Mapping;
using DataAccess.Migrations;
using Entity.Capture;
using Entity.Security;

namespace DataAccess
{
    public class HealthyChoicesContext : DbContext
    {
        public HealthyChoicesContext()
            : base("HealthyChoicesConnection")
        {
            //Database.SetInitializer<HealthyChoicesContext>(null);
            //Database.SetInitializer(new DropCreateDatabaseAlways<HealthyChoicesContext>());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<HealthyChoicesContext, Configuration>());
        }

        public DbSet<User> UserProfiles { get; set; }
        public DbSet<TakenAt> TakenAts { get; set; }
        public DbSet<FoodType> FoodTypes { get; set; }
        public DbSet<FoodRegister> FoodRegisters { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserMapping());
            modelBuilder.Configurations.Add(new TakenAtMapping());
            modelBuilder.Configurations.Add(new FoodTypeMapping());
            modelBuilder.Configurations.Add(new FoodRegisterMapping());

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
