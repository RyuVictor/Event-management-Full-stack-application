using Asp.Versioning;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace Bajaj.Events.Api.Controllers.V1
{
    [EnableCors]
    //[Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public string[] cities = null;
        [HttpGet(Name = "citiesv10")]
        public string[] GetCitiesV1()
        {
            throw new ArithmeticException();
            //return new string[] { "Bangalore", "Chennai", "Hyderabad" };
            return cities;
        }
        //[HttpGet("states",Name = "statesv1")]
        //public string[] GetStatesV1()
        //{
        //    throw new HttpRequestException("Not found");
        //    return new string[] { "Andhra Pradesh", "Tamilnadu", "Karnatak" };
        //}
        [HttpGet("states")]
        public IActionResult GetStatesV1()
        {
            try
            {
                throw new Exception("Not found");
                // This code will not be reached due to the exception being thrown above.
                return Ok(new string[] { "Andhra Pradesh", "Tamilnadu", "Karnataka" });
            }
            catch (Exception ex)
            {
                // Handle the exception here.
                Console.WriteLine($"Exception: {ex.Message}");
                // Set the HTTP status code to 404 Not Found.
                return StatusCode((int)HttpStatusCode.NotFound, "Resource not found");
            }
        }

    }
}
