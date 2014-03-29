using System.Data.Entity.ModelConfiguration;
using Entity.Capture;

namespace DataAccess.Mapping
{
    public class TakenAtMapping : EntityTypeConfiguration<TakenAt>
    {
        public TakenAtMapping()
        {
            HasKey(t => t.Id);
            Property(t => t.Name).IsRequired().HasMaxLength(50);
            Property(t => t.Description).IsRequired().HasMaxLength(500);
            Property(t => t.OrderNumber).IsRequired();
            Property(t => t.Active).IsRequired();
        }
    }
}
