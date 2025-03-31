using FutureArbitrage.Application.Pipeline.Commands;
using FutureArbitrage.Application.Pipeline.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace FutureArbitrage.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FutureArbitrageController : ControllerBase
    {
        protected internal readonly Serilog.ILogger _logger = Log.ForContext(typeof(FutureArbitrageController));
        private readonly IMediator _mediator;
        public FutureArbitrageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("QuarterlyContractArbitrage")]
        public async Task<IActionResult> QuarterlyContractArbitrage(QuarterlyContractArbitrageCommand command)
        {
            try
            {
                _logger.Information($"Start execute => {nameof(FutureArbitrageController)}/{nameof(QuarterlyContractArbitrage)}");
                await _mediator.Send(command);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error($"Error => {nameof(FutureArbitrageController)}/{nameof(QuarterlyContractArbitrage)},  exception message => {ex.Message}");
                return BadRequest(ex);
            }
        }

        [HttpPost("GetArbitrageResults")]
        public async Task<IActionResult> GetArbitrageResults(GetArbitrageResultsQuery query)
        {
            try
            {
                _logger.Information($"Start execute => {nameof(FutureArbitrageController)}/{nameof(GetArbitrageResults)}");
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error($"Error => {nameof(FutureArbitrageController)}/{nameof(GetArbitrageResults)},  exception message => {ex.Message}");
                return BadRequest(ex);
            }
        }
    }
}
