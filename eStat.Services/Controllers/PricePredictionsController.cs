using eStat.Library.Models;
using eStat.Services.Core;
using Microsoft.AspNetCore.Mvc;

namespace eStat.Services.Controllers
{
    public class PricePredictionsController : ApiControllerBase
    {
        [HttpGet("{uid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PricePrediction>))]
        public IActionResult GetByUid([FromRoute] Guid uid)
        {
            return Ok(BusinessContext.PricePredictionsBL.GetByUid(uid));
        }
    }
}
