using Booking.Application.Common.Models;
using Booking.Application.Common.Request;
using Booking.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Booking.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TutorController : ControllerBase
    {
        private readonly ILogger<TutorController> _logger;
        private readonly ITutorService _service;

        public TutorController(ILogger<TutorController> logger, ITutorService service)
        {
            _logger = logger;
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var planes = await _service.GetTutorsAsync();
            return Ok(planes);
        }

        // GET api/<PlaneController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var tutor = await _service.GetTutorByIdAsync(id);

            if (tutor == null)
                return NotFound();

            return Ok(tutor);
        }


        // POST api/<PracticeController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post([FromBody] TutorCreateRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var tutorid = await _service.CreateTutorAsync(request);
                return Created($"/tutor/{tutorid}", tutorid);
            }
            catch (ArgumentException aex)
            {
                return BadRequest(aex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TutorDto request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var plane = await _service.UpdateTutorAsync(request, id);
                return Ok(plane);
            }
            catch (ArgumentException aex)
            {
                return BadRequest(aex.Message);
            }
        }
    }
}
