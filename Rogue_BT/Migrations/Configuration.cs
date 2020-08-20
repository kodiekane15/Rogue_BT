namespace Rogue_BT.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Rogue_BT.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Configuration;

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
                    AvatarPath = "/Images/default_avatar.png",
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
                    AvatarPath = "/Images/default_avatar.png",

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
                    AvatarPath = "/Images/default_avatar.png",

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
                    AvatarPath = "/Images/default_avatar.png",

                }, "Abc123.");

                var userId = userManager.FindByEmail("TeriGarrison@gmail.com").Id;
                //Now that I have created a user, I want to assign them to a specific role.
                userManager.AddToRole(userId, "Submitter");
            }
            #region Demo Creation
           
            var demoPassword = WebConfigurationManager.AppSettings["DemoPassword"];


            if (!context.Users.Any(u => u.Email == "DemoAdmin@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoAdmin@mailinator.com",
                    Email = "DemoAdmin@mailinator.com",
                    FirstName = "Admin",
                    LastName = "Demo",
                    AvatarPath = "/Images/default_avatar.png"
                }, demoPassword);
                var userId = userManager.FindByEmail("DemoAdmin@mailinator.com").Id;
                //Now that I have created a user, I want to assign them to a specific role.
                userManager.AddToRole(userId, "Admin");

            }
            if (!context.Users.Any(u => u.Email == "DemoPM1@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoPM1@mailinator.com",
                    Email = "DemoPM1@mailinator.com",
                    FirstName = "Project Manager 1",
                    LastName = "Demo",
                    AvatarPath = "/Images/default_avatar.png"
                }, demoPassword);
                var userId = userManager.FindByEmail("DemoPM1@mailinator.com").Id;
                //Now that I have created a user, I want to assign them to a specific role.
                userManager.AddToRole(userId, "Project Manager");
            }
            if (!context.Users.Any(u => u.Email == "DemoPM2@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoPM2@mailinator.com",
                    Email = "DemoPM2@mailinator.com",
                    FirstName = "Project Manager 2",
                    LastName = "Demo",
                    AvatarPath = "/Images/default_avatar.png"
                }, demoPassword);
                var userId = userManager.FindByEmail("DemoPM2@mailinator.com").Id;
                //Now that I have created a user, I want to assign them to a specific role.
                userManager.AddToRole(userId, "Project Manager");
            }
            if (!context.Users.Any(u => u.Email == "DemoDeveloper1@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoDeveloper1@mailinator.com",
                    Email = "DemoDeveloper1@mailinator.com",
                    FirstName = "Developer 1",
                    LastName = "Demo",
                    AvatarPath = "/Images/default_avatar.png"
                }, demoPassword);
                var userId = userManager.FindByEmail("DemoDevloper1@mailinator.com").Id;
                //Now that I have created a user, I want to assign them to a specific role.
                userManager.AddToRole(userId, "Developer");
            }
            if (!context.Users.Any(u => u.Email == "DemoDeveloper2@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoDeveloper2@mailinator.com",
                    Email = "DemoDeveloper2@mailinator.com",
                    FirstName = "Developer 2",
                    LastName = "Demo",
                    AvatarPath = "/Images/default_avatar.png"
                }, demoPassword);
                var userId = userManager.FindByEmail("DemoDevloper2@mailinator.com").Id;
                //Now that I have created a user, I want to assign them to a specific role.
                userManager.AddToRole(userId, "Developer");
            }
            if (!context.Users.Any(u => u.Email == "DemoDeveloper3@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoDeveloper3@mailinator.com",
                    Email = "DemoDeveloper3@mailinator.com",
                    FirstName = "Developer 3",
                    LastName = "Demo",
                    AvatarPath = "/Images/default_avatar.png"
                }, demoPassword);
                var userId = userManager.FindByEmail("DemoDevloper3@mailinator.com").Id;
                //Now that I have created a user, I want to assign them to a specific role.
                userManager.AddToRole(userId, "Developer");
            }
            if (!context.Users.Any(u => u.Email == "DemoSubmitter1@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoSubmitter1@mailinator.com",
                    Email = "DemoSubmitter1@mailinator.com",
                    FirstName = "Submitter 1",
                    LastName = "Demo",
                    AvatarPath = "/Images/default_avatar.png"
                }, demoPassword);
                var userId = userManager.FindByEmail("DemoSubmitter1@mailinator.com").Id;
                //Now that I have created a user, I want to assign them to a specific role.
                userManager.AddToRole(userId, "Submitter");
            }
            if (!context.Users.Any(u => u.Email == "DemoSubmitter2@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoSubmitter2@mailinator.com",
                    Email = "DemoSubmitter2@mailinator.com",
                    FirstName = "Submitter 2",
                    LastName = "Demo",
                    AvatarPath = "/Images/default_avatar.png"
                }, demoPassword);
                var userId = userManager.FindByEmail("DemoSubmitter2@mailinator.com").Id;
                //Now that I have created a user, I want to assign them to a specific role.
                userManager.AddToRole(userId, "Submitter");
            }
            if (!context.Users.Any(u => u.Email == "DemoSubmitter3@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoSubmitter3@mailinator.com",
                    Email = "DemoSubmitter3@mailinator.com",
                    FirstName = "Submitter 3",
                    LastName = "Demo",
                    AvatarPath = "/Images/default_avatar.png"
                }, demoPassword);
                var userId = userManager.FindByEmail("DemoSubmitter3@mailinator.com").Id;
                //Now that I have created a user, I want to assign them to a specific role.
                userManager.AddToRole(userId, "Submitter");
            }
            #endregion
            #region project Seed

            #endregion



            context.SaveChanges();
            #region TicketType seed
            context.TicketTypes.AddOrUpdate
                (
                    tt => tt.Name,
                    new TicketType() { Name = "Software" },
                    new TicketType() { Name = "Hardware" },
                    new TicketType() { Name = "UI" },
                    new TicketType() { Name = "Defect" },
                    new TicketType() { Name = "Other" },
                    new TicketType() { Name = "Feature Request" }
                );

            #endregion
            #region TicketPriority seed
            context.TicketPriorities.AddOrUpdate
                (
                    tp => tp.Name,
                    new TicketPriority() { Name = "Low" },
                    new TicketPriority() { Name = "Medium" },
                    new TicketPriority() { Name = "High" },
                    new TicketPriority() { Name = "On Hold" }
                );

            #endregion
            #region TicketStatus seed
            context.TicketStatuses.AddOrUpdate
               (
                   ts => ts.Name,
                   new TicketStatus() { Name = "Open" },
                   new TicketStatus() { Name = "Assigned" },
                   new TicketStatus() { Name = "Resolved" },
                   new TicketStatus() { Name = "Reopened" },
                   new TicketStatus() { Name = "Archived" }
               );

            #endregion
            #region Project Seed
            context.Projects.AddOrUpdate
                (
                    p => p.Name,
                    new Project() { Name = "Seed 1", Created = DateTime.Now.AddDays(-60), IsArchived = true },
                    new Project() { Name = "Seed 2", Created = DateTime.Now.AddDays(-45) },
                    new Project() { Name = "Seed 3", Created = DateTime.Now.AddDays(-30) },
                    new Project() { Name = "Seed 4", Created = DateTime.Now.AddDays(-15) },
                    new Project() { Name = "Seed 5", Created = DateTime.Now.AddDays(-7) }
                );
            #endregion
            context.SaveChanges();
        }
    }
}
