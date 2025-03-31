using FutureArbitrage.Application.Models.Response;
using MediatR;

namespace FutureArbitrage.Application.Pipeline.Queries
{
    public class GetArbitrageResultsQuery : IRequest<List<GetArbitrageResults>>
    {
        public string Asset { get; set; }
    }
}
