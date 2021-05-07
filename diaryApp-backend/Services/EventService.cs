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

        public EventService(diaryAppContext dbContext)
        {
            db = dbContext;
        }
        public async Task<List<Events>> GetEvents(string uid) {

            var events = await db.Events.Where(e => e.UserId == uid).ToListAsync();

     
            return events.Select(e => new Events()
            {
                Id = e.Id,
                DateTime = e.DateTime,
                EventName = e.EventName,
                Memo = e.Memo,
                UserId = e.UserId

            }).ToList();
                
        }


        public async Task<Events> GetEventDetail(int id)
        {
            var detail = await db.Events.Where(d => d.Id == id).FirstOrDefaultAsync();

            if (detail == null) {
                return null;
            }
            else {
                return detail;
            }
        }

        public async Task<List<Events>> SearchByEventName(string name) {

            var content = await db.Events.Where(e => e.EventName == name).ToListAsync();

            if (content == null) {
                return null;
            }
            else {
                return content;
            }

        }

        public async Task<Events> addEvent(Events eventObj)
        {

            var ev = new Events()
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

        public async Task<Events> editEvent(int id, Events newEvent)
        {
            try
            {
                var userObj = db.Users.Single(u => u.Id == newEvent.UserId);
            }
            catch (Exception err)
            {
                throw err;
            }

            var editObj = db.Events.First(a => a.Id == id);

            editObj.EventName = newEvent.EventName;
            editObj.Memo = newEvent.Memo;
            await db.SaveChangesAsync();
            
            return newEvent;
        }

        public async Task<Events> delEvent(int id)
        {
            var delObj = db.Events.Find(id);

            //var delObj = await db.Events.Where(e => e.Id == id).FirstAsync();


            try {
                
                db.Remove(delObj);
                await db.SaveChangesAsync();
                
            }
            catch (DbUpdateConcurrencyException err)
            {
                throw err;
                //return null;
            }

            return new Events()
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
