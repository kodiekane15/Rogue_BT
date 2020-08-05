namespace Rogue_BT.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Rogue_BT.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Rogue_BT.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Rogue_BT.Models.ApplicationDbContext context)
        {
        
            var roleManager = new RoleManager<IdentityRole>(
                    new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            if (!context.Roles.Any(r => r.Name == "Project Manager"))
            {
                roleManager.Create(new IdentityRole { Name = "Project Manager" });
            }

            if (!context.Roles.Any(r => r.Name == "Developer"))
            {
                roleManager.Create(new IdentityRole { Name = "Developer" });
            }

            if (!context.Roles.Any(r => r.Name == "Submitter"))
            {
                roleManager.Create(new IdentityRole { Name = "Submitter" });
            }

            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));

            if (!context.Users.Any(u => u.Email == "Kodiekanedev@gmail.com"))
            {

                userManager.Create(new ApplicationUser()
                {
                    Email = "Kodiekanedev@gmail.com",
                    UserName = "kodiekanedev@gmail.com",
                    FirstName = "Kodie",
                    LastName = "Kane",
                    AvatarPath = "/Avatars/default_user.png",
                }, "Abc123.");

                var userId = userManager.FindByEmail("Kodiekanedev@gmail.com").Id;
                //Now that I have created a user, I want to assign them to a specific role.
                userManager.AddToRole(userId, "Admin");
            }

            if (!context.Users.Any(u => u.Email == "LukasCline@gmail.com"))
            {

                userManager.Create(new ApplicationUser()
                {
                    Email = "LukasCline@gmail.com",
                    UserName = "LukasCline@gmail.com",
                    FirstName = "Lukas",
                    LastName = "Cline",
                    AvatarPath = "/Avatars/default_user.png",

                }, "Abc123.");

                var userId = userManager.FindByEmail("LukasCline@gmail.com").Id;
                //Now that I have created a user, I want to assign them to a specific role.
                userManager.AddToRole(userId, "Project Manager");
            }

            if (!context.Users.Any(u => u.Email == "AyanaSwift@gmail.com"))
            {

                userManager.Create(new ApplicationUser()
                {
                    Email = "AyanaSwift@gmail.com",
                    UserName = "AyanaSwift@gmail.com",
                    FirstName = "Ayana",
                    LastName = "Swift",
                    AvatarPath = "/Avatars/default_user.png",

                }, "Abc123.");

                var userId = userManager.FindByEmail("AyanaSwift@gmail.com").Id;
                //Now that I have created a user, I want to assign them to a specific role.
                userManager.AddToRole(userId, "Developer");
            }

            if (!context.Users.Any(u => u.Email == "TeriGarrison@gmail.com"))
            {

                userManager.Create(new ApplicationUser()
                {
                    Email = "TeriGarrison@gmail.com",
                    UserName = "TeriGarrison@gmail.com",
                    FirstName = "Teri",
                    LastName = "Garrison",
                    AvatarPath = "/Avatars/default_user.png",

                }, "Abc123.");

                var userId = userManager.FindByEmail("TeriGarrison@gmail.com").Id;
                //Now that I have created a user, I want to assign them to a specific role.
                userManager.AddToRole(userId, "Submitter");
            }
        }
    }
}
