using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FutureArbitrage.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FuturePriceController : ControllerBase
    {
        private readonly IMediator _mediator;

        [HttpGet]
        public async Task<IActionResult> GetAllPrices()
        {


            return Ok();
        }
    }
}
