using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SeniorLearn.Data.Core
{
    public class ApplicationDbContext : IdentityDbContext<OrganisationUser, OrganisationRole, string>
    {
        public DbSet<Organisation> Organisations { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new EntityMapper(modelBuilder);
        }
    }
}
