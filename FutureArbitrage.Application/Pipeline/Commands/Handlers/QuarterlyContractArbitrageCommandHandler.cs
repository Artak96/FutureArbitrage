using FutureArbitrage.Application.Dtos;
using FutureArbitrage.Application.Services.Abstructions;
using MediatR;
using Serilog;

namespace FutureArbitrage.Application.Pipeline.Commands.Handlers
{
    internal class QuarterlyContractArbitrageCommandHandler : IRequestHandler<QuarterlyContractArbitrageCommand>
    {
        protected internal readonly ILogger _logger = Log.ForContext(typeof(QuarterlyContractArbitrageCommandHandler));

        private readonly IArbitrageCalculatorService _arbitreageCalculator;

        public QuarterlyContractArbitrageCommandHandler(IArbitrageCalculatorService arbitreageCalculator)
        {
            _arbitreageCalculator = arbitreageCalculator;
        }

        public async Task Handle(QuarterlyContractArbitrageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.Information($"Start => {nameof(QuarterlyContractArbitrageCommandHandler)} => request {System.Text.Json.JsonSerializer.Serialize(request)}");
                await _arbitreageCalculator.SetExchange(request.ExchangeType);
                var arbitageCalculator = new ArbitrageCalculatorDto
                {
                    Asset = request.Asset,
                    EndTime = DateTime.UtcNow,
                    Interval = TimeSpan.FromHours(request.IntervalByHours),
                    StartTime = DateTime.UtcNow.AddDays(-request.StartTimeByDay)
                };
                await _arbitreageCalculator.CalculateArbitrage(arbitageCalculator, cancellationToken);

                _logger.Information($"End => {nameof(QuarterlyContractArbitrageCommandHandler)}");
            }
            catch (Exception ex)
            {
                _logger.Information($"Error => {nameof(QuarterlyContractArbitrageCommandHandler)}, exception message => {ex.Message}");
                throw;
            }
        }
    }
}
