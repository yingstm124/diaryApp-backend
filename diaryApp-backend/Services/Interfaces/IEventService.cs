using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace diaryApp_backend.Services.Interfaces
{
    public interface IEventService
    {
        public Task<List<Event>> GetEvents(string uid);
        public Task<Event> GetEventDetail(int eventId);
        public Task<List<Event>> SearchByEventName(string name);
        public Task<Event> addEvent(Event newEvent);
        public Task<Event> editEvent(int id, Event newEvent);
        public Task<Event> delEvent(int id);
    }

    
}
