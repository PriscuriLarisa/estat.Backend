﻿using eStat.Library.Models;
using eStat.Services.Core;
using Microsoft.AspNetCore.Mvc;

namespace eStat.Services.Controllers
{
    public class PurchasesController : ApiControllerBase
    {
        [HttpPost("addPurchase/{shoppingCartUid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult AddPurchase([FromRoute] Guid shoppingCartUid)
        {   
            return Ok(BusinessContext.PurchasesBL.AddPurchase(shoppingCartUid));
        }

        [HttpPost("addPurchase/{shoppingCartUid}/{address}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult AddPurchaseWithAddress([FromRoute] Guid shoppingCartUid, [FromRoute] string address)
        {
            return Ok(BusinessContext.PurchasesBL.AddPurchaseWithAddress(shoppingCartUid, address));
        }

        [HttpGet("user/{userUid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetPurchasesByUser([FromRoute] Guid userUid)
        {
            return Ok(BusinessContext.PurchasesBL.GetByUser(userUid));
        }
    }
}
