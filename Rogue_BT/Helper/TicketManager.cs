using Microsoft.AspNet.Identity;
using Rogue_BT.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;

namespace Rogue_BT.Helper
{
    public class TicketManager
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private UserRoleHelper userRoleHelper = new UserRoleHelper();
        public List<Ticket> GetMyTickets()
        {
            var tickets = new List<Ticket>();
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var myRole = userRoleHelper.ListUserRoles(userId).FirstOrDefault();

            switch (myRole)
            {
                case "Admin":
                    tickets.AddRange(db.Tickets);
                    break;
                case "Project Manager":
                    tickets.AddRange(db.Users.Find(userId).Projects.SelectMany(p => p.Tickets));
                    break;
                case "Developer":
                    tickets.AddRange(db.Tickets.Where(t => t.DeveloperId == userId));
                    break;
                case "Submitter":
                    tickets.AddRange(db.Tickets.Where(t => t.SubmitterId == userId));
                    break;

            };
            return tickets;
        }

        public void ManageTicketNotifications(Ticket oldTicket, Ticket newTicket)
        {   //Scenario 1: A new assignment - oldTicket.DeveloperId = null newticket.DeveloperId is not
            if (oldTicket.DeveloperId != newTicket.DeveloperId && newTicket.DeveloperId != null)
            {
                //I have determined that this particular change requires a notification
                //I need to create a new TicketNotification record

                var newNotification = new TicketNotification()
                {
                    TicketId = newTicket.Id,
                    UserId = newTicket.DeveloperId,
                    Created = DateTime.Now,
                    Subject = $"You have been assigned Ticket id: {newTicket.Id}",
                    Message = $"Heads up {newTicket.Developer.FullName}, you have been assigned to Ticket Id {newTicket.Id} titled '{newTicket.Title}' on Project '{newTicket.Project.Name}'."
                };
                
                db.TicketNotifications.Add(newNotification);
                db.SaveChanges();
            }
            //Scenario 2: An unsassignment - oldTicekt.DeveloperId = was not null and newTicket.DeveloeprId is null
           

            //Scenario 3: Reassignment neither old nor new ticket.developer is null, and they don't match
            
        }


    
    }
}