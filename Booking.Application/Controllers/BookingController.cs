using Booking.Application.Common.Models;
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
    public class BookingController : ControllerBase
    {
        private readonly ILogger<BookingController> _logger;
        private readonly IBookingService _service;

        public BookingController(ILogger<BookingController> logger, IBookingService service)
        {
            _logger = logger;
            _service = service;
        }
        // GET: api/<PracticeController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PracticeController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PracticeController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post([FromBody] BookingDto request)
        {
            try
            {
                var practiceId = await _service.CreateBookingAsync(request);
                return Created($"/practice/{practiceId}", practiceId);
            }
            catch (ArgumentException aex)
            {
                return BadRequest(aex.Message);
            }
        }

        // PUT api/<PracticeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
    }
}
