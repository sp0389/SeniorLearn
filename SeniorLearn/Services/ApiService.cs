using Mapster;
using Microsoft.EntityFrameworkCore;
using SeniorLearn.Data.Core;
using SeniorLearn.Models;

namespace SeniorLearn.Services
{
    public class ApiService
    {
        private readonly ApplicationDbContext _context;
        private readonly OrganisationUserService _organisationUserService;

        public ApiService(ApplicationDbContext context, OrganisationUserService organisationUserService)
        {
            _context = context;
            _organisationUserService = organisationUserService;
        }

        public async Task<IEnumerable<CalendarDTO>> GetDatesForApiAsync(string userId)
        {
            var member = await _organisationUserService.GetUserByUserNameAsync(userId);

            var dates = await _context.Lessons.Include(l => l.Enrolments)
                .Where(l => l.Enrolments.Any(e => e.LessonId == l.Id && e.MemberId == member.Id))
                .ProjectToType<CalendarDTO>()
                .ToListAsync();
            return dates;
        }
    }
}