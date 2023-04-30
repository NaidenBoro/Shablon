using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Data.Context;
using Data.Models;

namespace Web.Seed
{
    public static class DatabaseSeedUtilities
    {
        public static void SeedRoles(this WebApplication app)
        {
            using (var serviceScope = app.Services.CreateScope())
            {
                using (var appDbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>())
                {
                    appDbContext.Database.Migrate();

                    if (appDbContext.Roles.ToList().Count == 0)
                    {

                        foreach(var r in Enum.GetNames(typeof(Roles)))
                        {
                            IdentityRole role = new IdentityRole();
                            role.Name = r;
                            role.NormalizedName = role.Name.ToUpper();

                            appDbContext.Add(role);


                            appDbContext.SaveChanges();
                        }
                    }
                }
            }
        }
    }
}
