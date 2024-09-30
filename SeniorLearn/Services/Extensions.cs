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
            TypeAdapterConfig<Lesson, LessonDTO>
                .NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.GroupId, src => src.GroupId);

            services.AddSingleton(config);
            return services;
        }

        public static IServiceCollection AddLibraryServices(this IServiceCollection services)
        {
            services.AddScoped<OrganisationUserService>();
            services.AddScoped<OrganisationUserRoleService>();
            services.AddScoped<PaymentService>();
            services.AddScoped<LessonService>();

            return services;
        }

    }
}