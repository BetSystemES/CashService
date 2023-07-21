using CashService.GRPC.Extensions;
using FluentValidation;

namespace CashService.GRPC.Infrastructure.Validators
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
            RuleFor(e => e.Deposit.ProfileId)
                .MustBeValidGuid();

            RuleForEach(e => e.Deposit.Transactions)
                .MustBeValidCashTypeInTransaction();
        }
    }
}