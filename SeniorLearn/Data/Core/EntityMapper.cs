using Microsoft.EntityFrameworkCore;

namespace SeniorLearn.Data.Core
{
    public class EntityMapper
    {
        public EntityMapper(ModelBuilder mb)
        {
            mb.Entity<Organisation>(o =>
            {
                o.Property(o => o.Name).IsRequired()
                .HasMaxLength(100);
                o.HasMany(o => o.Users)
                .WithOne(ou => ou.Organisation)
                .HasForeignKey(ou => ou.OrganisationId)
                .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
