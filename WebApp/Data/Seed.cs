using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Models;

namespace WebApp.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<ApplicationDataContext>();

            context?.Database.EnsureCreated();

            if (context != null && context.Clubs.Any() == false)
            {
                context.Clubs.AddRange(new List<Club>
                {
                    new()
                    {
                        Title = "Running Club 1",
                        Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                        Description = "This is the description of the first cinema",
                        ClubCategory = ClubCategory.City,
                        Address = new Address
                        {
                            Street = "123 Main St",
                            City = "Charlotte",
                            State = "NC"
                        }
                    },
                    new()
                    {
                        Title = "Running Club 2",
                        Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                        Description = "This is the description of the first cinema",
                        ClubCategory = ClubCategory.Endurance,
                        Address = new Address
                        {
                            Street = "123 Main St",
                            City = "Charlotte",
                            State = "NC"
                        }
                    },
                    new()
                    {
                        Title = "Running Club 3",
                        Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                        Description = "This is the description of the first club",
                        ClubCategory = ClubCategory.Trail,
                        Address = new Address
                        {
                            Street = "123 Main St",
                            City = "Charlotte",
                            State = "NC"
                        }
                    },
                    new()
                    {
                        Title = "Running Club 3",
                        Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                        Description = "This is the description of the first club",
                        ClubCategory = ClubCategory.City,
                        Address = new Address
                        {
                            Street = "123 Main St",
                            City = "Michigan",
                            State = "NC"
                        }
                    }
                });
                
                context.SaveChanges();
            }
            
            if (context != null && context.Races.Any() == false)
            {
                context.Races.AddRange(new List<Race>
                {
                    new()
                    {
                        Title = "Running Race 1",
                        Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                        Description = "This is the description of the first race",
                        RaceCategory = RaceCategory.Marathon,
                        Address = new Address
                        {
                            Street = "123 Main St",
                            City = "Charlotte",
                            State = "NC"
                        }
                    },
                    new()
                    {
                        Title = "Running Race 2",
                        Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                        Description = "This is the description of the first race",
                        RaceCategory = RaceCategory.Ultra,
                        AddressId = 5,
                        Address = new Address
                        {
                            Street = "123 Main St",
                            City = "Charlotte",
                            State = "NC"
                        }
                    }
                });
                
                context.SaveChanges();
            }
        }

        // public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        // {
        //     using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        //     {
        //         //Roles
        //         var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        //
        //         if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
        //             await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
        //         if (!await roleManager.RoleExistsAsync(UserRoles.User))
        //             await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
        //
        //         //Users
        //         var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
        //         string adminUserEmail = "teddysmithdeveloper@gmail.com";
        //
        //         var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
        //         if (adminUser == null)
        //         {
        //             var newAdminUser = new AppUser()
        //             {
        //                 UserName = "teddysmithdev",
        //                 Email = adminUserEmail,
        //                 EmailConfirmed = true,
        //                 Address = new Address()
        //                 {
        //                     Street = "123 Main St",
        //                     City = "Charlotte",
        //                     State = "NC"
        //                 }
        //             };
        //             await userManager.CreateAsync(newAdminUser, "Coding@1234?");
        //             await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
        //         }
        //
        //         string appUserEmail = "user@etickets.com";
        //
        //         var appUser = await userManager.FindByEmailAsync(appUserEmail);
        //         if (appUser == null)
        //         {
        //             var newAppUser = new AppUser()
        //             {
        //                 UserName = "app-user",
        //                 Email = appUserEmail,
        //                 EmailConfirmed = true,
        //                 Address = new Address()
        //                 {
        //                     Street = "123 Main St",
        //                     City = "Charlotte",
        //                     State = "NC"
        //                 }
        //             };
        //             await userManager.CreateAsync(newAppUser, "Coding@1234?");
        //             await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
        //         }
        //     }
        // }
    }
}