using AutoMapper;
using Bajaj.Events.Api.DTOs.EventRegistrationDTOs;
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
    public class EventRegistrationsController : ControllerBase
    {
        private readonly ICommonRepository<EventRegistration> _eventregistrationsRepository;
        private readonly IMapper _mapper;

        public EventRegistrationsController(ICommonRepository<EventRegistration> eventregistrationsRepository, IMapper mapper)
        {
            _eventregistrationsRepository = eventregistrationsRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Employee,Hr")]
        public async Task<ActionResult<IEnumerable<EventRegistrationDto>>> Get()
        {
            var eventregistrations = await _eventregistrationsRepository.GetAllAsync();
            if (eventregistrations.Count > 0)
            {
                return Ok(eventregistrations);
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

        public async Task<ActionResult<EventRegistrationDetailsDto>> Get(int id)
        {
            var eventregistration = await _eventregistrationsRepository.GetDetailAsync(id);
            if (eventregistration != null)
            {
                return Ok(eventregistration);
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
        public async Task<ActionResult<EventRegistration>> Post(NewEventRegistrationDto eventregistration)
        {
            if (ModelState.IsValid)
            {
                var eventregistrationModel = _mapper.Map<EventRegistration>(eventregistration);
                int result = await _eventregistrationsRepository.InsertAsync(eventregistrationModel);
                if (result > 0)
                {
                    return CreatedAtAction("Get", eventregistrationModel);
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
        public async Task<ActionResult<EventRegistration>> Post(UpdateEventRegistrationDto eventregistration)
        {
            if (ModelState.IsValid)
            {
                var eventregistrationModel = _mapper.Map<EventRegistration>(eventregistration);
                int result = await _eventregistrationsRepository.UpdateAsync(eventregistrationModel);
                if (result > 0)
                {
                    return Ok(eventregistration);
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
            var result = await _eventregistrationsRepository.DeleteAsync(id);
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
