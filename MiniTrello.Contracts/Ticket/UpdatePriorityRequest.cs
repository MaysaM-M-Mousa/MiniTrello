﻿namespace MiniTrello.Contracts.Ticket;

public class UpdatePriorityRequest
{
    public Guid TicketId { get; set; }
    
    public string Priority { get; set; }
}
