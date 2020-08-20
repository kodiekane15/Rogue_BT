using Microsoft.AspNet.Identity;
using Rogue_BT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rogue_BT.Helper
{
    public class TicketHelper
    {
        private UserRoleHelper roleHelper = new UserRoleHelper();
        private ApplicationDbContext db = new ApplicationDbContext();
        private HistoryHelper historyHelper = new HistoryHelper();
        public bool CanEditTicket(int ticketId)
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var myRole = roleHelper.ListUserRoles(userId).FirstOrDefault();
            switch (myRole)
            {
                case "Admin":
                    return true;

                case "Project Manager":
                    var user = db.Users.Find(userId);
                    return (user.Projects.SelectMany(p => p.Tickets).Any(t => t.Id == ticketId));

                case "Developer":
                case "Submitter":
                    var ticket = db.Tickets.Find(ticketId);
                    if (ticket.DeveloperId == userId || ticket.SubmitterId == userId)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                default:
                    return false;
            }

        }
        public bool CanMakeComment(int ticketId)
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var myRole = roleHelper.ListUserRoles(userId).FirstOrDefault();
            switch (myRole)
            {
                case "Admin":
                    return true;

                case "Project Manager":
                    var user = db.Users.Find(userId);
                    return (user.Projects.SelectMany(p => p.Tickets).Any(t => t.Id == ticketId));

                case "Developer":
                case "Submitter":
                    var ticket = db.Tickets.Find(ticketId);
                    if (ticket.DeveloperId == userId || ticket.SubmitterId == userId)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                default:
                    return false;
            }
        }

        public void EditedTicket(Ticket oldTicket, Ticket newTicket)
        {
            historyHelper.ManageHistories(oldTicket, newTicket);
          
        }
    }
}