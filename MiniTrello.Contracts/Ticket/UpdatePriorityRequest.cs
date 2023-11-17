using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniTrello.Contracts.Ticket;

public class UpdatePriorityRequest
{
    public Guid TicketId { get; set; }
    
    public string Priority { get; set; }
}
