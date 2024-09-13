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

            mb.Entity<OrganisationUser>(ou =>
            {
                ou.HasMany(ur => ur.UserRoles)
                .WithOne(ur => ur.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
                ou.HasDiscriminator<string>("Discriminator")
                .HasValue<OrganisationUser>("OrganisationUser")
                .HasValue<Member>("Member");
            });

            mb.Entity<OrganisationUser>(ou =>
            {
                ou.HasMany(p => p.Payments)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            mb.Entity<OrganisationRole>(or =>
            {
                or.HasMany(r => r.UserRoles)
                   .WithOne(ur => ur.Role)
                   .HasForeignKey(ur => ur.RoleId)
                   .IsRequired();
            });
        }
    }
}
