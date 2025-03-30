using FluentValidation;
using FutureArbitrage.Application.Pipeline.Commands;

namespace FutureArbitrage.Application.Validators
{
    public class QuarterlyContractArbitrageCommandValidator : AbstractValidator<QuarterlyContractArbitrageCommand>
    {
        public QuarterlyContractArbitrageCommandValidator()
        {
            RuleFor(x => x.ContractType)
               .NotEmpty()
               .NotNull()
               .WithMessage("ContractType is required.");

            RuleFor(x => x.IntervalByHours)
               .GreaterThan(0)
               .WithMessage("IntervalByHours must be greater than 0.");

            RuleFor(x => x.StartTimeByDay)
               .GreaterThan(0)
               .WithMessage("StartTimeByDay must be greater than 0.");
        }
    }
}
