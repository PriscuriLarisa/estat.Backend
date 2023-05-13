using eStat.Library.Models;
using eStat.Services.Core;
using Microsoft.AspNetCore.Mvc;

namespace eStat.Services.Controllers
{
    public class ShoppingCartController : ApiControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserInfo>))]
        public IActionResult GetAll()
        {
            return Ok(BusinessContext.ShoppingCartsBL.GetAll());
        }


        [HttpGet("{uid:Guid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetByUid([FromRoute] Guid uid)
        {
            ShoppingCart? shoppingCart = BusinessContext.ShoppingCartsBL.GetByUid(uid);
            if (shoppingCart == null)
            {
                return NotFound();
            }
            return Ok(shoppingCart);
        }

        [HttpGet("user/{userUid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetShoppingCartByUser([FromRoute] Guid userUid)
        {
            ShoppingCart? shoppingCart = BusinessContext.ShoppingCartsBL.GetshoppingCartByUser(userUid);
            if (shoppingCart == null)
            {
                return NotFound();
            }
            return Ok(shoppingCart);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult CreateShoppingCart([FromBody] ShoppingCartCreate shoppingCart)
        {
            ShoppingCart? createdShoppingCart = BusinessContext.ShoppingCartsBL.Add(shoppingCart);
            if (createdShoppingCart == null)
            {
                return NotFound();
            }
            return Ok(createdShoppingCart);
        }

        [HttpPut("addItemToCart")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult AddItemToCart([FromBody] ShoppingCartProductAdd shoppingCartProduct)
        {
            BusinessContext.ShoppingCartsBL.AddItemToCart(shoppingCartProduct);
            return Ok();
        }
    }
}
