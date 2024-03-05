using AutoMapper;
using Bajaj.Events.Api.DTOs.EmployeeDTOs;
using Bajaj.Events.Models;

namespace Bajaj.Events.Api.Profiles
{
	public class EmployeeProfile: Profile
	{
		public EmployeeProfile()
		{
			CreateMap<Employee, EmployeeDto>();
			CreateMap<Employee, EmployeeDetailsDto>();
			CreateMap<NewEmployeeDto, Employee>();
			CreateMap<UpdateEmployeeDto, Employee>();
		}
	}
}
