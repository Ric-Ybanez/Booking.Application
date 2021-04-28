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
    public class PlaneController : ControllerBase
    {
        private readonly ILogger<PlaneController> _logger;
        private readonly IPlaneService _service;

        public PlaneController(ILogger<PlaneController> logger, IPlaneService service)
        {
            _logger = logger;
            _service = service;
        }
        // GET: api/<PlaneController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var planes = await _service.GetPlanesAsync();
            return Ok(planes);
        }

        // GET api/<PlaneController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var plane = await _service.GetPlaneByIdAsync(id);
            return Ok(plane);
        }

        // POST api/<PlaneController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post([FromBody] PlaneCreateRequest request)
        {
            try
            {
                var practiceId = await _service.CreatePlaneAsync(request);
                return Created($"/practice/{practiceId}", practiceId);
            }
            catch (ArgumentException aex)
            {
                return BadRequest(aex.Message);
            }
        }

        // PUT api/<PlaneController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] PlaneDto request)
        {   
            try
            {
                var plane = await _service.UpdatePlaneAsync(request, id);
                return Ok(plane);
            }
            catch (ArgumentException aex)
            {
                return BadRequest(aex.Message);
            }
        }
    }
}
