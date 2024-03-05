using Asp.Versioning;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Bajaj.Events.Api.Controllers.V2
{
    [EnableCors]
    //[Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    [ApiVersion("2.0")]
    [ApiVersion("2.1")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet(Name = "citiesv2")]
        public string[] GetCitiesV2()
        {
            return new string[] { "Bangalore", "Chennai", "Hyderabad", "Mumbai", "Pune" };
        }
        [HttpGet("states", Name = "statesv2")]
        public string[] GetStatesV2()
        {
            return new string[] { "Andhra Pradesh", "Tamilnadu", "Karnatak", "Rajasthan", "Maharashtra" };
        }
        [MapToApiVersion("2.1")]
        [HttpGet("states",Name = "statesv21")]
        public string[] GetStatesV21()
        {
            return new string[] { "Andhra Pradesh", "Madhya Pradesh", "Gujarat", "Tamilnadu", "Karnatak", "Rajasthan", "Maharashtra" };
        }
    }
}