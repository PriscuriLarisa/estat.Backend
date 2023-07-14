using eStat.Library.Models;
using eStat.Services.Core;
using Microsoft.AspNetCore.Mvc;

namespace eStat.Services.Controllers
{
    public class UsersController : ApiControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserInfo>))]
        public IActionResult GetAll()
        {
            return Ok(BusinessContext.UsersBL.GetAllInfo());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Add([FromBody] UserCreate user)
        {
            var createdUser = BusinessContext.UsersBL.Add(user);
            if (createdUser == null)
                return BadRequest();
            
            return new ObjectResult(createdUser) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpPut("userInfo")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult UpdateUserInfo([FromBody] UserInfo user)
        {
            BusinessContext.UsersBL.UpdateUserInfo(user);
            return Ok();
        }

        [HttpGet("{uid:Guid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetByUid([FromRoute] Guid uid)
        {
            User? user = BusinessContext.UsersBL.GetByUid(uid);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("userInfo/{uid:Guid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetUserInfo([FromRoute] Guid uid)
        {
            UserInfo? user = BusinessContext.UsersBL.GetUserInfo(uid);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}
