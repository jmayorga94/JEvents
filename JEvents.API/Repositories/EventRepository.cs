using JEvents.API.Context;
using JEvents.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JEvents.API.Repositories
{
    public class EventRepository : IEventRepository
    {
        private  readonly JEventsContext _context;

        public EventRepository(JEventsContext context)
        {
            _context = context;
        }
        public void CreateEvent(Event evnt)
        {
            if (evnt == null)
            {
                throw new ArgumentNullException(nameof(evnt));
            }
            _context.Add(evnt);
        }

        public void DeleteEvent(Event evnt)
        {
            if (evnt == null)
            {
                throw new ArgumentNullException(nameof(evnt));
            }

            _context.Events.Remove(evnt);
        }

        public Event GetEventById(Guid id)
        {
            var eventOnDb = _context.Events.FirstOrDefault(x=> x.Id == id);
            return eventOnDb;
        }

        public IEnumerable<Event> GetEvents()
        {
            return _context.Events.ToList();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateEvent(Event evnt)
        {
          
        }
    }
}
