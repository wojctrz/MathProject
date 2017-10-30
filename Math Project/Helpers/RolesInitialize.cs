using Math_Project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MathProject.Helpers
{
    public static class RolesInitialize
    {
        public static async Task CreateRoles(IServiceProvider serviceProvider)

        {

            //adding custom roles

            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roleNames = { "Admin", "Teacher", "Student" };

            IdentityResult roleResult;

            foreach (var roleName in roleNames)

            {

                //creating the roles and seeding them to the database

                var roleExist = await RoleManager.RoleExistsAsync(roleName);

                if (!roleExist)

                {

                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));

                }

            }

            //creating a super user who could maintain the web app

//            var poweruser = new ApplicationUser

//            {

//                UserName = Configuration.GetSection("UserSettings")["UserEmail"],

//                Email = Configuration.GetSection("UserSettings")["UserEmail"]

//            };

//​

//            string UserPassword = Configuration.GetSection("UserSettings")["UserPassword"];

//            var _user = await UserManager.FindByEmailAsync(Configuration.GetSection("UserSettings")["UserEmail"]);

//            if (_user == null)

//            {

//                var createPowerUser = await UserManager.CreateAsync(poweruser, UserPassword);

//                if (createPowerUser.Succeeded)

//                {

//                    //here we tie the new user to the "Admin" role 

//                    await UserManager.AddToRoleAsync(poweruser, "Admin");

//​

//                    }

//            }

        }
    }
}
