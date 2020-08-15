using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreAuthorization.Infrastructure.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreAuthorization.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class TestApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Successful");
        }

        [HttpGet("{id}")]
        [AcceptHeader("Custom")]
        public IActionResult Get(int id)
        {
            /*This action is only accessible if you have accept header with a value of Custom*/

            return Ok($"Succesful with id: {id}");
        }
    }
}
