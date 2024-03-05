using AutoMapper;
using Bajaj.Events.Api.DTOs.EventDTOs;
using Bajaj.Events.Dal;
using Bajaj.Events.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bajaj.Events.Api.Controllers.V2
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ICommonRepository<Event> _eventsRepository;
        private readonly IMapper _mapper;

        public EventsController(ICommonRepository<Event> eventsRepository, IMapper mapper)
        {
            _eventsRepository = eventsRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Employee,Hr")]
        public async Task<ActionResult<IEnumerable<EventDto>>> Get()
        {
            var eventList = _mapper.Map<IEnumerable<EventDto>>(await _eventsRepository.GetAllAsync());
            if (eventList.Count() > 0)
            {
                return Ok(eventList);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Employee,Hr")]
        public async Task<ActionResult<EventDetailsDto>> Get(int id)
        {
            var @event = _mapper.Map<EventDetailsDto>( await _eventsRepository.GetDetailAsync(id));
            if (@event != null)
            {
                return Ok(@event);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Hr")]
        public async Task<ActionResult<Event>> Post(NewEventDto eventt)
        {
            if (ModelState.IsValid)
            {
                var eventModel = _mapper.Map<Event>(eventt);
                int result = await _eventsRepository.InsertAsync(eventModel);
                if (result > 0)
                {
                    return CreatedAtAction("Get", eventModel);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Hr")]
        public async Task<ActionResult<Event>> Post(UpdateEventDto eventt)
        {
            if (ModelState.IsValid)
            {
                var eventModel = _mapper.Map<Event>(eventt);
                int result = await _eventsRepository.UpdateAsync(eventModel);
                if (result > 0)
                {
                    return Ok(eventt);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Hr")]

        public async Task<ActionResult> Delete(int id)
        {
            var result = await _eventsRepository.DeleteAsync(id);
            if (result == 0)
            {
                return NotFound();
            }
            if (result >= 0)
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}

