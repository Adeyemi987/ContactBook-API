using ContactBook.BL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactBook.DB.DTOS;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Amazon.IdentityManagement.Model;
using ContactBook.Utilities;

namespace ContactBookAPI.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("api/v1/[controller]/getUser")]
        [HttpGet]
        public async Task<IActionResult> Get(string userId)
        {
            try
            {
                return Ok(await _userService.GetUser(userId));
            }
            catch (ArgumentException argex)
            {
                return BadRequest(argex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }


        [Route("api/v1/[controller]/update")]
        [HttpPatch]
        public async Task<IActionResult> Update(UpdateRequest updateRequest)
        {
            try
            {
                var userId = HttpContext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;
                var result = await _userService.Update("", updateRequest);
                return NoContent();
            }
            catch (MissingMemberException mmex)
            {
                return BadRequest(mmex.Message);
            }
            catch (ArgumentException argex)
            {
                return BadRequest(argex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }     
            
        }

        [Route("api/v1/[controller]/delete")]
        [HttpDelete]
        [Authorize(Roles = "Admin" )]
        public async Task<IActionResult> Delete(string userId)
        {
            try
            {
                await _userService.DeleteUser(userId);
                return NoContent();

            }
            catch (MissingMemberException mmex)
            {
                return BadRequest(mmex.Message);
            }
            catch (ArgumentException argex)
            {
                return BadRequest(argex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [Route("api/v1/[controller]/getAllUsers")]
        [HttpGet]
        public IActionResult GetAll([FromQuery] Pagination pagination)
        {
            
            try
            {
                Pagination pagenation = new Pagination(pagination.PageSize, pagination.CurrentPage);
                return Ok( _userService.GetAllUsers().Skip((pagination.CurrentPage -1)*pagination.PageSize).Take(pagination.PageSize).ToArray());
            }
            catch (NullReferenceException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
