using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SeniorLearn.Data;
using SeniorLearn.Data.Core;
using SeniorLearn.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
namespace SeniorLearn.Services
{
    public class UtilityService
    {
        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _context;
        private readonly OrganisationUserService _organisationUserService;
        private readonly UserManager<OrganisationUser> _userManager;

        public UtilityService(IConfiguration config, ApplicationDbContext context, OrganisationUserService organisationUserService, UserManager<OrganisationUser> userManager)
        {
            _config = config;
            _context = context;
            _organisationUserService = organisationUserService;
            _userManager = userManager;
        }

        public async Task<IEnumerable<CalendarDTO>> GetDatesForApiAsync(string userId)
        {
            var member = await _organisationUserService.GetUserByUserNameAsync(userId);

            return await _context.Lessons.Include(l => l.Enrolments)
                .Where(l => l.Enrolments.Any(e => e.LessonId == l.Id && e.MemberId == member.Id))
                .ProjectToType<CalendarDTO>()
                .ToListAsync();
        }

        public async Task<string> GetJwtTokenAsync(LoginDTO loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email!);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginDto.Password!))
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName!),
                    new Claim(ClaimTypes.Email, user.Email!),
                    new Claim(ClaimTypes.GivenName, user.FirstName),
                    new Claim(JwtRegisteredClaimNames.Sub, _config["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
                };

                var roles = (await _userManager.GetRolesAsync(user))
                    .Select(role => new Claim(ClaimTypes.Role, role));
                claims.AddRange(roles);

                SymmetricSecurityKey key = new SymmetricSecurityKey(Convert.FromBase64String(_config["Jwt:Key"]));

                SigningCredentials sign = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                JwtSecurityToken token = new JwtSecurityToken(
                    _config["Jwt:Issuer"],
                    _config["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddDays(1),
                    signingCredentials: sign);
                
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            return "Invalid Credentials!";
        }

        public async Task<int> GetActiveUserEnrolmentCountForApiAsync()
        {
            return await _context.Users.OfType<Member>()
                .Include(u => u.Enrolments)
                .Where(u => u.Enrolments.Count != 0 && u.Status == Status.Active)
                .CountAsync();
        }

        public async Task<int> GetInactiveUserEnrolmentCountForApiAsync()
        {
            return await _context.Users.OfType<Member>()
                .Include(u => u.Enrolments)
                .Where(u => u.Enrolments.Count != 0 && u.Status == Status.Inactive)
                .CountAsync();
        }
    }
}