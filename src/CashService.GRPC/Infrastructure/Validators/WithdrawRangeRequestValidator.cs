using CashService.GRPC.Extensions;
using FluentValidation;

namespace CashService.GRPC.Infrastructure.Validators
{
    /// <summary>
    /// Validation rules for <seealso cref="WithdrawRangeRequest"/>
    /// </summary>
    public class WithdrawRangeRequestValidator : AbstractValidator<WithdrawRangeRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WithdrawRangeRequestValidator"/> class.
        /// </summary>
        public WithdrawRangeRequestValidator()
        {
            RuleForEach(e => e.WithdrawRangeRequests)
                .MustBeValidCashTypeInTransactionModel();

            RuleForEach(e => e.WithdrawRangeRequests)
                .MustBeValidGuidInTransactionModel();
        }
    }
}