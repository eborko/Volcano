using System;
using System.Collections.Generic;
using System.Text;

namespace Volcano.Model
{
    public class Player
    {
        public string Name { get; set; }
        public List<Ticket> Tickets { get; private set; }
        public Account Account { get; set; }

        public Player(string name)
        {
            this.Name = name;
            Tickets = new List<Ticket>();
        }

        public void AddTicket(Ticket ticket)
        {
            if (Tickets == null)
            {
                Tickets = new List<Ticket>();
            }

            Tickets.Add(ticket);
        }
    }
}
