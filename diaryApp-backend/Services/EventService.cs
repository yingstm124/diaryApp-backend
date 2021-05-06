using System;
using diaryApp_backend.Services.Interfaces;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace diaryApp_backend.Services
{
    public class EventService : IEventService
    {
        private readonly diaryAppContext db = new diaryAppContext();

        public EventService()
        {
        }
        public async Task<List<Event>> GetEvents(string uid) {

            var events = await db.Events.Where(e => e.UserId == uid).ToListAsync();

            return events.Select(e => new Event()
            {
                Id = e.Id,
                DateTime = e.DateTime,
                EventName = e.EventName,
                Memo = e.Memo,
                UserId = e.UserId

            }).ToList();
                
        }


        public async Task<Event> GetEventDetail(int id)
        {
            var detail = await db.Events.Where(d => d.Id == id).FirstOrDefaultAsync();

            if (detail == null) {
                return null;
            }
            else {
                return detail;
            }
        }

        public async Task<List<Event>> SearchByEventName(string name) {

            var content = await db.Events.Where(e => e.EventName == name).ToListAsync();

            if (content == null) {
                return null;
            }
            else {
                return content;
            }

        }

        public async Task<Event> addEvent(Event eventObj)
        {

            var ev = new Event()
            {

                DateTime = eventObj.DateTime,
                EventName = eventObj.EventName,
                Memo = eventObj.Memo,
                UserId = eventObj.UserId
            };

          

            try {
                db.Add(ev);
                await db.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException err)
            {
                throw err;
            }


            return ev;
            

        }

        public async Task<Event> editEvent(int id, Event newEvent)
        {
            var editObj = db.Events.First(a => a.Id == id);

            editObj.EventName = newEvent.EventName;
            editObj.Memo = newEvent.Memo;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException err)
            {
                throw err;
            }

            return newEvent;
        }

        public async Task<Event> delEvent(int id)
        {
            var delObj = db.Events.Find(id);

            //var delObj = await db.Events.Where(e => e.Id == id).FirstAsync();


            try {
                
                db.Remove(delObj);
                await db.SaveChangesAsync();
                
            }
            catch (DbUpdateConcurrencyException err) {
                throw err;
            }

            return new Event()
            {
                Id = delObj.Id,
                DateTime = delObj.DateTime,
                EventName = delObj.EventName,
                Memo = delObj.Memo,
                UserId = delObj.UserId
            };
        }


    }
}
