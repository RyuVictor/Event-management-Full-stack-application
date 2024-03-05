using Bajaj.Events.Dal;
using Bajaj.Events.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bajaj.Events.Api.DTOs.RoleDtos;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace Bajaj.Events.Api.Controllers.V2
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly ICommonRepository<Role> _rolesRepository;
        private readonly IMapper _mapper;

        public RolesController(ICommonRepository<Role> rolesRepository, IMapper mapper)
        {
            _rolesRepository = rolesRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Employee,Hr")]
        public async Task<ActionResult<IEnumerable<RoleDto>>> Get()
        {
            var roles = await _rolesRepository.GetAllAsync();
            if (roles.Any())
            {
                return Ok(roles);
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
        public async Task<ActionResult<RoleDetailsDto>> Get(int id)
        {
            var role = await _rolesRepository.GetDetailAsync(id);
            if (role != null)
            {
                return Ok(role);
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
        public async Task<ActionResult<Role>> Post(NewRoleDto role)
        {
            if (ModelState.IsValid)
            {
                var roleModel = _mapper.Map<Role>(role);
                int result = await _rolesRepository.InsertAsync(roleModel);
                if (result > 0)
                {
                    return CreatedAtAction("Get", roleModel);
                }
            }
            return BadRequest();
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Hr")]
        public async Task<ActionResult<Role>> Put(UpdateRoleDto role)
        {
            if (ModelState.IsValid)
            {
                var roleModel = _mapper.Map<Role>(role);
                int result = await _rolesRepository.UpdateAsync(roleModel);
                if (result > 0)
                {
                    return Ok(role);
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
            var result = await _rolesRepository.DeleteAsync(id);
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
