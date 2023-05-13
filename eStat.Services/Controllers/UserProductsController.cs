using eStat.Library.Models;
using eStat.Services.Core;
using Microsoft.AspNetCore.Mvc;

namespace eStat.Services.Controllers
{
    public class UserProductsController : ApiControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserProduct>))]
        public IActionResult GetAll()
        {
            return Ok(BusinessContext.UserProductsBL.GetAll());
        }

        [HttpGet("product/{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserProduct>))]
        public IActionResult GetUserProductsByProduct([FromRoute] Guid productId)
        {
            return Ok(BusinessContext.UserProductsBL.GetUserProductsByProduct(productId));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserProduct>))]
        public IActionResult Update([FromBody] UserProduct userProduct)
        {
            BusinessContext.UserProductsBL.Update(userProduct);
            return Ok();
        }

        [HttpGet("user/{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserProduct>))]
        public IActionResult GetUserProductsByUser([FromRoute] Guid productId)
        {
            return Ok(BusinessContext.UserProductsBL.GetUserProductsByUser(productId));
        }

        [HttpGet("user/{userId}/{batchNb}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserProduct>))]
        public IActionResult GetUserProductsByUserInBatches([FromRoute] Guid userId, [FromRoute] int batchNb)
        {
            return Ok(BusinessContext.UserProductsBL.GetUserProductsByProductInBatches(userId, batchNb));
        }
    }
}
