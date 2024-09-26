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
                ou.Property(p => p.Version)
                .IsRowVersion();
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

            // Course and Lesson relationships
            mb.Entity<Course>(c =>
            {
                c.HasMany(course => course.Lessons)
                 .WithOne(lesson => lesson.Course)
                 .HasForeignKey(lesson => lesson.CourseId)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            mb.Entity<Lesson>(l =>
            {
                l.HasOne(lesson => lesson.Course)
                 .WithMany(course => course.Lessons)
                 .HasForeignKey(lesson => lesson.CourseId);
            });

            mb.Entity<LessonEnrolment>(le =>{
                le.HasOne(le => le.Lesson)
                .WithMany(l => l.LessonEnrolments)
                .HasForeignKey(le => le.LessonId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            mb.Entity<Payment>()
               .Property(p => p.PaymentAmount)
               .HasPrecision(18, 2);
        }
    }
}
