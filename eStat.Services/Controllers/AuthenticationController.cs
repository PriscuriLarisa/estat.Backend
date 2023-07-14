using eStat.Library.Models;
using eStat.Services.Core;
using Microsoft.AspNetCore.Mvc;

namespace eStat.Services.Controllers
{
    public class AuthenticationController : ApiControllerBase
    {
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            var user = BusinessContext.AuthenticationBL.Login(userLogin);
            if (user == null)
            {
                return BadRequest(new { message = "Invalid credentials" });
            }

            var jwt = BusinessContext.JWTBL.Generate(user.UserGUID);
            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true,
                IsEssential = true,
                SameSite = SameSiteMode.None,
                Secure = true
            });

            return Ok(userLogin);
        }

        [HttpGet("authenticatedUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAutheticatedUser()
        {
            var jwt = Request.Cookies["jwt"];
            if(jwt == null)
            {
                return Unauthorized(new { message = "" });
            }
            var token = BusinessContext.JWTBL.Verify(jwt);
            Guid userId = Guid.Parse(token.Issuer);
            var user = BusinessContext.UsersBL.GetUserInfo(userId);
            if( user == null)
            {
                return Unauthorized(new { message = "" });
            }

            return Ok(user);
        }

        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt", new CookieOptions
            {
                HttpOnly = true,
                IsEssential = true,
                SameSite = SameSiteMode.None,
                Secure = true
            }
            );
            return Ok(new {message = "Success" });
        }

    }
}
