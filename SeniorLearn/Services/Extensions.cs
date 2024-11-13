using Mapster;
using SeniorLearn.Data;
using SeniorLearn.Models;

namespace SeniorLearn.Services
{
    public static class Extensions
    {
        public static IServiceCollection AddMapster(this IServiceCollection services)
        {
            var config = new TypeAdapterConfig();

            TypeAdapterConfig<Payment, PaymentDTO>
                .NewConfig()
                .Map(dest => dest.PaymentDate, src => src.PaymentDate)
                .Map(dest => dest.PaymentType, src => src.PaymentType)
                .Map(dest => dest.PaymentAmount, src => src.PaymentAmount);
            TypeAdapterConfig<Member, MemberDTO>
                .NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.FirstName, src => src.FirstName)
                .Map(dest => dest.LastName, src => src.LastName)
                .Map(dest => dest.Email, src => src.Email);
            TypeAdapterConfig<Lesson, CalendarDTO>
                .NewConfig()
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.Start, src => src.StartDate)
                .Map(dest => dest.End, src => src.EndDate)
                .Map(dest => dest.Address, src => src.Address)
                .Map(dest => dest.Lecturer, src => $"{src.Member.FirstName} {src.Member.LastName}");
            TypeAdapterConfig<Lesson, EnrolmentDTO>
                .NewConfig()
                .Map(dest => dest.ImageUrl, src => src.ImageUrl);

            services.AddSingleton(config);
            return services;
        }

        public static IServiceCollection AddOrganisationServices(this IServiceCollection services)
        {
            services.AddScoped<OrganisationUserService>();
            services.AddScoped<OrganisationUserRoleService>();
            services.AddScoped<PaymentService>();
            services.AddScoped<LessonService>();
            services.AddScoped<EnrolmentService>();
            services.AddScoped<UtilityService>();
            services.AddScoped<BulletinService>();

            return services;
        }

    }
}