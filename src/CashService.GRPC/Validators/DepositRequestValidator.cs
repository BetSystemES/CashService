using CashService.GRPC.ValidationRules;
using FluentValidation;

namespace CashService.GRPC.Validators
{
    /// <summary>
    /// Validation rules for <seealso cref="DepositRequest"/>
    /// </summary>
    public class DepositRequestValidator : AbstractValidator<DepositRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DepositRequestValidator"/> class.
        /// </summary>
        public DepositRequestValidator()
        {
            RuleFor(e => e.Deposit.Profileid)
                .MustBeValidGuid();

            RuleForEach(e => e.Deposit.Transactions)
                .MustBeValidCashTypeInTransaction();
        }
    }
}