using Microsoft.Extensions.DependencyInjection;
using SeniorLearn.Data;
using SeniorLearn.Data.Core;
using Microsoft.EntityFrameworkCore;
using SeniorLearn.Services;

namespace SeniorLearn.Test
{
    public class Startup
    {
        private string _connectionString =
            "Data Source=localhost;Database=SeniorLearnTestDb;Integrated Security=true;TrustServerCertificate=True";
            //"Server=localhost;Database=SeniorLearnTestDb;User Id=SA;Password=<YourStrong@Passw0rd>;TrustServerCertificate=True";

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(_connectionString));
            services.AddIdentity<OrganisationUser, OrganisationRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 1;
            })
               .AddRoles<OrganisationRole>()
               .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddMapster();
            services.AddOrganisationServices();
        }
    }
}
