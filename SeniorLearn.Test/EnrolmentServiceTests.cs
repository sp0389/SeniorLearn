using Microsoft.EntityFrameworkCore;
using SeniorLearn.Data;
using SeniorLearn.Data.Core;
using SeniorLearn.Services;

namespace SeniorLearn.Test
{
    public class EnrolmentServiceTests
    {
        private readonly ApplicationDbContext _context;
        private readonly EnrolmentService _enrolmentService;
        private readonly OrganisationUserService _organisationUserService;

        public EnrolmentServiceTests(ApplicationDbContext context, EnrolmentService enrolmentService,
            OrganisationUserService organisationUserService)
        {
            _context = context;
            _organisationUserService = organisationUserService;
            _enrolmentService = enrolmentService;
        }

        [Fact]
        public async Task Should_EnrolMemberInLesson()
        {
            var m = await _organisationUserService.GetUserByUserNameAsync("a.admin@seniorlearn.com.au");
            var lessonId = 1;
            var l = await _context.Lessons.Where(l => l.Id == lessonId).FirstOrDefaultAsync();
            var e = l!.EnrolMemberInLesson(m, l, DateTime.UtcNow);

            Assert.NotNull(e);
            Assert.IsType<Enrolment>(e);
        }

        [Fact]
        public async Task ShouldNot_EnrolLessonCreatorInLessons()
        {
            var lessonId = 1;
            var m = await _organisationUserService.GetUserByUserNameAsync("p.professional@seniorlearn.com.au");
            var l = await _context.Lessons.Where(l => l.Id == lessonId).FirstOrDefaultAsync();

            await Assert.ThrowsAsync<DomainRuleException>(async () => l.EnrolMemberInLesson(m, l, DateTime.UtcNow));
        }
    }
}