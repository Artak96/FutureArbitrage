using AutoMapper;
using FutureArbitrage.Application.Models.Response;
using FutureArbitrage.Domain.Abstractions;
using MediatR;
using Serilog;

namespace FutureArbitrage.Application.Pipeline.Queries.Handler
{
    internal class GetArbitrageResultsQueryHandler : IRequestHandler<GetArbitrageResultsQuery, List<GetArbitrageResults>>
    {
        protected internal readonly ILogger _logger = Log.ForContext(typeof(GetArbitrageResultsQueryHandler));

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetArbitrageResultsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GetArbitrageResults>> Handle(GetArbitrageResultsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.Information($"Start => {nameof(GetArbitrageResultsQueryHandler)}");

                var arbitrageResults = await _unitOfWork.ArbitrageResult.GetArbitrageResultByAssetAsync(request.Asset);
                return _mapper.Map<List<GetArbitrageResults>>(arbitrageResults);
            }
            catch (Exception ex)
            {
                _logger.Information($"Error => {nameof(GetArbitrageResultsQueryHandler)}, exception message => {ex.Message}");
                throw;
            }
        }
    }
}
