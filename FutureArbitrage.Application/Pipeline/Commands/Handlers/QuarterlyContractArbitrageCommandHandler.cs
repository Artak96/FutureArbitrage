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
            _logger.Information($"Start => {nameof(QuarterlyContractArbitrageCommandHandler)} => request {System.Text.Json.JsonSerializer.Serialize(request)}");

            var arbitageCalculator = new ArbitrageCalculatorDto
            {
                ContractType = request.ContractType,
                EndTime = DateTime.UtcNow,
                Interval = TimeSpan.FromHours(request.IntervalByHours),
                StartTime = DateTime.UtcNow.AddDays(-request.StartTimeByDay)
            };
            await _arbitreageCalculator.CalculateArbitrage(arbitageCalculator, cancellationToken);

            _logger.Information($"End => {nameof(QuarterlyContractArbitrageCommandHandler)}");
        }
    }
}
