using eStat.BLL.Core;
using eStat.Library.Models;
using eStat.Services.Core;
using Microsoft.AspNetCore.Mvc;

namespace eStat.Services.Controllers
{
    public class ShoppingCartProductsController : ApiControllerBase
    {
        [HttpDelete("{uid:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserInfo>))]
        public IActionResult Delete([FromRoute] Guid uid)
        {
            BusinessContext.ShoppingCartProductsBL.Delete(uid);
            return Ok();
        }
    }
}
