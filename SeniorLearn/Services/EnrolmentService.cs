using Mapster;
using Microsoft.EntityFrameworkCore;
using SeniorLearn.Data;
using SeniorLearn.Data.Core;
using SeniorLearn.Models;

namespace SeniorLearn.Services
{
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
            var member = await _organisationUserService.GetUserByUserNameAsync(userId);
            var lessons = await _context.Lessons.Where(l => Lessons.Contains(l.Id))
                .ToListAsync();

            foreach (var lesson in lessons)
            {
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

        public async Task<IEnumerable<EnrolmentDTO>> GetMemberLessonEnrolmentsAsync(string userId)
        {
            var member = await _organisationUserService.GetUserByUserNameAsync(userId);

            var enrolments = await _context.Lessons.Include(l => l.Enrolments)
                .Where(l => l.Availability == Availability.Scheduled && l.Enrolments.Any(e => e.MemberId == member.Id))
                .ProjectToType<EnrolmentDTO>()
                .ToListAsync();
                
            return enrolments;
        }

        public async Task UnenrolMemberFromLessonAsync(string userId, IList<int> Lessons, int id)
        {
            var member = await _organisationUserService.GetUserByUserNameAsync(userId);
            
            if (id != 0)
            {
                var lessonEnrolment = await _context.Enrolments.Where(e => e.MemberId == member.Id && e.LessonId == id)
                    .FirstOrDefaultAsync() ?? throw new DomainRuleException("You are not enroled in that lesson.");

                _context.Enrolments.Remove(lessonEnrolment);
                await _context.SaveChangesAsync();
            }
            else
            {
                var lessonEnrolment = await _context.Enrolments
                    .Where(e => e.MemberId == member.Id && Lessons.Contains(e.LessonId))
                    .ToListAsync() ?? throw new DomainRuleException("You are not enroled in that lesson.");

                _context.RemoveRange(lessonEnrolment);
                await _context.SaveChangesAsync();
            }
        }
    }
}