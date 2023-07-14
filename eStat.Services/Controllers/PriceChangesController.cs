using eStat.BLL.Core;
using eStat.Library.Models;
using eStat.Services.Core;
using Microsoft.AspNetCore.Mvc;

namespace eStat.Services.Controllers
{
    public class PriceChangesController : ApiControllerBase
    {
        [HttpGet("product/{productUid}/user/{userUid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserInfo>))]
        public IActionResult GetByUser([FromRoute] Guid userUid, [FromRoute] Guid productUid)
        {
            return Ok(BusinessContext.PriceChangesBL.GetByUser(userUid, productUid));
        }

        [HttpGet("product/{productUid}/user/{userUid}/month")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserInfo>))]
        public IActionResult GetByUserLastMonth([FromRoute] Guid userUid, [FromRoute] Guid productUid)
        {
            return Ok(BusinessContext.PriceChangesBL.GetByUserInLastMonth(userUid, productUid));
        }

        [HttpGet("product/{productUid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserInfo>))]
        public IActionResult GetByProduct([FromRoute] Guid productUid)
        {
            return Ok(BusinessContext.PriceChangesBL.GetByProduct(productUid));
        }

        [HttpGet("product/{productUid}/month")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserInfo>))]
        public IActionResult GetByProductLastMonth([FromRoute] Guid productUid)
        {
            return Ok(BusinessContext.PriceChangesBL.GetByProductInLastMonth(productUid));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserInfo>))]
        public IActionResult Add([FromBody] PriceChangeCreate priceChangeCreate)
        {
            return Ok(BusinessContext.PriceChangesBL.Add(priceChangeCreate));
        }
    }
}
