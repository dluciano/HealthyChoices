using System.Data.Entity.ModelConfiguration;
using Entity.Capture;

namespace DataAccess.Mapping
{
    public class FoodTypeMapping : EntityTypeConfiguration<FoodType>
    {
        public FoodTypeMapping()
        {
            HasKey(t => t.Id);
            Property(t => t.Name).IsRequired().HasMaxLength(50);
            Property(t => t.Description).IsRequired().HasMaxLength(500);
            Property(t => t.Active).IsRequired();
        }
    }
}
