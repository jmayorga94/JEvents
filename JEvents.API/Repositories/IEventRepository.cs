using JEvents.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JEvents.API.Repositories
{
    public interface IEventRepository
    {
        bool SaveChanges();
        IEnumerable<Event> GetEvents();
        Event GetEventById(Guid id);

        void CreateEvent(Event evnt);
        void UpdateEvent(Event evnt);
        void DeleteEvent(Event evnt);
    }
}
