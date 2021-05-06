using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using diaryApp_backend;
using diaryApp_backend.Services.Interfaces;

namespace diaryApp_backend.Controllers
{
    [Route("api/event")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet("{uid}")]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents(string uid) {

            return await _eventService.GetEvents(uid);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("detail/{id}")]
        public async Task<ActionResult<Event>> GetEventDetail(int id) {

            var task = await _eventService.GetEventDetail(id);
            if (task == null)
            {
                return NotFound();
            }
            return task;
        }

        [HttpGet("serach/{name}")]
        public async Task<ActionResult<IEnumerable<Event>>> SearchByName(string name) {

            return await _eventService.SearchByEventName(name);
        }


        [HttpPost("add")]
        public async Task<ActionResult<Event>> AddEvent([FromBody]Event newEvent) {
            return await _eventService.addEvent(newEvent);
        }


        [HttpPut("edit")]
        public async Task<ActionResult<Event>> ModifyEvent(int eventId,[FromBody]Event edit_event) {
            return await _eventService.editEvent(eventId, edit_event);
        }


        [HttpDelete("id")]
        public async Task<ActionResult<Event>> DelEvent(int id) {
            return await _eventService.delEvent(id);
        }


       

        
    }
}
