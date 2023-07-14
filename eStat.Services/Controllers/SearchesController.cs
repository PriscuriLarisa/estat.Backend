using eStat.Library.Models;
using eStat.Services.Core;
using Microsoft.AspNetCore.Mvc;

namespace eStat.Services.Controllers
{
    public class SearchesController : ApiControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Add([FromBody] Search search)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return new ObjectResult(BusinessContext.SearchesBL.Add(search)) { StatusCode = StatusCodes.Status201Created };
        }
    }
}
