using Rogue_BT.Helper;
using Rogue_BT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rogue_BT.Controllers
{
    public class AssignmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserRoleHelper roleHelper = new UserRoleHelper();
        // GET: Assignments
        [Authorize(Roles = "Admin")]
        
        public ActionResult ManageRoles()
        {
            //Use my ViewBag to hold a multi select list of Users in the sysyem
            //the data  Primary key Unique Identifier
            //new MultiSelectList(db.Users.ToList(), "Id", "Email")
            ViewBag.UserIds = new MultiSelectList(db.Users, "Id", "Email");

            //Use my ViewBag to hold a select list of Roles
            ViewBag.RoleName = new SelectList(db.Roles.Where(r => r.Name != "Admin"), "Name", "Name");

            return View(db.Users.ToList());
        }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult ManageRoles(List<string> userIds, string roleName)
            {
            //Step1: If anyone was selected, remove them from all of their rolls
            if (userIds == null)
                return RedirectToAction("RolesIndex");
            //If people were selected, spin through them and strip them of their rolls
            foreach (var userId in userIds) 
            {
                //determine if this user occupies a role

                foreach(var role in roleHelper.ListUserRoles(userId).ToList())
                {
                    roleHelper.RemoveUserFromRole(userId, role);
                }

                //If a role was selected add the user back to it
                //Step2: If a roll was chosen, Add each person to that roll.
                if (!string.IsNullOrEmpty(roleName))
                {
                    roleHelper.AddUserToRole(userId, roleName);
                }
            }
            return RedirectToAction("ManageRoles");
            }


        public ActionResult ManageUserRoles()
        {
            return View();
        }
    }
}