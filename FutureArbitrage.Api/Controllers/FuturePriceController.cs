using Microsoft.AspNetCore.Mvc;

namespace FutureArbitrage.Api.Controllers
{
    [ApiController]
    [Route("api/prices")]
    public class FuturePriceController : ControllerBase
    {
        //private readonly IMediator _mediator;

        [HttpGet]
        public async Task<IActionResult> GetAllPrices()
        {
            //var prices = await _repository.GetAllAsync();
            return Ok();
        }
    }
}
