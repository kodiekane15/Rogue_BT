﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace Rogue_BT.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        #region Parent/Children
        public int ProjectId { get; set; }

        public int TicketPriorityId { get; set; }

        public int TicketStatusId { get; set; }

        public int TicketTypeId { get; set; }

        public string SubmitterId { get; set; }

        public string DeveloperId { get; set; }

        public virtual Project Project { get; set; }
        public virtual TicketType TicketType { get; set; }
        public virtual TicketStatus TicketStatus { get; set; }
        public virtual TicketPriority TicketPriority { get; set; }
        
        

        public ICollection<TicketAttachment> Attachments { get; set; }

        public ICollection<TicketComment> Comments { get; set; }

        public ICollection<TicketHistory> Histories { get; set; }

        public ICollection<TicketNotification> Notifications { get; set; }

        #endregion
        #region Actual Properties
        public string Issue { get; set; }

        public string IssueDescription { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public bool IsResolved { get; set; }

        public bool IsArchived { get; set; }
        #endregion
        #region Constructor
        public Ticket()
        {
            Attachments = new HashSet<TicketAttachment>();
            Comments = new HashSet<TicketComment>();
            Histories = new HashSet<TicketHistory>();
            Notifications = new HashSet<TicketNotification>();
        }
        #endregion
    }
}