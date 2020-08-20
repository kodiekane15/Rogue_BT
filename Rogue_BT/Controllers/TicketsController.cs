using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Rogue_BT.Helper;
using Rogue_BT.Models;
using Rogue_BT.ViewModels;

namespace Rogue_BT.Controllers
{
    [Authorize]
    public class TicketsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ProjectHelper projectHelper = new ProjectHelper();
        private TicketHelper ticketHelper = new TicketHelper();
        private TicketManager ticketManager = new TicketManager();
        private HistoryHelper historyHelper = new HistoryHelper();
        // GET: Tickets
        public ActionResult Index()
        {
            //var myTickets = ticketManager.GetMyTickets(User.Identity.GetUserId());
            var myTicketVM = new MyTicketViewModel();
            myTicketVM.AllTickets = db.Tickets.ToList();
            myTicketVM.MyTickets = ticketManager.GetMyTickets();
            return View(myTicketVM);
            //return View(myTicketVM)
        }
        public ActionResult GetProjectTickets()
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);
            var ticketList = new List<Ticket>();
            ticketList = user.Projects.SelectMany(p => p.Tickets).ToList();
            return View("Index",ticketList);        
        }
        //public ActionResult GetMyTicket()
        //{
        //    var userId = User.Identity.GetUserId();
           
        //    var ticketList = new List<Ticket>();
        //    if (User.IsInRole("Developer"))
        //    {
        //        ticketList = db.Tickets.Where(t => t.DeveloperId == userId).ToList();
        //        return View("Index", ticketList);
        //    }
        //    if (User.IsInRole("Submitter"))
        //    {
        //        ticketList = db.Tickets.Where(t => t.SubmitterId == userId).ToList();
        //        return View("Index", ticketList);
        //    }
        //    else 
        //    {
        //        return RedirectToAction("GetProjectTickets");
        //    }
        //}
        // GET: Tickets/Details/5
        public ActionResult Dashboard(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Tickets/Create
        [Authorize(Roles = "Submitter")]
        public ActionResult Create()
        {
            var userId = User.Identity.GetUserId();
            if (userId == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(projectHelper.ListUserProjects(userId), "Id", "Name");
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name");

            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        

        public ActionResult Create([Bind(Include = "Id,ProjectId,TicketPriorityId,TicketTypeId,Issue,IssueDescription")] Ticket ticket)
        {
            var userId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                //Add back in created and submitter id
                //set DeveloperId to null, is archived and is resolved will be false
                ticket.TicketStatusId = db.TicketStatuses.Where(ts => ts.Name == "Open").FirstOrDefault().Id;
                ticket.Created = DateTime.Now;
                ticket.SubmitterId = userId;
                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Details", "Projects", new { id = ticket.ProjectId});
            }

            ViewBag.ProjectId = new SelectList(projectHelper.ListUserProjects(userId), "Id", "Name");
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityId);

            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeveloperId = new SelectList(db.Users, "Id", "Name", ticket.DeveloperId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Name", ticket.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProjectId,TicketPriorityId,TicketStatusId,TicketTypeId,DeveloperId,Issue,IssueDescription,IsResolved,IsArchived")] Ticket ticket)
        {
            //Memento Object - I need to go out to the db and grab the ticket in its current state in order to compare it to the ticket being submitted from the form
            if (ModelState.IsValid)
            {
            
               
                // var thisTicket = db.Tickets.Find(ticket.Id);
                //thisTicket.ProjectId = ticket.ProjectId;                
                
               
                var oldTicket = db.Tickets.AsNoTracking().FirstOrDefault(t => t.Id == ticket.Id);
              
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
     
                
                var newTicket = db.Tickets.AsNoTracking().FirstOrDefault(t => t.Id == ticket.Id);
                ticketManager.ManageTicketNotifications(oldTicket, newTicket);
                
                historyHelper.ManageHistories(oldTicket, newTicket);
               
                

                return RedirectToAction("Index");
            }
            ViewBag.DeveloperId = new SelectList(db.Users, "Id", "FullName", ticket.DeveloperId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Name", ticket.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);
            return View(ticket);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
