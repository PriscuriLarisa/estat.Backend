using eStat.Library.Models;
using eStat.Services.Core;
using Microsoft.AspNetCore.Mvc;

namespace eStat.Services.Controllers
{
    public class NotificationsController : ApiControllerBase
    {
        [HttpGet("user/{userUid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserInfo>))]
        public IActionResult GetByUser([FromRoute] Guid userUid)
        {
            return Ok(BusinessContext.NotificationsBL.GetByUser(userUid));
        }

        [HttpPut("user/{userUid}/readAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserInfo>))]
        public IActionResult ReadByUser([FromRoute] Guid userUid)
        {
            BusinessContext.NotificationsBL.ReadAllUserNotifications(userUid);
            return Ok();
        }
    }
}
