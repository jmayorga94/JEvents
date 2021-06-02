using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JEvents.API.Models
{
    public class Event
    {
        public Event()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
    }
}
