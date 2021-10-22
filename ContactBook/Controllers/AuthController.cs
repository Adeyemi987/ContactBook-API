using ContactBook.BL;
using ContactBook.DB.DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactBookAPI.Controllers
{
    [ApiController]
    [Route("api /v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthentication _authentication;

        public AuthController(IAuthentication authentication)
        {
            _authentication = authentication;
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserRequest userRequest)
        {
            try
            {
                return Ok(await _authentication.Login(userRequest));
            }
            catch (AccessViolationException)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterationRequest registerationRequest)
        {
            try
            {
                var result = await _authentication.Register(registerationRequest);
                //return CreatedAtAction(nameof(Login), new { Id = result.Id }, result);
                return Created("", result);

            }
            catch (MissingFieldException msex)
            {
                return BadRequest(msex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
