﻿using eStat.Common.Enums;
using eStat.Library.Models;
using eStat.Services.Core;
using Microsoft.AspNetCore.Mvc;

namespace eStat.Services.Controllers
{
    public class ProductsController : ApiControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserInfo>))]
        public IActionResult GetAll()
        {
            return Ok(BusinessContext.ProductsBL.GetAll());
        }

        [HttpGet("page/{pageNumber}/{productsPerPage}/{sortingCriteria}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserInfo>))]
        public IActionResult GetIProductsByPage([FromRoute] int pageNumber, [FromRoute] int productsPerPage, [FromRoute] SortingCriteria sortingCriteria)
        {
            return Ok(BusinessContext.ProductsBL.GetProductsByPage(pageNumber, productsPerPage, null, sortingCriteria));
        }

        [HttpGet("page/{pageNumber}/{productsPerPage}/{sortingCriteria}/{category}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserInfo>))]
        public IActionResult GetIProductsByPageAndCategory([FromRoute] int pageNumber, [FromRoute] int productsPerPage, [FromRoute] SortingCriteria sortingCriteria, [FromRoute] string category)
        {
            return Ok(BusinessContext.ProductsBL.GetProductsByPage(pageNumber, productsPerPage, category, sortingCriteria));
        }

        [HttpGet("page/{productsPerPage}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserInfo>))]
        public IActionResult GetIProductsByLastPage([FromRoute] int productsPerPage)
        {
            return Ok(BusinessContext.ProductsBL.GetProductsByLastPage(productsPerPage));
        }

        [HttpGet("page/{pageNumber}/{productsPerPage}/{sortingCriteria}/search/{keywords}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserInfo>))]
        public IActionResult GetSearchedProducts([FromRoute] int pageNumber, [FromRoute] int productsPerPage, [FromRoute] SortingCriteria sortingCriteria, [FromRoute] string keywords)
        {
            List<string> keywordsList = keywords.Split(" ").ToList();
            return Ok(BusinessContext.ProductsBL.GetSearchedProductsByPage(pageNumber, productsPerPage, keywordsList, sortingCriteria));
        }

        [HttpGet("search/{keywords}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserInfo>))]
        public IActionResult GetNumberOfProductsBySearch([FromRoute] string keywords)
        {
            List<string> keywordsList = keywords.Split(" ").ToList();
            return Ok(BusinessContext.ProductsBL.GetNumberOfProductsBySearch(keywordsList));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Add([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return new ObjectResult(BusinessContext.ProductsBL.Add(product)) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpGet("{uid:Guid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetByUid([FromRoute] Guid uid)
        {
            Product? product = BusinessContext.ProductsBL.GetByUid(uid);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet("categories")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetProductsCategory()
        {
            return Ok(BusinessContext.ProductsBL.GetProductCategories());
        }

        [HttpDelete("{uid:Guid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Delete([FromRoute] Guid uid)
        {
            Product? product = BusinessContext.ProductsBL.GetByUid(uid);
            if (product == null)
            {
                return NotFound();
            }
            BusinessContext.ProductsBL.Delete(uid);
            return Ok();
        }

        [HttpGet("avgPriceLastSixMonths/{productUid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAvgPriceLastSixMonths([FromRoute] Guid productUid)
        {
            if(productUid == Guid.Empty)
                return NotFound();
            return Ok(BusinessContext.ProductsBL.GetAvgPriceLastSixMonths(productUid));
        }

        [HttpGet("highestPriceLastSixMonths/{productUid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetHighestPriceLastSixMonths([FromRoute] Guid productUid)
        {
            if (productUid == Guid.Empty)
                return NotFound();
            return Ok(BusinessContext.ProductsBL.GetHighestPriceLastSixMonths(productUid));
        }

        [HttpGet("lowestPriceLastSixMonths/{productUid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetLowestPriceLastSixMonths([FromRoute] Guid productUid)
        {
            if (productUid == Guid.Empty)
                return NotFound();
            return Ok(BusinessContext.ProductsBL.GetLowestPriceLastSixMonths(productUid));
        }

        [HttpGet("sellsForLowest/{productUid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetSellsForLowestPriceLastMonth([FromRoute] Guid productUid)
        {
            if (productUid == Guid.Empty)
                return NotFound();
            return Ok(BusinessContext.ProductsBL.GetSellsForLowestPrice(productUid));
        }

        [HttpGet("sellsForHighest/{productUid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetSellsForHighestPriceLastMonth([FromRoute] Guid productUid)
        {
            if (productUid == Guid.Empty)
                return NotFound();
            return Ok(BusinessContext.ProductsBL.GetSellsForHighestPrice(productUid));
        }

        [HttpGet("sellsForAvg/{productUid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetSellsForAveragePriceLastMonth([FromRoute] Guid productUid)
        {
            if (productUid == Guid.Empty)
                return NotFound();
            return Ok(BusinessContext.ProductsBL.GetSellsForAveragePrice(productUid));
        }

        [HttpGet("sellsLast6Months/{productUid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetSellsLast6Months([FromRoute] Guid productUid)
        {
            if (productUid == Guid.Empty)
                return NotFound();
            return Ok(BusinessContext.ProductsBL.GetSellsLastSixMonths(productUid));
        }

        [HttpGet("currentAveragePrice/{productUid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetCurrentAveragePrice([FromRoute] Guid productUid)
        {
            if (productUid == Guid.Empty)
                return NotFound();
            return Ok(BusinessContext.ProductsBL.GetAveragePrice(productUid));
        }
    }
}
