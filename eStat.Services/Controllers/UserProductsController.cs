using eStat.Library.Models;
using eStat.Services.Core;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

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

        [HttpGet("{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserProduct))]
        public IActionResult GetById([FromRoute] Guid productId)
        {
            if (productId == Guid.Empty)
                return NotFound();
            return Ok(BusinessContext.UserProductsBL.GetByUid(productId));
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

        [HttpGet("avg/{userProductUid}")]
        public IActionResult GetUserProductAveragePricesLast6Months([FromRoute] Guid userProductUid)
        {
            return Ok(BusinessContext.UserProductsBL.GetAvgPriceLastSixMonthsByUserProduct(userProductUid));
        }

        [HttpGet("user/{userId}/{batchNb}/{keywords}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserProduct>))]
        public IActionResult GetUserProductsByUserInBatches([FromRoute] Guid userId, [FromRoute] int batchNb, [FromRoute] string? keywords)
        {
            List<string> keywordsList = keywords.Split(" ").ToList();
            if (keywordsList.Count == 1 && keywordsList[0] == "_empty_search_bar_")
                keywordsList = new List<string>();
            return Ok(BusinessContext.UserProductsBL.GetUserProductsByProductInBatches(userId, batchNb, keywordsList));
        }

        [HttpGet("mySellsLast6Months/{uid}")]
        public IActionResult GetUserProductsByUserInBatches([FromRoute] Guid uid)
        {
            return Ok(BusinessContext.UserProductsBL.GetMySellsLastSixMonths(uid));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult CreateUserProduct([FromBody] UserProduct userProduct)
        {
            UserProduct? addedUserProduct = BusinessContext.UserProductsBL.Add(userProduct);
            if (addedUserProduct == null)
            {
                return NotFound();
            }
            return Ok(addedUserProduct);
        }
    }
}
