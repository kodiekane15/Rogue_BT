using Rogue_BT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rogue_BT.Helper
{
    public class ProjectHelper
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserRoleHelper userRoleHelper = new UserRoleHelper();
        //What do we need to do?
        //Add one or more users to a project
        public void AddUserToProject(string userId, int projectId)
        {
            Project project = db.Projects.Find(projectId);
            var user = db.Users.Find(userId);
            project.Users.Add(user);            
        }
        //Remove one or more users from a project
        public bool RemoveUserFromProject(string userId, int projectId)
        {
            Project project = db.Projects.Find(projectId);
            var user = db.Users.Find(userId);
            var result = project.Users.Remove(user);
            return result;
        }
        //List users on a project
        public List<ApplicationUser> ListUserOnProject(int projectId)
        {
            Project project = db.Projects.Find(projectId);
            var resultList = new List<ApplicationUser>();
            resultList.AddRange(project.Users);
            return resultList;
        }
        //List users not on a project
        public List<ApplicationUser> ListUsersNotOnProject(int projectId)
        {
            var resultList = new List<ApplicationUser>();
            foreach (var user in db.Users.ToList()) 
            {
                if (!IsUserOnProject(user.Id, projectId)) 
                {
                    resultList.Add(user);
                }
            }
            return resultList;
        }
        //Boolean method IsUserOnProject
        public bool IsUserOnProject(string userId, int projectId)
        {
            Project project = db.Projects.Find(projectId);
            var user = db.Users.Find(userId);
            
            return project.Users.Contains(user);
        }
        //OPTIONAL: List users on a project that occupy a specific role
        //Filters the list of users on a project by role - use to populate ticket assignment dropdown
        public List<ApplicationUser> ListUsersOnProjectInRole(int projectId, string roleName) 
        {
            var userList = ListUserOnProject(projectId);
            var resultList = new List<ApplicationUser>();
            foreach (var user in userList) 
            {
                if (userRoleHelper.IsUserInRole(user.Id, roleName))
                {
                    resultList.Add(user);
                }
            }
            return resultList;
        }
        //OPTIONAL: List users not on a project in a specific role
        //OPTIONAL: List projects for a specific user
        public List<Project> ListUserProjects (string userId)
        {
            var user = db.Users.Find(userId);
            var resultList = new List<Project>();
            resultList.AddRange(user.Projects);
            return resultList;
        }
        //OPTIONAL: Filter for Project Manager role - if you code for only one allowed

    }
}