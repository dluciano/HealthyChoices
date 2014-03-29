using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Capture;

namespace DataAccess.Mapping
{
    public class FoodRegisterMapping : EntityTypeConfiguration<FoodRegister>
    {
        public FoodRegisterMapping()
        {
            HasKey(t => t.Id);

            Property(t => t.Description).IsOptional().HasMaxLength(1000);
            Property(t => t.FoodTypeOther).IsOptional();
            Property(t => t.CreationDate).IsRequired();
            Property(t => t.Active).IsRequired();

            HasRequired(t => t.FoodType)
                .WithMany(t => t.FoodRegisters)
                 .HasForeignKey(t => t.FoodTypeId)
                .WillCascadeOnDelete(false);

            HasRequired(t => t.TakenAt)
                .WithMany(t => t.FoodRegisters)
                 .HasForeignKey(t => t.TakenAtId)
                .WillCascadeOnDelete(false);

            HasRequired(t => t.User)
                .WithMany(t => t.FoodRegisters)
                 .HasForeignKey(t => t.UserId)
                .WillCascadeOnDelete(false);
        }
    }
}
