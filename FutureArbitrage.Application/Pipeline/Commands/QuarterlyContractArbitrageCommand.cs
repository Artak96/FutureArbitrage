﻿using FutureArbitrage.Domain.Enums;
using MediatR;

namespace FutureArbitrage.Application.Pipeline.Commands
{
    public class QuarterlyContractArbitrageCommand : IRequest
    {
        public int StartTimeByDay { get; set; }
        public int IntervalByHours { get; set; }
        public required string Asset { get; set; } = "BTCUSDT"; // e.g. BTCUSDT
        public ExchangeTypeEnum ExchangeType { get; set; }
    }
}
