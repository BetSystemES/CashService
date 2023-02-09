using CashService.GRPC.ValidationRules;
using FluentValidation;

namespace CashService.GRPC.Validators
{
    /// <summary>
    /// Validation rules for <seealso cref="WithdrawRequest"/>
    /// </summary>
    public class WithdrawRequestValidator : AbstractValidator<WithdrawRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WithdrawRequestValidator"/> class.
        /// </summary>
        public WithdrawRequestValidator()
        {
            RuleFor(e => e.Withdrawrequest.Profileid)
                .MustBeValidGuid();

            RuleForEach(e => e.Withdrawrequest.Transactions)
                .MustBeValidCashTypeInTransaction();
        }
    }
}