using Mapster;
using Microsoft.EntityFrameworkCore;
using SeniorLearn.Data;
using SeniorLearn.Data.Core;
using SeniorLearn.Models;

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
            lesson.EnrolmentValidationChecks(member, lesson);

            var enrolment = lesson.EnrolMemberInLesson(member, lesson, DateTime.UtcNow);
            await _context.AddAsync(enrolment);
        }
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<EnrolmentDTO>> GetLessonOverviewForEnrolmentAsync()
    {
        var lessons = await _context.Lessons.Where(l => l.Availability == Availability.Scheduled)
            .ProjectToType<EnrolmentDTO>()
            .ToListAsync();
        return lessons.GroupBy(l => l.GroupId).Select(l => l.First()).ToList();
    }

    public async Task<IEnumerable<EnrolmentDTO>> GetLessonDetailsForEnrolmentAsync(Guid id, string userId)
    {
        var member = await _organisationUserService.GetUserByUserNameAsync(userId);

        var lessons = await _context.Lessons
            .Include(l => l.Enrolments)
            .Where(l => l.GroupId == id
                        && l.Availability == Availability.Scheduled
                        && l.StartDate <= l.EndDate
                        && !l.Enrolments.Any(e => e.MemberId == member.Id))
            .ProjectToType<EnrolmentDTO>()
            .ToListAsync();

        if (lessons.Count == 0)
        {
            throw new DomainRuleException("You have already enroled in all lessons available.");
        }
        return lessons;
    }
}