﻿using Microsoft.AspNet.Identity;
using Rogue_BT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rogue_BT.Helper
{
    public class UserHelper
    {
       
            private ApplicationDbContext db = new ApplicationDbContext();
            public string GetFullName(string userId)
            {
                var user = db.Users.Find(userId);
                var firstName = user.FirstName;
                var lastName = user.LastName;
                return firstName + " " + lastName;
            }
            public string LastNameFirst(string userId)
            {
                var user = db.Users.Find(userId);
                return user.FullName;
            }

        public List<Project> ListUserProjects(string userId)
        {
            var user = db.Users.Find(userId);
            return db.Projects.Where(p => p.Users.Contains(user)).ToList();
        }

        internal object ListUserProjects(ApplicationUser userId)
        {
            throw new NotImplementedException();
        }

        public string GetUserRole()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var user = db.Users.Find(userId);
            var roleId = user.Roles.Where(u => u.UserId == userId);
            return null;
        }
        //public string GetAvatarPath()
        //{
        //    var userId = HttpContext.Current.User.Identity.GetUserId();
        //    var user = db.Users.Find(userId);
        //    return user.AvatarPath;
        //}

        public string GetUserRole(string userId)
        {
            return null;
        }
        

    }
}