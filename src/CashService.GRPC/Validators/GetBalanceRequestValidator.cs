using CashService.GRPC.ValidationRules;
using FluentValidation;

namespace CashService.GRPC.Validators
{
    // TODO: Change file location to CashService.Grpc.Infrastructure.Validators
    /// <summary>
    /// Validation rules for <seealso cref="GetBalanceRequest"/>
    /// </summary>
    public class GetBalanceRequestValidator : AbstractValidator<GetBalanceRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetBalanceRequestValidator"/> class.
        /// </summary>
        public GetBalanceRequestValidator()
        {
            RuleFor(e => e.Profileid)
                .MustBeValidGuid();
        }
    }
}