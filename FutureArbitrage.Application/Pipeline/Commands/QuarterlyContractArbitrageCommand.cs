using MediatR;

namespace FutureArbitrage.Application.Pipeline.Commands
{
    public class QuarterlyContractArbitrageCommand : IRequest
    {
        public int StartTimeByDay { get; set; }
        public int IntervalByHours { get; set; }
    }
}
