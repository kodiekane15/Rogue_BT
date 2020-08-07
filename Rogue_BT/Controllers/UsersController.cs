using Rogue_BT.Helper;
using Rogue_BT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rogue_BT.Controllers
{
    public class UsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserRoleHelper roleHelper = new UserRoleHelper();
        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users);
        }

        public ActionResult ManageUserRole(string id)
        {
            //Does this user already occupy a role? If so i need to display that role in the dropdown
            var userRole = roleHelper.ListUserRoles(id).FirstOrDefault();
            ViewBag.RoleName = new SelectList(db.Roles.Where(r => r.Name != "Admin"), "Name", "Name", userRole);
            return View(db.Users.Find(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageUserRole(string id, string roleName)
        {
            //Code in here looks very similar to the code we've already seen for managing the other roles
            //I need to remove all the roles from this user and then add back the chosen role.
            //Spin through all the roles for this user and remove them
            foreach (var role in roleHelper.ListUserRoles(id))
            {
                roleHelper.RemoveUserFromRole(id, role);

            }
            // If a role was chosen, then add this user to that role
            if (!string.IsNullOrEmpty(roleName))
            {
                roleHelper.AddUserToRole(id, roleName);
            }

            //Now that the user has been assigned a role, I want to redirect them back to the page they came from
            return RedirectToAction("ManageUserRole", new { id });
        }



    }

}