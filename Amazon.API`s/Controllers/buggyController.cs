using Amazon.API_s.Controllers;
using Amazon.API_s.Errors;
using Amazon.repository.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace YourNamespace.Controllers
{
    
    public class BuggyController : BaseApiController
    {
        private readonly StoreContext _context;

        public BuggyController(StoreContext context)
        {
            _context = context;
        }
        // GET: api/buggy/notfound
        [HttpGet("notfound")]
        public IActionResult GetNotFound()
        {
            return NotFound(new ApiResponse(404));
        }

        // GET: api/buggy/servererror
        [HttpGet("servererror")]
        public IActionResult GetServerError()
        {
            var product = _context.Products.Find(100);
            var productString = product.ToString();
            return Ok(productString);
        }

        // GET: api/buggy/badrequest
        [HttpGet("badrequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest(new
            {
                Status = 400,
                Message = "Bad request occurred."
            });
        }

        // POST: api/buggy/validationerror
        [HttpPost("validationerror")]
        public IActionResult PostValidationError(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(new
            {
                Message = "Request is valid."
            });
        }
    }

    
}