using Microsoft.EntityFrameworkCore;
using SeniorLearn.Data.Core;

namespace SeniorLearn.Services;

public class EnrolmentService
{
    private readonly ApplicationDbContext _context;
    private readonly OrganisationUserService _organisationUserService;

    public EnrolmentService(ApplicationDbContext context, OrganisationUserService organisationUserService)
    {
        _context = context;
        _organisationUserService = organisationUserService;
    }

    public async Task EnrolMemberAsync(string userId, IList<int> Lessons)
    {
        //TODO: member should never technically ever be null as long as they are logged in, but maybe should consider implementing a null check for an edge case.

        var member = await _organisationUserService.GetUserByUserNameAsync(userId);

        var lessons = await _context.Lessons.Where(l => Lessons.Contains(l.Id)).ToListAsync();

        foreach (var lesson in lessons)
        {
            lesson.EnrolmentValidationChecks(member!, lesson);

            var enrolment = lesson.EnrolMemberInLesson(member!, lesson, DateTime.UtcNow);
            await _context.AddAsync(enrolment);
        }
        await _context.SaveChangesAsync();
    }

    // TODO: Fetch enrolments for member & convert to DTO
}