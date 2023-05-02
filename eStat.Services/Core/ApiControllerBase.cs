using eStat.BLL.Core;
using Microsoft.AspNetCore.Mvc;

namespace eStat.Services.Core;
[Route("api/[controller]")]
[ApiController]
public class ApiControllerBase : ControllerBase
    {
        private BusinessContext _businessContext = null!;
        protected BusinessContext BusinessContext => _businessContext ??= HttpContext.RequestServices.GetRequiredService<BusinessContext>();
    }
