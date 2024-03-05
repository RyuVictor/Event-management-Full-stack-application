using AutoMapper;
using Bajaj.Events.Api.DTOs.UserDtos;
using Bajaj.Events.Api.JWT;
using Bajaj.Events.Dal;
using Bajaj.Events.Dal.Authentication;
using Bajaj.Events.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bajaj.Events.Api.Controllers.V2
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly ICommonRepository<User> _usersRepository;
        private readonly IMapper _mapper;
        private readonly ITokenManager _tokenManager;
        public UsersController(IAuthenticationRepository authenticationRepository, IMapper mapper, ICommonRepository<User> commonRepository, ITokenManager tokenManager)
        {
            _authenticationRepository = authenticationRepository;
            _mapper = mapper;
            _usersRepository = commonRepository;
            _tokenManager = tokenManager;
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDetailsDto>> Get(int id)
        {
            var user = await _usersRepository.GetDetailAsync(id);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<User>> Post(NewUserDto userDto)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<User>(userDto);
                int result = await _authenticationRepository.RegisterUser(user);
                if (result > 0)
                {
                    return CreatedAtAction("Get", new { id = user.UserId }, user);
                }
            }
            else
            {
                return BadRequest();
            }
            return BadRequest();
        }
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuthenticationResponse>> AuthenticateUser(NewUserDto userDto)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<User>(userDto);
                var response = await _authenticationRepository.AuthenticateCredentials(user);
                if (response.IsAuthenticated)
                {

                    response.Token = _tokenManager.GenerateAccessToken(user.UserName, response.RoleName);
                    response.RefreshToken = _tokenManager.GenerateRefreshToken();
                    user.RefreshToken = response.RefreshToken;
                    user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(30);
                    await _authenticationRepository.ModifyUserRefreshToken(user.UserName, response.RefreshToken, user.RefreshTokenExpiry);
                    return Ok(response);
                }
                else
                {
                    return NotFound(response);
                }
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost("Refresh")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Refresh([FromBody] RefreshModel model)
        {
            var principal = _tokenManager.GetPrincipalFromExpiredToken(model.AccessToken);

            if (principal?.Identity?.Name is null)
                return Unauthorized();
            var user = await _authenticationRepository.GetUser(principal.Identity.Name);
            if(user == null) return Unauthorized();
            if (user.RefreshTokenExpiry < DateTime.UtcNow)
            {
                await _authenticationRepository.ModifyUserRefreshToken(principal.Identity.Name, null, DateTime.UtcNow.AddDays(-1));
                return Unauthorized(new { Message = "Your Refresh token is incorrect or it has been expired", Relogin = true });
            }
            if (user.RefreshToken!=model.RefreshToken || user.RefreshTokenExpiry<DateTime.Now) return Unauthorized(new {Message="Your Refresh token is incorrect or it has been expired"});
            var token = _tokenManager.GenerateAccessToken(principal.Identity.Name, principal.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).First());
            await Console.Out.WriteLineAsync(principal.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).First());
            return Ok(new AuthenticationResponse
            {
                Token = token,
                RefreshToken = model.RefreshToken
            });
        }
    }
}