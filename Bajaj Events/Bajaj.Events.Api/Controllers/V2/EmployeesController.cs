using Bajaj.Events.Dal;
using Bajaj.Events.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bajaj.Events.Api.DTOs.EmployeeDTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Bajaj.Events.Api.DTOs.EventDTOs;

namespace Bajaj.Events.Api.Controllers.V2
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ICommonRepository<Employee> _EmployeeRepository;
        private readonly IMapper _mapper;

        public EmployeesController(ICommonRepository<Employee> EmployeeRepository, IMapper mapper)
        {
            _EmployeeRepository = EmployeeRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Employee,Hr")]

        public async Task<ActionResult<IEnumerable<EmployeeDto>>> Get()
        {
            var employeeList = _mapper.Map<IEnumerable<EmployeeDto>>(await _EmployeeRepository.GetAllAsync());
            if (employeeList.Any())
            {
                return Ok(employeeList);
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
        public async Task<ActionResult<EmployeeDetailsDto>> Get(int id)
        {
            var employeeList = _mapper.Map<EmployeeDetailsDto>(await _EmployeeRepository.GetDetailAsync(id));
            if (employeeList != null)
            {
                return Ok(employeeList);
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
        public async Task<ActionResult<Employee>> Post(NewEmployeeDto Employee)
        {
            if (ModelState.IsValid)
            {
                var EmployeeModel = _mapper.Map<Employee>(Employee);
                int result = await _EmployeeRepository.InsertAsync(EmployeeModel);
                if (result > 0)
                {
                    return CreatedAtAction("Get", EmployeeModel);
                }
            }
            return BadRequest();
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Hr")]

        public async Task<ActionResult<Employee>> Put(UpdateEmployeeDto Employee)
        {
            if (ModelState.IsValid)
            {
                var EmployeeModel = _mapper.Map<Employee>(Employee);
                int result = await _EmployeeRepository.UpdateAsync(EmployeeModel);
                if (result > 0)
                {
                    return Ok(Employee);
                }
            }
            return BadRequest();
        }
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Hr")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _EmployeeRepository.DeleteAsync(id);
            if (result > 0)
            {
                return NoContent();
            }
            if (result >= 0)
            {
                return NoContent();
            }
            return BadRequest();
        }
    }
}
