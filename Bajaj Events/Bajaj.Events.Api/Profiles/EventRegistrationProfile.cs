
using Bajaj.Events.Models;
using AutoMapper;
using Bajaj.Events.Api.DTOs.EventRegistrationDTOs;
namespace Bajaj.Events.Api.Profiles
{
	public class EventRegistrationProfile: Profile
	{
		public EventRegistrationProfile()
		{
			CreateMap<EventRegistration, EventRegistrationDto>();
			CreateMap<EventRegistration, EventRegistrationDetailsDto>();
			CreateMap<NewEventRegistrationDto, EventRegistration>();
			CreateMap<UpdateEventRegistrationDto, EventRegistration>();
		}
	}
}
