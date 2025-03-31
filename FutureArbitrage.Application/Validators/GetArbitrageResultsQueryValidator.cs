using FluentValidation;
using FutureArbitrage.Application.Pipeline.Queries;

namespace FutureArbitrage.Application.Validators
{
    public class GetArbitrageResultsQueryValidator : AbstractValidator<GetArbitrageResultsQuery>
    {
        public GetArbitrageResultsQueryValidator()
        {
            RuleFor(x => x.Asset)
                .NotEmpty()
                .NotNull()
                .WithMessage("Asset is required.");
        }
    }
}
