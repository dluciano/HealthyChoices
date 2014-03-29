using System.Data.Entity.ModelConfiguration;
using Entity.Security;

namespace DataAccess.Mapping
{
    public class UserMapping : EntityTypeConfiguration<User>
    {
        public UserMapping()
        {
            HasKey(t => t.Id);
            Property(t => t.UserName).IsRequired().HasMaxLength(50);
            Property(t => t.FirstName).IsRequired().HasMaxLength(200);
            Property(t => t.LastName).IsOptional().HasMaxLength(200);
            Property(t => t.Gender).IsRequired();
        }
    }
}
