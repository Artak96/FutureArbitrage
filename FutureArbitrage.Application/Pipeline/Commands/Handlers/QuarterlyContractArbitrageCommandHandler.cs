using FutureArbitrage.Application.Services.Abstructions;
using MediatR;
using Serilog;

namespace FutureArbitrage.Application.Pipeline.Commands.Handlers
{
    internal class QuarterlyContractArbitrageCommandHandler : IRequestHandler<QuarterlyContractArbitrageCommand>
    {
        protected internal readonly Serilog.ILogger _logger = Log.ForContext(typeof(QuarterlyContractArbitrageCommandHandler));

        private readonly IArbitrageCalculatorService _arbitreageCalculator;

        public QuarterlyContractArbitrageCommandHandler(IArbitrageCalculatorService arbitreageCalculator)
        {
            _arbitreageCalculator = arbitreageCalculator;
        }

        public async Task Handle(QuarterlyContractArbitrageCommand request, CancellationToken cancellationToken)
        {
            DateTime startTime = DateTime.UtcNow.AddDays(-request.StartTimeByDay);
            DateTime endTime = DateTime.UtcNow;
            TimeSpan interval = TimeSpan.FromHours(request.IntervalByHours);

            var results = await _arbitreageCalculator.CalculateArbitrage(interval, startTime, endTime);

            throw new NotImplementedException();
        }
    }
}
