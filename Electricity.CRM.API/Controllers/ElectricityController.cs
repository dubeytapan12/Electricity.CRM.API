using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Electricity.CRM.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ElectricityController : ControllerBase
    {
        [HttpGet]
        public List<string> Get()
        {
            var users = new List<string>
        {
            "Electricity MockUser",
            "Mock User 2",
            "Mock User 3"
        };

            return users;
        }
    }
}
