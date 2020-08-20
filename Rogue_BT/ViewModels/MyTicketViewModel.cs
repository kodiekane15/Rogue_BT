using Rogue_BT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rogue_BT.ViewModels
{
    public class MyTicketViewModel
    {
        public List<Ticket> AllTickets { get; set; }
        public List<Ticket> MyTickets { get; set; }
    }
}