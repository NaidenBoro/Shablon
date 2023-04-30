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
                using (var shablonContext = serviceScope.ServiceProvider.GetRequiredService<ShablonContext>())
                {
                    shablonContext.Database.Migrate();

                    if (shablonContext.Roles.ToList().Count == 0)
                    {

                        foreach(var r in Enum.GetNames(typeof(Roles)))
                        {
                            IdentityRole role = new IdentityRole();
                            role.Name = r;
                            role.NormalizedName = role.Name.ToUpper();

                            shablonContext.Add(role);


                            shablonContext.SaveChanges();
                        }
                    }
                }
            }
        }
    }
}
