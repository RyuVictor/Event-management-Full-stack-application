using Bajaj.Events.Api.DTOs.EmployeeDTOs;
using Bajaj.Events.Models;
using AutoMapper;
using Bajaj.Events.Api.DTOs.EventDTOs;

namespace Bajaj.Events.Api.Profiles
{
	public class EventProfile: Profile
	{
		public EventProfile()
		{
			CreateMap<Event, EventDto>();
			CreateMap<Event, EventDetailsDto>();
			CreateMap<NewEventDto, Event>();
			CreateMap<UpdateEventDto, Event>();
		}
	}
}
