using Booking.Application.Common.Models;
using Booking.Application.Common.Request;
using Booking.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        private readonly ILogger<AirportController> _logger;
        private readonly IPlaneService _service;

        public AirportController(ILogger<AirportController> logger, IPlaneService service)
        {
            _logger = logger;
            _service = service;
        }
        // GET: api/<AirportController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var planes = await _service.GetAirportsAsync();
            return Ok(planes);
        }

        // GET api/<AirportController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var plane = await _service.GetAirportByIdAsync(id);
            return Ok(plane);
        }

        // POST api/<AirportController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post([FromBody] AirportDto request)
        {
            try
            {
                var practiceId = await _service.CreateAirportAsync(request);
                return Created($"/practice/{practiceId}", practiceId);
            }
            catch (ArgumentException aex)
            {
                return BadRequest(aex.Message);
            }
        }

        // PUT api/<AirportController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AirportDto request)
        {
            try
            {
                var plane = await _service.UpdateAirportAsync(request, id);
                return Ok(plane);
            }
            catch (ArgumentException aex)
            {
                return BadRequest(aex.Message);
            }
        }
    }
}
