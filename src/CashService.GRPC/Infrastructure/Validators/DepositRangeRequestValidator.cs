using CashService.GRPC.Extensions;
using FluentValidation;

namespace CashService.GRPC.Infrastructure.Validators
{
    /// <summary>
    /// Validation rules for <seealso cref="DepositRangeRequest"/>
    /// </summary>
    public class DepositRangeRequestValidator : AbstractValidator<DepositRangeRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DepositRangeRequestValidator"/> class.
        /// </summary>
        public DepositRangeRequestValidator()
        {
            RuleForEach(e => e.DepositRangeRequests)
                .MustBeValidCashTypeInTransactionModel();

            RuleForEach(e => e.DepositRangeRequests)
                .MustBeValidGuidInTransactionModel();
        }
    }
}