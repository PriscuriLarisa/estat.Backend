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
            return Ok(BusinessContext.UsersBL.GetAll());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Add([FromBody] UserCreate user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return new ObjectResult(BusinessContext.UsersBL.Add(user)) { StatusCode = StatusCodes.Status201Created };
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
    }
}
