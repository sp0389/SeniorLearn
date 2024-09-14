using Microsoft.EntityFrameworkCore;

namespace SeniorLearn.Data.Core
{
    public class SeedData
    {
        public SeedData(ModelBuilder mb)
        {
            mb.Entity<Organisation>().HasData(new Organisation
            {
                Id = 1,
                Name = "Senior Learn"
            }
            );

            InitTestMembersAndRoles(mb);

        }
        private static void InitTestMembersAndRoles(ModelBuilder mb)
        {
            mb.Entity<OrganisationRole>().HasData(
                new OrganisationRole
                {
                    Id = "09adf476-7af7-4bd7-89e5-d173778b3ec9",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new OrganisationRole
                {
                    Id = "1455a748-82ad-4e31-bb41-7c72cfc0fbfa",
                    Name = "Standard",
                    NormalizedName = "STANDARD",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                },
                new OrganisationRole
                {
                    Id = "de1e5fe5-585b-4867-aae8-57776d64f330",
                    Name = "Professional",
                    NormalizedName = "PROFESSIONAL",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                },
                new OrganisationRole
                {
                    Id = "2199dac7-bac1-49f0-8820-07b34f79533b",
                    Name = "Honorary",
                    NormalizedName = "HONORARY",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                
                }
            );
            mb.Entity<Member>().HasData(
                
                new Member
                {
                    Id = "ca32e0e5-46b8-4f44-9a97-0d685a2c54b2",
                    UserName = "a.admin@seniorlearn.com.au",
                    FirstName = "Adam",
                    LastName = "Admin",
                    Registered = DateTime.UtcNow,
                    OrganisationId = 1,
                    NormalizedUserName = "A.ADMIN@SENIORLEARN.COM.AU",
                    Email = "a.admin@seniorlearn.com.au",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAIAAYagAAAAEHsSevUsbVfCvzTrAPeOAJGAdLJXoClxNuG4OJyPozgYXexeGOqLXgnIxAZgTQTbfA==",
                    SecurityStamp = "M67EBX32EPBJDLSU75U3EA5SFKIR7MDP",
                    ConcurrencyStamp = "3e098325-ba04-4578-8bd8-231bbf8dde66",
                    PhoneNumber = null,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                },

                new Member
                {
                    Id = "c6e5a515-b561-458a-85e6-ab9e7eed58f4",
                    UserName = "m.member@seniorlearn.com.au",
                    FirstName = "Mary",
                    LastName = "Member",
                    Registered = DateTime.UtcNow,
                    OrganisationId = 1,
                    NormalizedUserName = "M.MEMBER@SENIORLEARN.COM.AU",
                    Email = "m.member@seniorlearn.com.au",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAIAAYagAAAAEGuoaNhuyNZDd/SdkB7dMyKO61l9hBzj4h26Bm6gmQpnrpwe+vNFNyBLSPj0JGM13Q==",
                    SecurityStamp = "ISWZYSPA6TIRY35DE4KKKESEPQZKL6VG",
                    ConcurrencyStamp = "36bea754-e167-42af-83ed-bd78392859f3",
                    PhoneNumber = null,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                },

                new Member
                {
                    Id = "7610170e-d0e7-43b9-a289-02d13056d54e",
                    UserName = "j.bloggs@seniorlearn.com.au",
                    NormalizedUserName = "J.BLOGGS@SENIORLEARN.COM.AU",
                    FirstName = "Joe",
                    LastName = "Bloggs",
                    Registered = DateTime.UtcNow,
                    OrganisationId = 1,
                    Email = "j.bloggs@seniorlearn.com.au",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAIAAYagAAAAENaAF8X3fgawsa7CT8EKV1Bm+PGcrq9PhRBL+ee6Rb8lCZVRf/6it+zEesnSHS6q1w==",
                    SecurityStamp = "LZOWMFVS2SAJIT7PFI3CPG4WQDCHQS5R",
                    ConcurrencyStamp = "b2686cbb-099f-4c58-91a4-8fcb9c048d35",
                    PhoneNumber = null,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                }
            );

            mb.Entity<OrganisationUserRole>().HasData(
                new OrganisationUserRole
                {
                    RoleId = "09adf476-7af7-4bd7-89e5-d173778b3ec9",
                    UserId = "ca32e0e5-46b8-4f44-9a97-0d685a2c54b2",
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.MaxValue,
                    RoleType = RoleTypes.Administrator,
                },
                new OrganisationUserRole
                {
                    RoleId = "1455a748-82ad-4e31-bb41-7c72cfc0fbfa",
                    UserId = "c6e5a515-b561-458a-85e6-ab9e7eed58f4",
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddYears(1),
                    RoleType = RoleTypes.Standard,
                },
                new OrganisationUserRole
                {
                    RoleId = "de1e5fe5-585b-4867-aae8-57776d64f330",
                    UserId = "7610170e-d0e7-43b9-a289-02d13056d54e",
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddYears(1),
                    RoleType = RoleTypes.Professional,
                }
            );
        }
    }
}
