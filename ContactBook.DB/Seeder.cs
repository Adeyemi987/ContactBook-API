using ContactBook.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.DB
{
    public class Seeder
    {
        public async static Task Seed(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, ContactBookContext context )
        {
            await context.Database.EnsureCreatedAsync();


            if (!context.Users.Any())
            {
                List<string> roles = new List<string> { "Admin" };
                 
                foreach (var role in roles)
                {
                  await  roleManager.CreateAsync(new IdentityRole { Name = role });
                }


                List<User> users = new List<User>
                {
                    new User
                    {
                        FirstName = "charles",
                        LastName = "Ade",
                        Email = "cc@gmail.com",
                        UserName = "cPro",
                        PhoneNumber = "09034962686"
                    },

                    new User
                    {
                        FirstName = "John",
                        LastName = "kenny", 
                        Email = "jk@yahoo.com",
                        UserName = "JKen",
                        PhoneNumber = "09052782088"
                    },
                };


                foreach (var user in users)
                {
                   await userManager.CreateAsync(user, "chemistryB3@");
                    if (user == users[0])
                    {
                        await userManager.AddToRoleAsync(user, "Admin");
                    }
                    else 
                    {
                        await userManager.AddToRoleAsync(user, "Regular");
                    }
                   
                }

            }
        }
    }
}
