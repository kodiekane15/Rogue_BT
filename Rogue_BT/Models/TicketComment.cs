using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rogue_BT.Models
{
    public class TicketComment
    {
        public int Id { get; set; }
        #region parents and children    
       
        public int TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }
       
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        #endregion
        #region Actual Properties
        public string Comment { get; set; }
        public DateTime Created { get; set; }
        #endregion
    }
}