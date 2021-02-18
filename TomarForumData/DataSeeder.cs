using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomarForumData.EntityModels;

namespace TomarForumData
{
    public class DataSeeder
    {
        private ApplicationDbContext _applicationDbContext;

        public DataSeeder(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task SeedSuperUser()
        {
            var roleStore = new RoleStore<IdentityRole>(_applicationDbContext);
            var userStore = new UserStore<ApplicationUser>(_applicationDbContext);

            var user = new ApplicationUser
            {
                UserName = "ForumAdmin",
                NormalizedUserName = "forumadmin",
                Email = "admin@example.com",
                NormalizedEmail = "admin@example.com",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var hasher = new PasswordHasher<ApplicationUser>();
            var hashedPassword = hasher.HashPassword(user,"admin");
            user.PasswordHash = hashedPassword;

            var hasAdminRole = _applicationDbContext.Roles.Any(roles => roles.Name == "Admin");

            if (!hasAdminRole)
            {
                await roleStore.CreateAsync(new IdentityRole 
                { 
                    Name = "Admin", 
                    NormalizedName = "admin" 
                });
            }
            var hasSuperUser = _applicationDbContext.Users.Any(usr => usr.NormalizedUserName == user.UserName);

            if (!hasSuperUser)
            {
                await userStore.CreateAsync(user);
                await userStore.AddToRoleAsync(user, "Admin");
            }

            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
